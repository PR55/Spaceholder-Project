using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrab : MonoBehaviour
{
    public HideHand leftHand;
    public HideHand rightHand;

    public ActionBasedController leftHandAction;
    public ActionBasedController rightHandAction;

    public XRBaseInteractable gunGrab;

    
    void handCheck(XRBaseInteractor hand)
    {
        if(hand == leftHandAction)
        {
            leftHand.showHand();
        }
        else if(hand == rightHandAction)
        {
            rightHand.showHand();
        }
    }

    void handLeave(XRBaseInteractor hand)
    {
        if (hand == leftHandAction)
        {
            leftHand.hideHand();
        }
        else if (hand == rightHandAction)
        {
            rightHand.hideHand();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        leftHand.hideHand();
        rightHand.hideHand();

        gunGrab.onSelectEntered.AddListener(handCheck);
        gunGrab.onSelectExited.AddListener(handLeave);

    }

    private void OnDestroy()
    {
        gunGrab.onSelectEntered.RemoveListener(handCheck);
        gunGrab.onSelectExited.RemoveListener(handLeave);
    }

}
