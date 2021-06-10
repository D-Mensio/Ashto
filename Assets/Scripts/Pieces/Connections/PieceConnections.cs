using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North,
    East,
    South,
    West,
    Stop,
    Error
}

public abstract class PieceConnections : MonoBehaviour
{

    public PieceConnections northNeighbor;
    public PieceConnections southNeighbor;
    public PieceConnections westNeighbor;
    public PieceConnections eastNeighbor;

    public abstract bool IsAccessible(Direction direction, string label);

    public abstract Direction GetNextDirection(Direction inDirection);

    public PieceConnections GetConnection(Direction direction)
    {
        return direction switch
        {
            Direction.North => northNeighbor,
            Direction.South => southNeighbor,
            Direction.East => eastNeighbor,
            Direction.West => westNeighbor,
            _ => null,
        };
    }
}
