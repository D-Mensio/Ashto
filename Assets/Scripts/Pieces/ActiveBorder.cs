using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBorder : MonoBehaviour
{
    public Material borderMaterial;

    public GameObject northBorder;
    public GameObject eastBorder;
    public GameObject southBorder;
    public GameObject westBorder;

    private PieceConnections connections;
    private string label;

    void Awake()
    {
        //Set shared instance of material for all borders of object
        borderMaterial = new Material(Shader.Find("Sprites/Default"));
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Border"))
            {
                Renderer childRenderer = child.gameObject.GetComponent<Renderer>();
                childRenderer.material = borderMaterial;
            }
        }
        connections = GetComponent<PieceConnections>();
        if (GetComponent<TargetActivation>())
            label = GetComponent<TargetActivation>().label;
        else if (GetComponent<SourceSpawn>())
            label = GetComponent<SourceSpawn>().label;
    }

    // Update is called once per frame
    void Update()
    {
        if (connections.GetConnection(Direction.North) && connections.GetConnection(Direction.North).IsAccessible(Direction.North, label))
            northBorder.GetComponent<Animator>().SetBool("isOpen", true);
        else
            northBorder.GetComponent<Animator>().SetBool("isOpen", false);
        if (connections.GetConnection(Direction.East) && connections.GetConnection(Direction.East).IsAccessible(Direction.East, label))
            eastBorder.GetComponent<Animator>().SetBool("isOpen", true);
        else
            eastBorder.GetComponent<Animator>().SetBool("isOpen", false);
        if (connections.GetConnection(Direction.South) && connections.GetConnection(Direction.South).IsAccessible(Direction.South, label))
            southBorder.GetComponent<Animator>().SetBool("isOpen", true);
        else
            southBorder.GetComponent<Animator>().SetBool("isOpen", false);
        if (connections.GetConnection(Direction.West) && connections.GetConnection(Direction.West).IsAccessible(Direction.West, label))
            westBorder.GetComponent<Animator>().SetBool("isOpen", true);
        else
            westBorder.GetComponent<Animator>().SetBool("isOpen", false);
    }
}
