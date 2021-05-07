using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiblePiece : MonoBehaviour, Piece
{
    //public int guid;
    
    private Quaternion target;
    public int phase;
    public float yAngle;

    public bool isRotating;
    public float angleDetectRotation = 30f; //min angle to detect a rotation

    public GameObject northGameObject;
    public GameObject southGameObject;
    public GameObject westGameObject;
    public GameObject eastGameObject;

    private List<Ball> containedBalls;


    protected Piece northNeighbor;
    protected Piece southNeighbor;
    protected Piece westNeighbor;
    protected Piece eastNeighbor;

    /*
    //Executed only in editor mode when a field is changed
    void OnInspectorGUI()
    {
        switch (phase)
        {
            case 0:
                yAngle = 0f;
                break;
            case 1:
                yAngle = 90f;
                break;
            case 2:
                yAngle = 180f;
                break;
            case 3:
                yAngle = 270f;
                break;
        }
        transform.rotation = Quaternion.Euler(0, 0, yAngle);
    }
    */

    void Awake()
    {
        transform.rotation = Quaternion.Euler(0, 0, yAngle);
        target = transform.rotation;
        isRotating = false;
        containedBalls = new List<Ball>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (northGameObject)
            northNeighbor = northGameObject.GetComponent<Piece>();
        if (southGameObject)
            southNeighbor = southGameObject.GetComponent<Piece>();
        if (westGameObject)
            westNeighbor = westGameObject.GetComponent<Piece>();
        if (eastGameObject)
            eastNeighbor = eastGameObject.GetComponent<Piece>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Quaternion.Angle(transform.rotation,target) <= angleDetectRotation)
            isRotating = false;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
    }

    void OnMouseDown()
    {
        isRotating = true;
        yAngle = yAngle == 270f ? 0 : yAngle += 90.0f;
        phase = phase == 3 ? 0 : phase + 1;
        //Debug.Log(phase.ToString());

        // Rotate the cube by converting the angles into a quaternion.
        target = Quaternion.Euler(0, 0, yAngle);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
        foreach(Ball ball in new List<Ball>(containedBalls))
        {
            containedBalls.Remove(ball);
            if(ball != null)
                Destroy(ball.gameObject);
        }
    }

    public void RegisterBall(Ball ball)
    {
        containedBalls.Add(ball);
        StartCoroutine(RemoveBall(ball));
    }

    private IEnumerator RemoveBall(Ball ball)
    {
        yield return new WaitForSeconds(2f);
        containedBalls.Remove(ball);
    }

    public abstract bool IsAccessible(Direction direction);

    public abstract Piece GetNextPiece(Direction inDirection, out Direction outDirection);

}
