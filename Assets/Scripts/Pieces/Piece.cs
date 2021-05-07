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

public interface Piece
{
    //Returns true if the piece is accessible from direction direction
    public bool IsAccessible(Direction direction);

    public Piece GetNextPiece(Direction inDirection, out Direction outDirection);
}
