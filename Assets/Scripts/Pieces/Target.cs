using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, Piece
{
    public string label;

    public bool IsAccessible(Direction direction)
    {
        return true;
    }

    public Piece GetNextPiece(string label, Direction inDirection, out Direction outDirection)
    {
        outDirection = Direction.North;
        return null;
    }
}
