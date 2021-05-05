using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour, Piece
{
    public int strength;
    public string label;

    public GameObject northGameObject;
    public GameObject southGameObject;
    public GameObject westGameObject;
    public GameObject eastGameObject;

    public GameObject ballPrefab;

    protected Piece northNeighbor;
    protected Piece southNeighbor;
    protected Piece westNeighbor;
    protected Piece eastNeighbor;

    void Start()
    {
        if(northGameObject)
            northNeighbor = northGameObject.GetComponent<Piece>();
        if (southGameObject)
            southNeighbor = southGameObject.GetComponent<Piece>();
        if (westGameObject)
            westNeighbor = westGameObject.GetComponent<Piece>();
        if (eastGameObject)
            eastNeighbor = eastGameObject.GetComponent<Piece>();
        StartCoroutine(SpawnBalls());
    }

    public bool IsAccessible(Direction direction)
    {
        return false;
    }

    public Piece GetNextPiece(Direction inDirection, out Direction outDirection)
    {
        outDirection = inDirection;
        return inDirection switch
        {
            Direction.North => northNeighbor,
            Direction.South => southNeighbor,
            Direction.East => eastNeighbor,
            Direction.West => westNeighbor,
            _ => null,
        };
    }

    public void Activate()
    {
        if (!(northNeighbor is null) && northNeighbor.IsAccessible(Direction.North))
        {
            Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
            newBall.Initialize(label, Direction.North, this, transform.position);
        }
        if (!(southNeighbor is null) && southNeighbor.IsAccessible(Direction.South))
        {
            Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
            newBall.Initialize(label, Direction.South, this, transform.position);
        }
        if (!(eastNeighbor is null) && eastNeighbor.IsAccessible(Direction.East))
        {
            Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
            newBall.Initialize(label, Direction.East, this, transform.position);
        }
        if (!(westNeighbor is null) && westNeighbor.IsAccessible(Direction.West))
        {
            Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
            newBall.Initialize(label, Direction.West, this, transform.position);
        }
    }

    private IEnumerator SpawnBalls()
    {
        while (true)
        {
            Activate();
            yield return new WaitForSeconds(1f);
        }
    }
}
