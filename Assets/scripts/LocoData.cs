using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocoData
{
    public int moveState;
    public int turnState;

    public LocoData(LocomotionSettings locomotionSettings)
    {
        if (locomotionSettings.moveStateCheck())
            moveState = 0;
        else
            moveState = 1;
        if (locomotionSettings.turnStateCheck())
            turnState = 0;
        else
            turnState = 1;
    }
}
