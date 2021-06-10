using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceConnections : PieceConnections
{

    private string label;

    private void Start()
    {
        label = GetComponent<SourceSpawn>().label;
    }

    public override bool IsAccessible(Direction direction, string label)
    {
        return false;
    }

    public override bool IsConnected(Direction direction, string label, bool fromTarget = false, bool fromSource = false)
    {
        return (!fromSource && (!fromTarget || this.label == label));
    }

    public override Direction GetNextDirection(Direction inDirection)
    {
        return inDirection;
    }
}
