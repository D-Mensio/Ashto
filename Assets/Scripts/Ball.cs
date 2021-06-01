using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Vector3 targetPosition;
    private PieceConnections targetPiece;
    private PieceConnections currentPiece;
    private bool targetIsMidPoint;
    private bool destroyOnNextMove;
    private bool targetReached;

    private bool isDeleting;
    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = new Material(rend.material);
    }

    public void Initialize(string label, Direction direction, PieceConnections piece, Vector3 position, float strength, Color color)
    {
        this.label = label;
        this.speed = 1;
        this.strength = strength;
        this.currentStrength = strength;
        this.direction = direction;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
        this.targetPosition = transform.position;
        this.currentPiece = piece;            
        this.targetIsMidPoint = true;
        this.destroyOnNextMove = false;
        this.targetReached = false;
        rend.material.color = color;
        this.color = color;
        UpdateTargetPosition();
        Move();
    }

    private void UpdateTargetPosition()
    {
        //get Direction
        if (targetIsMidPoint)
        {
            direction = currentPiece.GetNextDirection(direction);
            targetPiece = currentPiece.GetConnection(direction);
            targetIsMidPoint = false;
        }
        else if (!(targetPiece is null) && targetPiece.IsAccessible(direction))
        {    
            //direction doesn't change
            targetIsMidPoint = true;
        }
        else
        {
            //either next piece is not accessible or it doesn't exist
            destroyOnNextMove = true;
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
                targetReached = true;
                break;
        }
    }

    private void Move()
    {
        if (destroyOnNextMove || targetReached || (targetIsMidPoint && !targetPiece.IsAccessible(direction)))
        {
            Delete();
        }
        else
        {
            StartCoroutine(AsyncMove(targetPosition));
            UpdateTargetPosition();
        }
    }

    private IEnumerator AsyncMove(Vector3 targetPosition)
    {
        Vector3 startingPos = transform.position;

        float elapsedTime = 0;
        var time = 0.5f / speed;
        while (elapsedTime < time && !isDeleting)
        {
            Vector3 pos = transform.position;
            transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / time));

            currentStrength -= (transform.position - pos).magnitude;

            SetOpacity(Mathf.Max(0.2f + currentStrength / strength, 0.05f));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (!isDeleting)
        {
            if (targetIsMidPoint)
            {
                currentPiece = targetPiece;
            }
            Move();
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
        float initialOpacity = rend.material.color.a;
        while (time > 0)
        {
            SetOpacity(initialOpacity * time);
            time -= Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(100, 100, 0);
        Destroy(gameObject);
    }

    private void SetOpacity(float f)
    {
        Color col = rend.material.color;
        col.a = f;
        rend.material.color = col;
    }

}
