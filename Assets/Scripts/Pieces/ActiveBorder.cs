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

    private PieceConnections northConnections;
    private PieceConnections eastConnections;
    private PieceConnections southConnections;
    private PieceConnections westConnections;

    private string label;
    private bool isTarget;
    private bool isSource;

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
        PieceConnections connections = GetComponent<PieceConnections>();
        if (connections.GetConnection(Direction.North))
            northConnections = connections.GetConnection(Direction.North);
        if (connections.GetConnection(Direction.East))
            eastConnections = connections.GetConnection(Direction.East);
        if (connections.GetConnection(Direction.South))
            southConnections = connections.GetConnection(Direction.South);
        if (connections.GetConnection(Direction.West))
            westConnections = connections.GetConnection(Direction.West);


        if (GetComponent<TargetActivation>())
        {
            label = GetComponent<TargetActivation>().label;
            isTarget = true;
            isSource = false;
        }
        else if (GetComponent<SourceSpawn>())
        {
            label = GetComponent<SourceSpawn>().label;
            isTarget = false;
            isSource = true;
        }
    }

    private void Start()
    {
        if (northConnections && northConnections.IsConnected(Direction.North, label, isTarget, isSource))
            northBorder.GetComponent<Animator>().Play("open");

        if (eastConnections && eastConnections.IsConnected(Direction.East, label, isTarget, isSource))
            eastBorder.GetComponent<Animator>().Play("open");

        if (southConnections && southConnections.IsConnected(Direction.South, label, isTarget, isSource))
            southBorder.GetComponent<Animator>().Play("open");

        if (westConnections && westConnections.IsConnected(Direction.West, label, isTarget, isSource))
            westBorder.GetComponent<Animator>().Play("open");
    }


    // Update is called once per frame
    void Update()
    {
        if (northConnections && northConnections.IsConnected(Direction.North, label, isTarget, isSource))
            northBorder.GetComponent<Animator>().SetBool("isOpen", true);
        else
            northBorder.GetComponent<Animator>().SetBool("isOpen", false);
        if (eastConnections && eastConnections.IsConnected(Direction.East, label, isTarget, isSource))
            eastBorder.GetComponent<Animator>().SetBool("isOpen", true);
        else
            eastBorder.GetComponent<Animator>().SetBool("isOpen", false);
        if (southConnections && southConnections.IsConnected(Direction.South, label, isTarget, isSource))
            southBorder.GetComponent<Animator>().SetBool("isOpen", true);
        else
            southBorder.GetComponent<Animator>().SetBool("isOpen", false);
        if (westConnections && westConnections.IsConnected(Direction.West, label, isTarget, isSource))
            westBorder.GetComponent<Animator>().SetBool("isOpen", true);
        else
            westBorder.GetComponent<Animator>().SetBool("isOpen", false);
    }
}
