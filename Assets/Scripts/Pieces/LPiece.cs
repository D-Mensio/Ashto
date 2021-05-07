using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPiece : InteractiblePiece
{
    public override bool IsAccessible(Direction direction)
    {
        if (isRotating)
            return false;

        switch (direction)
        {
            case Direction.North:
                return phase == 0 || phase == 3;
            case Direction.South:
                return phase == 1 || phase == 2;
            case Direction.East:
                return phase == 2 || phase == 3;
            case Direction.West:
                return phase == 0 || phase == 1;
            default:
                return false;
        }
    }

    public override Piece GetNextPiece(Direction inDirection, out Direction outDirection)
    {
        //validate inDirection and calculate out direction based on phase and in direction
        switch (phase)
        {
            case 0 when inDirection == Direction.North:
                outDirection = Direction.East;
                break;
            case 0 when inDirection == Direction.West:
                outDirection = Direction.South;
                break;
            case 1 when inDirection == Direction.South:
                outDirection = Direction.East;
                break;
            case 1 when inDirection == Direction.West:
                outDirection = Direction.North;
                break;
            case 2 when inDirection == Direction.South:
                outDirection = Direction.West;
                break;
            case 2 when inDirection == Direction.East:
                outDirection = Direction.North;
                break;
            case 3 when inDirection == Direction.North:
                outDirection = Direction.West;
                break;
            case 3 when inDirection == Direction.East:
                outDirection = Direction.South;
                break;
            default:
                outDirection = Direction.Error;
                break;
        }

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
