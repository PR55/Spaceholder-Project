using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class LocomotionSettings : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider moveProvider;//choice 1-2
    public ActionBasedContinuousTurnProvider continuousTurnProvider; // choice 2-2
    public ActionBasedSnapTurnProvider snapTurnProvider;// choice 2-1
    public TeleportationProvider teleportationProvider; // choice 1-1

    public GameObject teleportProvider;

    public Text curStateLoc;

    public Text curStateTurn;

    public bool isPlayScene;

    private void Awake()
    {
        if(SaveSystem.LoadLocomotion() != null)
        {
            LocoData data = SaveSystem.LoadLocomotion();
            loadProviders(data.moveState, data.turnState);
        }
        else
        {
            SaveSettings();
            LocoData data = SaveSystem.LoadLocomotion();
            loadProviders(data.moveState, data.turnState);
        }
        
    }

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
        if(isPlayScene)
        {
            if (moveState == 0)
            {
                teleportationProvider.enabled = true;
                teleportProvider.SetActive(true);
                moveProvider.enabled = false;
            }
            else
            {
                teleportationProvider.enabled = false;
                teleportProvider.SetActive(false);
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
        else
        {
            if (moveState == 0)
            {
                teleportationProvider.enabled = true;
                teleportProvider.SetActive(true);
                moveProvider.enabled = false;

                curStateLoc.text = "Teleport";
            }
            else
            {
                teleportationProvider.enabled = false;
                teleportProvider.SetActive(false);
                moveProvider.enabled = true;
                curStateLoc.text = "Locomotion";
            }
            if (turnState == 0)
            {
                snapTurnProvider.enabled = true;
                continuousTurnProvider.enabled = false;

                curStateTurn.text = "Snap Turn";
            }
            else
            {
                snapTurnProvider.enabled = false;
                continuousTurnProvider.enabled = true;
                curStateTurn.text = "Smooth Turn";
            }
        }
    }

    public void ChangeActiveLoc()
    {
        if (teleportationProvider.enabled == true)
        {
            teleportationProvider.enabled = false;
            teleportProvider.SetActive(false);
            moveProvider.enabled = true;

            curStateLoc.text = "Locomotion";
        }
        else
        {
            teleportationProvider.enabled = true;
            teleportProvider.SetActive(true);
            moveProvider.enabled = false;
            curStateLoc.text = "Teleport";
        }
    }

    public void ChangeActiveTurn()
    {
        if (snapTurnProvider.enabled == true)
        {
            snapTurnProvider.enabled = false;
            continuousTurnProvider.enabled = true;
            curStateTurn.text = "Smooth Turn";
        }
        else
        {
            snapTurnProvider.enabled = true;
            continuousTurnProvider.enabled = false;
            curStateTurn.text = "Snap Turn";
        }
    }


    public void SaveSettings()
    {
        SaveSystem.SaveComfort(this);
    }


}
