using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Controls inputs for touch/mouse based devices
public class InputManager : MonoBehaviour
{
    public Camera cam;
    public Camera effectCam;
    public GameObject touchEffect; //touch/click visual feedback
    private Menu menu;
    private BackButton backButton;
    ProcessInputs processFunc;

    delegate void ProcessInputs();

    private void Start()
    {
        menu = GameObject.Find("UI").GetComponent<Menu>();
        backButton = GameObject.Find("BackButton").GetComponent<BackButton>();
        if (Input.touchSupported)
            processFunc = ProcessTouch;
        else
            processFunc = ProcessMouse;
    }

    void Update()
    {
        if (!menu.open && !backButton.open)
        {
            processFunc();
        }
    }

    private void ProcessTouch()
    {
        HashSet<RotatePiece> touchedObjects = new HashSet<RotatePiece>();
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            // Handle finger movements based on TouchPhase
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch detected");
                Vector3 pos = cam.ScreenToWorldPoint(touch.position);

                Vector3 effectPos = effectCam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 5));
                PlayTouchEffect(effectPos);
                //Debug.Log(pos);
                List<GameObject> gs = GetObjAtPos(pos);
                foreach (GameObject g in gs)
                {
                    RotatePiece r = g.GetComponent<RotatePiece>();
                    if (r)
                        touchedObjects.Add(r);
                }
            }
        }

        foreach (RotatePiece r in touchedObjects)
        {
            r.OnTouch();
        }
    }

    private void ProcessMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click detected");
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector3 effectPos = effectCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
            PlayTouchEffect(effectPos);

            List<GameObject> gs = GetObjAtPos(pos);
            foreach (GameObject g in gs)
            {
                RotatePiece r = g.GetComponent<RotatePiece>();
                if (r)
                    r.OnTouch();
            }
        }
    }

    private List<GameObject> GetObjAtPos(Vector3 p)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(p, 0);
        return new List<GameObject> (hitColliders.Select(x => x.gameObject));
    }

    private void PlayTouchEffect(Vector3 pos)
    {
        GameObject touchEff = Instantiate(touchEffect, pos, Quaternion.identity);
        StartCoroutine(DestroyAfter1s(touchEff));
    }

    private IEnumerator DestroyAfter1s(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        GameObject.Destroy(obj);
    }
}
