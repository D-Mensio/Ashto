using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPiece : InteractiblePiece
{
    public override bool IsAccessible(Direction direction)
    {
        if (isRotating)
            return false;

        switch (direction)
        {
            case Direction.North:
            case Direction.South:
                return phase == 0 || phase == 2;
            case Direction.East:
            case Direction.West:
                return phase == 1 || phase == 3;
            default:
                return false;
        }
    }

    public override Piece GetNextPiece(Direction inDirection, out Direction outDirection)
    {
        //check if inDirection is valid
        if (((phase == 0 || phase == 2) && (inDirection == Direction.South || inDirection == Direction.North)) || ((phase == 1 || phase == 3) && (inDirection == Direction.East || inDirection == Direction.West)))
            outDirection = inDirection;
        else
            outDirection = Direction.Error;

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
