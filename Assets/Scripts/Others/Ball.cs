using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the color and movement of a ball
public class Ball : MonoBehaviour
{
    //Parameters
    public string label;
    public float speed;
    public float strength;
    public float currentStrength;
    private Renderer rend;
    public Color color;

    //Movement variables
    private Direction direction;
    private Vector3 startingPos;
    private Vector3 targetPosition;
    private PieceConnections currentPiece;
    private bool targetIsMidPoint;

    private float progress;

    private bool isDeleting;
    private bool isCreating;

    private Vector3 ballDim;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = new Material(rend.material);
        isCreating = true;
        ballDim = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void Initialize(string label, Direction direction, PieceConnections piece, Vector3 position, float strength, Color color)
    {
        this.label = label;
        this.speed = 2;
        this.progress = 0;
        this.strength = strength;
        this.currentStrength = strength;
        this.direction = direction;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
        this.startingPos = transform.position;
        this.targetPosition = transform.position;
        this.currentPiece = piece;            
        this.targetIsMidPoint = true;
        rend.material.color = color;
        this.color = color;
        StartCoroutine(CreateBall());
    }

    private void Update()
    {
        if (!isDeleting && !isCreating)
        {
            if (transform.position == targetPosition)
            {
                UpdateTargetPosition();
                //Debug.Log("Updated target");
            }
            progress = Mathf.Clamp(progress + Time.deltaTime * speed,0,1);

            //Update position
            Vector3 tempPos = transform.position;
            transform.position = Vector3.Lerp(startingPos, targetPosition, progress);

            currentStrength -= (transform.position - tempPos).magnitude;
            SetOpacity(Mathf.Max(0.2f + currentStrength / strength, 0.05f));
        }
    }

    //Calculates next target position for the ball
    private void UpdateTargetPosition()
    {
        progress = 0;
        startingPos = targetPosition;
        //get Direction
        if (targetIsMidPoint)
        {
            direction = currentPiece.GetNextDirection(direction);
            currentPiece = currentPiece.GetConnection(direction);
            targetIsMidPoint = false;
        }
        else if (!(currentPiece is null) && currentPiece.IsAccessible(direction, label))
        {    
            //direction doesn't change
            targetIsMidPoint = true;
        }
        else
        {
            //either next piece is not accessible or it doesn't exist
            Delete();
        }
        //update target position
        switch (direction)
        {
            case Direction.North:
                targetPosition += new Vector3(0, 0.5f, 0);
                break;
            case Direction.South:
                targetPosition += new Vector3(0, -0.5f, 0);
                break;
            case Direction.East:
                targetPosition += new Vector3(0.5f, 0, 0);
                break;
            case Direction.West:
                targetPosition += new Vector3(-0.5f, 0, 0);
                break;
            case Direction.Error:
                //error handling
            case Direction.Stop:
                Delete();
                break;
        }
    }

    public void Delete()
    {
        if (!isDeleting)
        {
            isDeleting = true;
            StartCoroutine(DestroyBall());
        }
    }

    private IEnumerator DestroyBall()
    {
        float time = 0.15f;
        //float initialOpacity = rend.material.color.a;
        while (time > 0)
        {
            //SetOpacity(initialOpacity * time);
            transform.localScale = Vector3.Lerp(ballDim, Vector3.zero, 1 - time/0.15f);
            time -= Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(100, 100, 0);
        Destroy(gameObject);
    }

    private IEnumerator CreateBall()
    {
        float time = 0.15f;
        //float initialOpacity = rend.material.color.a;
        while (time > 0)
        {
            //SetOpacity(initialOpacity * time);
            transform.localScale = Vector3.Lerp(Vector3.zero, ballDim, 1 - time / 0.15f);
            time -= Time.deltaTime;
            yield return null;
        }
        isCreating = false;
    }

    private void SetOpacity(float f)
    {
        Color col = rend.material.color;
        col.a = f;
        rend.material.color = col;
    }

}
