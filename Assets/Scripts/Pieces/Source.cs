using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour, Piece
{
    public int strength;
    public string label;

    public bool IsAccessible(Direction direction)
    {
        return false;
    }

    public Piece GetNextPiece(string label, Direction inDirection, out Direction outDirection)
    {
        outDirection = Direction.North;
        return null;
    }
}
