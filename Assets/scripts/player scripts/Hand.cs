using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : XRDirectInteractor
{
    private MeshRenderer meshRenderer = null;

    protected override void Awake()
    {
        base.Awake();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }
    public void SetVisibility(bool value)
    {
        meshRenderer.enabled = value;
    }
}
