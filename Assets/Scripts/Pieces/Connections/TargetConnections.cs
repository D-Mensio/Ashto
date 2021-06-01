using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetConnections : PieceConnections
{
    public override bool IsAccessible(Direction direction)
    {
        return true;
    }

    public override Direction GetNextDirection(Direction inDirection)
    {
        return Direction.Stop;
    }
}
