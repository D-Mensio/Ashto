using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceSpawn : MonoBehaviour
{
    [SerializeField]
    private int strength;

    public string label;
    [SerializeField]
    private Color color;

    public GameObject ballPrefab;

    private SourceConnections sc;



    private void Awake()
    {
        sc = GetComponent<SourceConnections>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ActiveBorder>().borderMaterial.color = color;
        StartCoroutine(SpawnBalls());
    }

    private void Activate()
    {
        if (!(sc.northNeighbor is null) && sc.northNeighbor.IsAccessible(Direction.North, label))
        {
            Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
            newBall.Initialize(label, Direction.North, sc, transform.position, strength, color);
        }
        if (!(sc.southNeighbor is null) && sc.southNeighbor.IsAccessible(Direction.South, label))
        {
            Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
            newBall.Initialize(label, Direction.South, sc, transform.position, strength, color);
        }
        if (!(sc.eastNeighbor is null) && sc.eastNeighbor.IsAccessible(Direction.East, label))
        {
            Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
            newBall.Initialize(label, Direction.East, sc, transform.position, strength, color);
        }
        if (!(sc.westNeighbor is null) && sc.westNeighbor.IsAccessible(Direction.West, label))
        {
            Ball newBall = Instantiate(ballPrefab).GetComponent<Ball>();
            newBall.Initialize(label, Direction.West, sc, transform.position, strength, color);
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
