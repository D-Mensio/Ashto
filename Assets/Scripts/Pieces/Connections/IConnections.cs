using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the connections to neighbouring pieces for I-type (straight) pieces. Requires a RotatePiece component
public class IConnections : PieceConnections
{
    //RotatePiece component of the piece. Phase 0/2 is vertical, phases 1/3 are horizontal
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
            case Direction.South:
                return rotatePiece.phase == 0 || rotatePiece.phase == 2;
            case Direction.East:
            case Direction.West:
                return rotatePiece.phase == 1 || rotatePiece.phase == 3;
            default:
                return false;
        }
    }

    //Returns the exit direction for a ball entering from inDirection
    public override Direction GetNextDirection(Direction inDirection)
    {
        return inDirection; //exit direction is the same of the entering direction
    }
}
