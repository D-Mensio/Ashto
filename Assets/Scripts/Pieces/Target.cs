using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, Piece
{
    public string label;
    public bool reached;
    public bool active;

    public Color color;
    //private List<Renderer> borders;

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
        //targetColor = defaultColor;
        //borders = new List<Renderer>();
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Border"))
            {
                Renderer childRenderer = child.gameObject.GetComponent<Renderer>();
                //borders.Add(childRenderer);
                childRenderer.material = new Material(childRenderer.material);
                childRenderer.material.color = color;
            }
        }
        StartCoroutine(CheckActive());
    }

    private IEnumerator CheckActive()
    {
        while(true)
        {
            if (reached)
            {
                active = true;
                reached = false;
            }
            else
            {
                active = false;
            }
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
                reached = true;
            }
        }
    }
}
