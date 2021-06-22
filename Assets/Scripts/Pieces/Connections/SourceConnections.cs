using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the connections to neighbouring pieces for sources
public class SourceConnections : PieceConnections
{

    private string label;

    private void Start()
    {
        label = GetComponent<SourceSpawn>().label;
    }

    //Checks if the piece is currently accessible from a certain direction (always false)
    public override bool IsAccessible(Direction direction, string label)
    {
        return false;
    }

    //Checks if the piece is connected from a certain direction (only false if neighbouring piece is either a source, or a target with a different label)
    public override bool IsConnected(Direction direction, string label, bool fromTarget = false, bool fromSource = false)
    {
        return (!fromSource && (!fromTarget || this.label == label));
    }

    //Returns the exit direction for a ball entering from inDirection
    public override Direction GetNextDirection(Direction inDirection)
    {
        return inDirection;
    }
}
