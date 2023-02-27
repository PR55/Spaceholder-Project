using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DestinationSetCustom : AIDestinationSetter
{
    public overallLook overallLooker;
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (target != overallLooker.checkTarg())
            overallLooker.targetLocated(target);
    }
}
