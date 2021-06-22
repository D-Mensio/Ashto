using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component managing the connections to neighbouring pieces for targets
public class TargetConnections : PieceConnections
{
    private string label;

    private void Start()
    {
        label = GetComponent<TargetActivation>().label;
    }

    //Checks if the piece is currently accessible for a certain label
    public override bool IsAccessible(Direction direction, string label)
    {
        return label == this.label;
    }

    //Checks if the piece is connected from a certain direction (only false if neighbouring piece is either a target, or a source with a different label)
    public override bool IsConnected(Direction direction, string label, bool fromTarget = false, bool fromSource = false)
    {
        return (!fromTarget && (!fromSource || this.label == label));
    }

    //Returns the exit direction for a ball entering from inDirection
    public override Direction GetNextDirection(Direction inDirection)
    {
        return Direction.Stop;
    }
}
