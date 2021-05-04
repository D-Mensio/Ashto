using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public string label;
    public float speed;
    public Direction direction;

    public bool reachedMidPoint;

    private Vector3 targetPosition;

    public Piece currentPiece;

    public bool destroyOnTarget;

    public void Initialize(string label, Direction direction, Piece currentPiece, Vector3 position)
    {
        this.label = label;
        this.direction = direction;
        this.currentPiece = currentPiece;
        this.targetPosition = new Vector3 (position.x, position.y, -1);
        this.reachedMidPoint = true;

        Move(direction);
        //UpdateTargetPosition();
    }

    private void UpdateTargetPosition()
    {
        if (reachedMidPoint)
        {
            Direction newDirection;
            currentPiece = currentPiece.GetNextPiece(direction, out newDirection);
            direction = newDirection;
            reachedMidPoint = false;
        }
        else if (!(currentPiece is null) && currentPiece.IsAccessible(direction))
        {
            reachedMidPoint = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Move(Direction direction)
    {
        Debug.Log("Move");
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
        }
        StartCoroutine(AsyncMove(targetPosition));
        UpdateTargetPosition();
    }

    private IEnumerator AsyncMove(Vector3 targetPosition)
    {
        Vector3 startingPos = transform.position;

        float elapsedTime = 0;
        var time = 0.5f / speed;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("done");
        Move(direction);
    }
}
