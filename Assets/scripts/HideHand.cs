using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHand : MonoBehaviour
{
    public GameObject hand;

    public void hideHand()
    {
        hand.SetActive(false);
    }

    public void showHand()
    {
        hand.SetActive(true);
    }
}
