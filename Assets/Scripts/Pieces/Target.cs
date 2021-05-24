using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, Piece
{
    public string label;
    public bool reached;
    public bool active;

    public Color color;
    private float targetOpacity;
    private Material borderMaterial;
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
        borderMaterial = new Material(Shader.Find("Sprites/Default"));
        targetOpacity = 0.5f;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Border"))
            {
                Renderer childRenderer = child.gameObject.GetComponent<Renderer>();
                //borders.Add(childRenderer);
                childRenderer.material = borderMaterial;
            }
            borderMaterial.color = color;
        }
        //StartCoroutine(CheckActive());
    }

    void Update()
    {
        Color currColor = borderMaterial.color;
        currColor.a = Mathf.Lerp(currColor.a, targetOpacity, Time.deltaTime * 5);
        borderMaterial.color = currColor;    
    }

    private IEnumerator CheckActive()
    {
        //Debug.Log("act");
        while(reached)
        {
            active = true;
            reached = false;
            targetOpacity = 1f;
                yield return new WaitForSeconds(1.2f);
        }
        active = false;
        targetOpacity = 0.5f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ball")
        {
            Ball ball = col.GetComponent<Ball>();
            if (ball.label.Equals(label))
            {
                reached = true;
                if (!active)
                {
                    StartCoroutine(CheckActive());
                }
            }
        }
    }


}
