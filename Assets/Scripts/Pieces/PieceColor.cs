using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the color of a piece's borders
public class PieceColor : MonoBehaviour
{
    [SerializeField]
    private Color defaultColor; //default color for the borders
    private Color targetColor;
    public List<Ball> containedBalls;   //list containing all the balls currently present in the piece
    private Dictionary<Color, int> containedColors; //dictionary containing, for each color of the balls present in containedBalls, the number of balls currently present in the piece
    private Material borderMaterial;

    private void Awake()
    {
        containedBalls = new List<Ball>();
        containedColors = new Dictionary<Color, int>();
    }

    private void Start()
    {
        borderMaterial = new Material(Shader.Find("Sprites/Default"));
        borderMaterial.color = defaultColor;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Border"))
            {
                Renderer childRenderer = child.gameObject.GetComponent<Renderer>();
                childRenderer.material = borderMaterial;
            }
        }
    }

    //Updates border color toward the target color
    private void Update()
    {
        setBordersColor();
        borderMaterial.color = Color.Lerp(borderMaterial.color, targetColor, Time.deltaTime * 5);
    }

    //Checks if the colliding object entering the piece is a ball, and updates containedBalls and containedColors
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ball")
        {
            Ball ball = col.GetComponent<Ball>();
            containedBalls.Add(ball);
            if (containedColors.TryGetValue(ball.color, out int count))
            {
                containedColors[ball.color] = count + 1;
            }
            else
            {
                containedColors.Add(ball.color, 1);
            }
        }
    }

    //Checks if the colliding object exiting the piece is a ball, and updates containedBalls and containedColors
    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ball")
        {
            Ball ball = col.GetComponent<Ball>();
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
    }

    //Calculates target color as an interpolation of the colors of containedColors
    private void setBordersColor()
    {
        Color borderColor = defaultColor;

        foreach (Color c in containedColors.Keys)
        {
            if (borderColor == defaultColor)
            {
                borderColor = c;
                //Debug.Log(borderColor);
            }
            else
                borderColor = InterpolateColors(containedColors.Keys);
        }

        targetColor = borderColor;
    }

    //Calculates the linear interpolation of a collection of colors
    private Color InterpolateColors(ICollection<Color> colors)
    {

        float increment = 1f / colors.Count;
        Color color = new Color(0, 0, 0, 0);
        foreach (Color c in colors)
        {
            color += c * increment;   
        }
        color.a = 1;
        return color;
    }
}
