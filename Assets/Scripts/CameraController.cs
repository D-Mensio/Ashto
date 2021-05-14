using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private float targetSize;
    private float normalSize;
    private Vector3 targetPos;
    private Vector3 startingPos;


    public float speed;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        normalSize = cam.orthographicSize;
        targetSize = cam.orthographicSize;
        startingPos = transform.position;
        targetPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetSize != cam.orthographicSize)
            cam.orthographicSize += (targetSize - cam.orthographicSize) * speed * Time.deltaTime;
        if (Mathf.Abs(targetSize - cam.orthographicSize) < 0.01)
            cam.orthographicSize = targetSize;
        if(targetPos != transform.position)
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        if ((targetPos - transform.position).magnitude <= 0.01)
            transform.position = targetPos;
    }

    public void ZoomOut(float f)
    {
        targetSize = normalSize + f;
    }

    public void MoveY(float f)
    {
        targetPos = new Vector3(targetPos.x, targetPos.y + f, targetPos.z);
    }

    public void Reset()
    {
        targetSize = normalSize;
        targetPos = startingPos;
    }
}
