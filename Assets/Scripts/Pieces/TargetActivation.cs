using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActivation : MonoBehaviour
{
    [SerializeField]
    private string label;
    private bool reached;
    public bool active { get; private set; }
    [SerializeField]
    private Color color;
    private float targetOpacity;
    private Material borderMaterial;

    private void Start()
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
                childRenderer.material = borderMaterial;
            }
            borderMaterial.color = color;
        }
    }

    void Update()
    {
        Color currColor = borderMaterial.color;
        currColor.a = Mathf.Lerp(currColor.a, targetOpacity, Time.deltaTime * 5);
        borderMaterial.color = currColor;
    }

    private IEnumerator CheckActive()
    {
        while (reached)
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
            if (ball.label.Equals(label) && ball.currentStrength > 0)
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
