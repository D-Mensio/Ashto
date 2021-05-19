using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiblePiece : MonoBehaviour, Piece
{
    //public int guid;
    
    private Quaternion target;
    public int phase;
    public float yAngle;

    public Color defaultColor;
    private Color targetColor;
    private Dictionary<Color, int> containedColors;

    public bool isRotating;
    public float angleDetectRotation = 30f; //min angle to detect a rotation

    public GameObject northGameObject;
    public GameObject southGameObject;
    public GameObject westGameObject;
    public GameObject eastGameObject;

    private Material borderMaterial;

    private List<Ball> containedBalls;

    private Menu menu;


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
        targetColor = defaultColor;
        containedBalls = new List<Ball>();
        containedColors = new Dictionary<Color, int>();

        borderMaterial = new Material(Shader.Find("Sprites/Default"));
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Border"))
            {
                Renderer childRenderer = child.gameObject.GetComponent<Renderer>();
                childRenderer.material = borderMaterial;
            }
        }

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

        menu = GameObject.Find("Image").GetComponent<Menu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Quaternion.Angle(transform.rotation,target) <= angleDetectRotation)
            isRotating = false;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
        ColorBorders();
        //var asString = string.Join(",", containedColors);
        //Debug.Log(asString);

        borderMaterial.color = Color.Lerp(borderMaterial.color, targetColor, Time.deltaTime * 5);
    }

    void OnMouseDown()
    {
        if (!menu.open)
        {
            isRotating = true;
            yAngle = yAngle == 270f ? 0 : yAngle += 90.0f;
            phase = phase == 3 ? 0 : phase + 1;
            //Debug.Log(phase.ToString());

            // Rotate the cube by converting the angles into a quaternion.
            target = Quaternion.Euler(0, 0, yAngle);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
            foreach (Ball ball in new List<Ball>(containedBalls))
            {
                DeRegisterBall(ball);
                if (ball != null)
                    Destroy(ball.gameObject);
            }
        }
    }

    public void RegisterBall(Ball ball)
    {
        //Debug.Log("newBallRegistered");
        containedBalls.Add(ball);
        if (containedColors.TryGetValue(ball.color, out int count))
        {
            containedColors[ball.color] = count + 1;
        }
        else
        {
            containedColors.Add(ball.color,1);
        }
        StartCoroutine(RemoveBall(ball));
    }

    public void DeRegisterBall(Ball ball)
    {
        if (containedBalls.Contains(ball))
        {
            //Debug.Log("ballRemoved");
            containedBalls.Remove(ball);
            if (containedColors.TryGetValue(ball.color, out int count))
            {
                if (count == 1)
                    containedColors.Remove(ball.color);
                else
                    containedColors[ball.color] = count - 1;
            }
        }
    }

    private IEnumerator RemoveBall(Ball ball)
    {
        yield return new WaitForSeconds(2f);
        DeRegisterBall(ball);
    }

    private void ColorBorders()
    {
        Color borderColor = defaultColor;

        foreach(Color c in containedColors.Keys)
        {
            if (borderColor == defaultColor)
            {
                borderColor = c;
                //Debug.Log(borderColor);
            }
            else
                borderColor = Color.Lerp(borderColor, c, 0.5f);
        }

        targetColor = borderColor;

    }

    public abstract bool IsAccessible(Direction direction);

    public abstract Piece GetNextPiece(Direction inDirection, out Direction outDirection);

}
