using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceConnections : PieceConnections
{
    public override bool IsAccessible(Direction direction)
    {
        return false;
    }

    public override Direction GetNextDirection(Direction inDirection)
    {
        return inDirection;
    }
}
