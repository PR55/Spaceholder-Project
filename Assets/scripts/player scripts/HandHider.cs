using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandHider : MonoBehaviour
{
    private MeshRenderer meshRenderer = null;
    private XRDirectInteractor interactor = null;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        interactor = GetComponent<XRDirectInteractor>();

        interactor.onHoverEntered.AddListener(Hide);
        interactor.onHoverExited.AddListener(Show);
    }

    private void OnDestroy()
    {
        interactor.onHoverEntered.RemoveListener(Hide);
        interactor.onHoverExited.RemoveListener(Show);
    }


    private void Show(XRBaseInteractable interactable)
    {
        meshRenderer.enabled = true;
    }

    private void Hide(XRBaseInteractable interactable)
    {
        meshRenderer.enabled = false;
    }

}
