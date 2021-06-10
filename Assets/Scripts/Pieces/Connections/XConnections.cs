using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XConnections : PieceConnections
{
    private RotatePiece rotatePiece;

    private void Awake()
    {
        rotatePiece = GetComponent<RotatePiece>();
    }

    public override bool IsAccessible(Direction direction, string label)
    {
        return !rotatePiece.isRotating;
    }

    public override Direction GetNextDirection(Direction inDirection)
    {
        return inDirection;
    }
}
