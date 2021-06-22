using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the activation of a target. Requires a TargetConnection and an ActiveBorder component
public class TargetActivation : MonoBehaviour
{
    public string label;
    private bool reached;   //true if, since last check, the target has been reached by any ball with the correct label, and strength >= 0
    public bool active { get; private set; }    //true if the target is currently active
    [SerializeField]
    private Color color;
    private float targetOpacity;
    private Material borderMaterial;

    private void Awake()
    {
        reached = false;
        active = false;     
        targetOpacity = 0.5f;
    }

    private void Start()
    {
        borderMaterial = GetComponent<ActiveBorder>().borderMaterial;
        color.a = 0.5f;
        borderMaterial.color = color;
    }

    //Updates borders' opacity towards the target opacity
    void Update()
    {
        Color currColor = borderMaterial.color;
        currColor.a = Mathf.Lerp(currColor.a, targetOpacity, Time.deltaTime * 5);
        borderMaterial.color = currColor;
    }

    //Coroutine that checks every 1.2 seconds if the target has been reached by any ball with the correct label, and strength >= 0. In the negative case, the target is deactivated, and the coroutine stopped
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

    //Checks if the colliding object is a ball with the same label of the target, and with strength >= 0
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ball")
        {
            Ball ball = col.GetComponent<Ball>();
            if (ball.label.Equals(label) && ball.currentStrength > 0)
            {
                reached = true;
                //if the target is not currently active, start activation check coroutine
                if (!active)
                {
                    StartCoroutine(CheckActive());
                }
            }
        }
    }
}
