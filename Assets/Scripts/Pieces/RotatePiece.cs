using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePiece : MonoBehaviour
{
    private Quaternion target;
    public int phase { get; private set; }
    private float yAngle;
    [SerializeField]
    private float rotationSpeed = 5.0f;
    public bool isRotating { get; private set; }
    [SerializeField]
    private float angleDetectRotation = 30f; //min angle to detect a rotation

    private PieceColor pc;

    private void Awake()
    {
        yAngle = transform.rotation.eulerAngles.z;
        phase = (int)yAngle / 90;
        target = transform.rotation;
        isRotating = false;
    }

    private void Start()
    {
        pc = GetComponent<PieceColor>();  
    }

    private void Update()
    {
        float angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target));
        if (isRotating && angle <= angleDetectRotation)
        {
            isRotating = false;
        }
        if (angle > 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSpeed);
        }
        else if (angle > 0)
        {
            transform.rotation = target;
        }

    }

    public void OnTouch()
    {
        isRotating = true;
        yAngle = yAngle == 270f ? 0 : yAngle += 90.0f;
        phase = phase == 3 ? 0 : phase + 1;
        target = Quaternion.Euler(0, 0, yAngle);

        foreach (Ball ball in new List<Ball>(pc.containedBalls))
        {
            ball.Delete();
        }

    }
}
