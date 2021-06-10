using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LConnections : PieceConnections
{

    private RotatePiece rotatePiece;

    private void Awake()
    {
        rotatePiece = GetComponent<RotatePiece>();
    }

    public override bool IsAccessible(Direction direction, string label)
    {
        //check if piece is currently rotating
        if (rotatePiece.isRotating)
            return false;

        return IsConnected(direction, label);
    }

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
