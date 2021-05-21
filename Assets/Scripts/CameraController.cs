using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float targetSize;
    private float normalSize;
    
    public Vector3 zeroPos;
    public Vector3 offScreenPos;

    private Vector3 relativePos1;
    private Vector3 relativePos2;

    private Vector3 targetPos1;
    private Vector3 targetPos2;

    private Vector3 targetPos;

    private bool levelTransition;
    private bool skipFrame;

    public float speed1;
    public float speed2;
    public float zoom;


    private void Awake()
    {
        cam = GetComponent<Camera>();
        normalSize = cam.orthographicSize;
        targetSize = cam.orthographicSize;
        targetPos1 = zeroPos;
        targetPos2 = zeroPos;
        targetPos = zeroPos;
        relativePos1 = offScreenPos;
        relativePos2 = zeroPos;
        transform.position = offScreenPos;
        skipFrame = true; //skip first frame
    }

    // Start is called before the first frame update
    void Start()
    {
        levelTransition = false;
        Menu m = GameObject.Find("UI").GetComponent<Menu>();
        if (m.open)
            ZoomOut();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        //Debug.Log(Time.unscaledDeltaTime);
        if (skipFrame)
        {
            //Debug.Log("skipped");
            skipFrame = false;
        }
        else
        {
            //Debug.Log("not skipped");       

            //Control zoom
            if (targetSize != cam.orthographicSize)
                cam.orthographicSize += (targetSize - cam.orthographicSize) * speed2 * Time.unscaledDeltaTime;
            if (Mathf.Abs(targetSize - cam.orthographicSize) < 0.01)
                cam.orthographicSize = targetSize;

            //Calculate position relative to movement 1 (level transition)
            Vector3 relativeMov1 = relativePos1;
            if (relativePos1 != targetPos1)
            {
                relativePos1 = Vector3.Lerp(relativePos1, targetPos1, Time.unscaledDeltaTime * speed1);
                if ((targetPos1 - relativePos1).magnitude <= 0.01)
                {
                    relativePos1 = targetPos1;
                }
                relativeMov1 = relativePos1 - relativeMov1;
            }
            else
                relativeMov1 = Vector3.zero;

            

            //Calculate position relative to movement 2 (menu open/closed)
            Vector3 relativeMov2 = relativePos2;
            if (relativePos2 != targetPos2)
            {
                relativePos2 = Vector3.Lerp(relativePos2, targetPos2, Time.unscaledDeltaTime * speed2);
                if ((targetPos2 - relativePos2).magnitude <= 0.01)
                {
                    relativePos2 = targetPos2;
                }
                relativeMov2 = relativePos2 - relativeMov2;
            }
            else
                relativeMov2 = Vector3.zero;

            //Extra check for actual transform position to avoid small errors (probably due to the rounding of the the relative movements)
            transform.position = transform.position + relativeMov1 + relativeMov2;
            if (targetPos1 == relativePos1 && targetPos2 == relativePos2)
                transform.position = targetPos;
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        //Enable skipFrame when unfocus in app
        if (!hasFocus)
        {
           //Debug.Log("focus");
            skipFrame = true;
            //timeFromLastFrame = 0;
        }
        else
        {
            //Debug.Log("unfocus");
            skipFrame = true;
            //timeFromLastFrame = 0;
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        //Enable skipFrame when pausing app
        if (pauseStatus)
        {
            //Debug.Log("Skipping next frame");
            skipFrame = true;
        }
    }

    public void ZoomOut()
    {
        if (!levelTransition)
        {
            targetSize = normalSize + zoom;
            targetPos2 += new Vector3(0, -zoom, 0);
            targetPos += new Vector3(0, -zoom, 0);
        }
    }

    public void Reset()
    {
        if (!levelTransition)
        {
            targetSize = normalSize;
            targetPos2 = zeroPos;
            targetPos = zeroPos;
        }
    }

    public void LevelTransition()
    {
        targetPos1 = offScreenPos;
        targetPos = offScreenPos;
        levelTransition = true;
    }
}
