using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LastSceneSourceSpawn : SourceSpawn
{
    public List<string> worlds;
    private int i;

    private void Start()
    {
        sc = GetComponent<SourceConnections>();
        GetComponent<ActiveBorder>().borderMaterial.color = color;
        StartCoroutine(SpawnBalls());
    }

    private bool Activate()
    {
        if (!(sc.northNeighbor is null) && sc.northNeighbor.IsAccessible(Direction.North, label))
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.GetComponentInChildren<TextMeshPro>().text = worlds[i];
            ball.GetComponent<Ball>().Initialize(label, Direction.North, sc, transform.position, strength, color);
            return false;
        }
        else
            return true;
    }

    private IEnumerator SpawnBalls()
    {
        bool reset;
        i = 0;
        while (true)
        {
            reset = Activate();
            if (reset || i >= worlds.Count - 1)
                i = 0;
            else
                i++;
            yield return new WaitForSeconds(1f);
        }
    }
}
