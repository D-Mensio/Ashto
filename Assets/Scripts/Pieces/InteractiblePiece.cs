using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiblePiece : MonoBehaviour, Piece
{
    //public int guid;
    
    private Quaternion target;
    public int phase;
    public float yAngle;

    public GameObject northGameObject;
    public GameObject southGameObject;
    public GameObject westGameObject;
    public GameObject eastGameObject;

    protected Piece northNeighbor;
    protected Piece southNeighbor;
    protected Piece westNeighbor;
    protected Piece eastNeighbor;

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

        yAngle = transform.rotation.y;
        phase = 0;

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);

    }

    void OnMouseDown()
    {
        yAngle = yAngle == 270f ? 0 : yAngle += 90.0f;
        phase = phase == 3 ? 0 : phase + 1;
        //Debug.Log(phase.ToString());

        // Rotate the cube by converting the angles into a quaternion.
        target = Quaternion.Euler(0, 0, yAngle);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
    }

    public abstract bool IsAccessible(Direction direction);

    public abstract Piece GetNextPiece(Direction inDirection, out Direction outDirection);

}
