using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHand : MonoBehaviour
{
    public SkinnedMeshRenderer hand;

    public void hideHand()
    {
        hand.enabled = false;
    }

    public void showHand()
    {
        hand.enabled = true;
    }
}
