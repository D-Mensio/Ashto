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


//Common base class for the management of the connections to neighbouring pieces of each type of piece
public abstract class PieceConnections : MonoBehaviour
{
    //Neighbouring pieces' PieceConnections component
    public PieceConnections northNeighbor;
    public PieceConnections southNeighbor;
    public PieceConnections westNeighbor;
    public PieceConnections eastNeighbor;

    //Checks if the piece is currently accessible from a certain direction/label (false if a piece is currently rotating)
    public abstract bool IsAccessible(Direction direction, string label);

    //Checks if the piece is connected from a certain direction/label
    public abstract bool IsConnected(Direction direction, string label, bool fromTarget = false, bool fromSource = false);

    //Returns the exit direction for a ball entering from inDirection
    public abstract Direction GetNextDirection(Direction inDirection);

    //Returns the PieceConnections component of the neighbouring piece in a given direction
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
