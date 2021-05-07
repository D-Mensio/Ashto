using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, Piece
{
    public string label;
    public bool reached;
    public bool active;

    public bool IsAccessible(Direction direction)
    {
        return true;
    }

    public Piece GetNextPiece(Direction inDirection, out Direction outDirection)
    {
        outDirection = Direction.Stop;
        return null;
    }

    void Start()
    {
        reached = false;
        active = false;
        StartCoroutine(CheckActive());
    }

    private IEnumerator CheckActive()
    {
        while(true)
        {
            // execute block of code here
            if (reached)
            {
                active = true;
                reached = false;
            }
            else
                active = false;
            yield return new WaitForSeconds(1f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ball")
        {
            Ball ball = col.GetComponent<Ball>();
            if (ball.label.Equals(label))
            {
                Debug.Log("reached");
                reached = true;
            }
        }
    }
}
