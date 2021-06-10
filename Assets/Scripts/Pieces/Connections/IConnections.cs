using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IConnections : PieceConnections
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

        switch (direction)
        {
            case Direction.North:
            case Direction.South:
                return rotatePiece.phase == 0 || rotatePiece.phase == 2;
            case Direction.East:
            case Direction.West:
                return rotatePiece.phase == 1 || rotatePiece.phase == 3;
            default:
                return false;
        }
    }

    public override Direction GetNextDirection(Direction inDirection)
    {
        return inDirection;
    }
}
