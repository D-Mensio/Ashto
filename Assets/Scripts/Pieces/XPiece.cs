using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPiece : InteractiblePiece
{
    public override bool IsAccessible(Direction direction)
    {
        return !isRotating;
    }

    public override Piece GetNextPiece(Direction inDirection, out Direction outDirection)
    {
        outDirection = inDirection;

        //return neighbor in outDirection
        return outDirection switch
        {
            Direction.North => northNeighbor,
            Direction.South => southNeighbor,
            Direction.East => eastNeighbor,
            Direction.West => westNeighbor,
            _ => null,
        };
    }
}
