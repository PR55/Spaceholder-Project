using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateUI : MonoBehaviour
{
    public GameObject HeadUI;

    private void Start()
    {
        HeadUI.SetActive(false);
    }

    public void UIActive()
    {
        HeadUI.SetActive(true);
    }

    public void UiNotActive()
    {
        HeadUI.SetActive(false);
    }
}
