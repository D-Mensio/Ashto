using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Camera cam;
    private Menu menu;
    ProcessInputs processFunc;

    delegate void ProcessInputs();

    private void Start()
    {
        menu = GameObject.Find("UI").GetComponent<Menu>();
        if (Input.touchSupported)
            processFunc = ProcessTouch;
        else
            processFunc = ProcessMouse;
    }

    void Update()
    {
        if (!menu.open)
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
                Debug.Log(pos);
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
}