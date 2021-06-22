using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the rotation of a piece. Requires a PieceColor component
public class RotatePiece : MonoBehaviour
{
    private Quaternion target;
    public int phase { get; private set; }  //current phase of the piece. Each piece has 4 possible phases/states, since each rotation is 90 degree
    private float yAngle;   //angle corresponding to the current phase, expressed in degrees
    [SerializeField]
    private float rotationSpeed = 5.0f;
    public bool isRotating { get; private set; }
    [SerializeField]
    private float angleDetectRotation = 30f;    //min angle to detect a rotation (if the difference between current rotation and target is inferior, the piece is not considered as rotating)

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

    //Updates piece rotation towards the target rotation
    private void Update()
    {
        float angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target));
        if (isRotating && angle <= angleDetectRotation)
        {
            isRotating = false; //stop the piece's rotation
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

    //Rotate the piece by 90 degree counterclockwise
    public void OnTouch()
    {
        isRotating = true;
        yAngle = yAngle == 270f ? 0 : yAngle += 90.0f;  //update target angle
        phase = phase == 3 ? 0 : phase + 1; //update piece phase
        target = Quaternion.Euler(0, 0, yAngle);

        //Destroy all balls currently contained in the piece
        foreach (Ball ball in new List<Ball>(pc.containedBalls))
        {
            ball.Delete();
        }

    }
}
