using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the connections to neighbouring pieces for X-type (cross) pieces. Requires a RotatePiece component
public class XConnections : PieceConnections
{
    //RotatePiece component of the piece. All phases are equal, but the component is needed to check for rotation
    private RotatePiece rotatePiece;

    private void Awake()
    {
        rotatePiece = GetComponent<RotatePiece>();
    }

    //Checks if the piece is currently accessible from a certain direction (false if a piece is currently rotating)
    public override bool IsAccessible(Direction direction, string label)
    {
        return !rotatePiece.isRotating;
    }

    //Checks if the piece is connected from a certain direction (always true)
    public override bool IsConnected(Direction direction, string label, bool fromTargetOrSource = false, bool fromSource = false)
    {
        return true;
    }

    //Returns the exit direction for a ball entering from inDirection
    public override Direction GetNextDirection(Direction inDirection)
    {
        return inDirection;
    }
}
