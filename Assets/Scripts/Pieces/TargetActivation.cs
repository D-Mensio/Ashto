using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetActivation : MonoBehaviour
{
    public string label;
    private bool reached;
    public bool active { get; private set; }
    [SerializeField]
    private Color color;
    private float targetOpacity;
    private Material borderMaterial;

    private void Awake()
    {
        reached = false;
        active = false;
        borderMaterial = GetComponent<ActiveBorder>().borderMaterial;
        targetOpacity = 0.5f;
        color.a = 0.5f;
        borderMaterial.color = color;
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
