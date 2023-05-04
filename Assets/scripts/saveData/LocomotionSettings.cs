using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class LocomotionSettings : MonoBehaviour
{
    [Header("All Scene Variables")]
    public ActionBasedContinuousMoveProvider moveProvider;//choice 1-2
    public ActionBasedContinuousTurnProvider continuousTurnProvider; // choice 2-2
    public ActionBasedSnapTurnProvider snapTurnProvider;// choice 2-1
    public TeleportationProvider teleportationProvider; // choice 1-1

    public GameObject teleportProvider;

    [Header("Main Menu Variables")]
    public Text curStateLoc;

    public Text curStateTurn;
    public Image locoControls;
    public Image teleControls;
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

    public void teleportCheck()
    {
        LocoData data = SaveSystem.LoadLocomotion();
        if(data.moveState == 0)
        {
            teleportProvider.SetActive(true);
        }
        else
        {
            if(teleportProvider.activeInHierarchy)
            {
                teleportProvider.SetActive(false);
            }
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
                teleControls.enabled = true;
                locoControls.enabled = false;

                curStateLoc.text = "Teleport";
            }
            else
            {
                teleportationProvider.enabled = false;
                teleportProvider.SetActive(false);
                moveProvider.enabled = true;
                teleControls.enabled = false;
                locoControls.enabled = true;
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
