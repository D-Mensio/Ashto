using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing main camera movement for each scene
public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float targetSize;
    private float normalSize;

    [SerializeField]
    private Vector3 zeroPos;
    private Vector3 offScreenPos;

    //Variables related to movement 1 (due to level transition)
    private Vector3 relativePos1;
    private Vector3 targetPos1;

    //Variable related to movement 2 (due to menu opening/closing)
    private Vector3 relativePos2;
    private Vector3 targetPos2;

    //Position obtained adding the relative movements of types 1 and 2
    private Vector3 targetPos;

    private bool levelTransition;
    private bool skipFrame;

    [SerializeField]
    private float speed1;
    [SerializeField]
    private float speed2;
    [SerializeField]
    private float zoomLevel;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        levelTransition = false;
        normalSize = cam.orthographicSize;
        zoomLevel = (normalSize / 3);
        targetSize = cam.orthographicSize;
        offScreenPos = new Vector3(0, -2 * normalSize, -10);    //calculate offsceen position based on camera's size
        targetPos1 = zeroPos;
        targetPos2 = zeroPos;
        targetPos = zeroPos;
        relativePos1 = offScreenPos;
        relativePos2 = zeroPos;
        transform.position = offScreenPos;
    }

    private void Start()
    {

        skipFrame = true; //skip first frame

        //if menu is open when loading the scene, the camera starts zoomed out
        Menu m = GameObject.Find("UI").GetComponent<Menu>();
        if (m.open)
            ZoomOut();
    }


    //Updates gradually camera position and zoom
    void Update()
    {
        if (skipFrame)
        {
            skipFrame = false;
        }
        else
        {
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

    //Sets next frame to be skipped on application focus/unfocus to avoid excessive movement due to big values of Time.unscaledDeltaTime
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            skipFrame = true;
        }
        else
        {
            skipFrame = true;
        }
    }

    //Sets next frame to be skipped on application pause to avoid excessive movement due to big values of Time.unscaledDeltaTime
    void OnApplicationPause(bool pauseStatus)
    {
        //Enable skipFrame when pausing app
        if (pauseStatus)
        {
            //Debug.Log("Skipping next frame");
            skipFrame = true;
        }
    }

    //Sets new target position/zoom for the camera, used for menu open animation
    public void ZoomOut()
    {
        if (!levelTransition)
        {
            targetSize = normalSize + zoomLevel;
            targetPos2 += new Vector3(0, -zoomLevel, 0);
            targetPos += new Vector3(0, -zoomLevel, 0);
        }
    }

    //Resets target position/zoom for the camera to the default values
    public void Reset()
    {
        if (!levelTransition)
        {
            targetSize = normalSize;
            targetPos2 = zeroPos;
            targetPos = zeroPos;
        }
    }

    //Controls camera movement for level transitions
    public void LevelTransition()
    {
        targetPos1 = offScreenPos;
        targetPos = offScreenPos;
        levelTransition = true;
    }
}
