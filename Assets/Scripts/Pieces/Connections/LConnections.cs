using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the connections to neighbouring pieces for L-type (90° corner) pieces. Requires a RotatePiece component
public class LConnections : PieceConnections
{
    //RotatePiece component of the piece. Phase 0 means the piece is connected to the south and east neighboor
    private RotatePiece rotatePiece;

    private void Awake()
    {
        rotatePiece = GetComponent<RotatePiece>();
    }

    //Checks if the piece is currently accessible from a certain direction (false if a piece is currently rotating)
    public override bool IsAccessible(Direction direction, string label)
    {
        //check if piece is currently rotating
        if (rotatePiece.isRotating)
            return false;

        return IsConnected(direction, label);
    }

    //Checks if the piece is connected from a certain direction
    public override bool IsConnected(Direction direction, string label, bool fromTarget = false, bool fromSource = false)
    {
        switch (direction)
        {
            case Direction.North:
                return rotatePiece.phase == 0 || rotatePiece.phase == 3;
            case Direction.South:
                return rotatePiece.phase == 1 || rotatePiece.phase == 2;
            case Direction.East:
                return rotatePiece.phase == 2 || rotatePiece.phase == 3;
            case Direction.West:
                return rotatePiece.phase == 0 || rotatePiece.phase == 1;
            default:
                return false;
        }
    }

    //Returns the exit direction for a ball entering from inDirection
    public override Direction GetNextDirection(Direction inDirection)
    {
        switch (rotatePiece.phase)
        {
            case 0 when inDirection == Direction.North:
                return Direction.East;
            case 0 when inDirection == Direction.West:
                return Direction.South;
            case 1 when inDirection == Direction.South:
                return Direction.East;
            case 1 when inDirection == Direction.West:
                return Direction.North;
            case 2 when inDirection == Direction.South:
                return Direction.West;
            case 2 when inDirection == Direction.East:
                return Direction.North;
            case 3 when inDirection == Direction.North:
                return Direction.West;
            case 3 when inDirection == Direction.East:
                return Direction.South;
            default:
                return Direction.Error;
        }

    }
}
