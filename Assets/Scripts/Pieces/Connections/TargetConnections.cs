using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetConnections : PieceConnections
{
    private string label;

    private void Start()
    {
        label = GetComponent<TargetActivation>().label;
    }

    public override bool IsAccessible(Direction direction, string label)
    {
        return label == this.label;
    }

    public override bool IsConnected(Direction direction, string label, bool fromTarget = false, bool fromSource = false)
    {
        return (!fromTarget && (!fromSource || this.label == label));
    }

    public override Direction GetNextDirection(Direction inDirection)
    {
        return Direction.Stop;
    }
}
