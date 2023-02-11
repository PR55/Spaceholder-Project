using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionSettings : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider moveProvider;//choice 1-2
    public ActionBasedContinuousTurnProvider continuousTurnProvider; // choice 2-2
    public ActionBasedSnapTurnProvider snapTurnProvider;// choice 2-1
    public TeleportationProvider teleportationProvider; // choice 1-1

    public bool moveStateCheck()
    {
        if (teleportationProvider.enabled == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool turnStateCheck()
    {
        if(snapTurnProvider.enabled == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void loadProviders(int moveState, int turnState)
    {
        if (moveState == 0)
        {
            teleportationProvider.enabled = true;
            moveProvider.enabled = false;
        }
        else
        {
            teleportationProvider.enabled = false;
            moveProvider.enabled = true;
        }
        if (turnState == 0)
        {
            snapTurnProvider.enabled = true;
            continuousTurnProvider.enabled = false;
        }
        else
        {
            snapTurnProvider.enabled = false;
            continuousTurnProvider.enabled = true;
        }
    }

}
