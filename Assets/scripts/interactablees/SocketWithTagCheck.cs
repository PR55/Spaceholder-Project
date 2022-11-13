using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public static class XRBaseInteractorExtension
{
    /// <summary>
    /// Force deselect any selected interactable for given interactor.
    ///
    /// This is an extension method for <c>XRBaseInteractor</c>.
    /// </summary>
    /// <param name="interactor">Interactor that has some interactable selected</param>
    public static void ForceDeselect(this XRBaseInteractor interactor)
    {
        interactor.interactionManager.CancelInteractorSelection(interactor);
        //Assert.IsFalse(interactor.isSelectActive);
    }
}

public class SocketWithTagCheck : XRSocketInteractor
{
    bool canGrab = true;
    public string targetTag = string.Empty;

    bool keepGrabbed = false;

    XRBaseInteractable xR;

    private void FixedUpdate()
    {
        if(keepGrabbed && xR != null)
        {
            if(xR.gameObject.GetComponent<ObjectGrabbed>() != null)
            {
                xR.gameObject.GetComponent<ObjectGrabbed>().Grabbed();
            }
        }
    }

    public override bool CanHover(XRBaseInteractable interactable)
    {
        if(GrabState(interactable))
        interactableAssign(interactable);
        return base.CanHover(interactable) && MatchUsingTag(interactable) && GrabState(interactable);
        
    }

    public void interactableAssign(XRBaseInteractable interactable)
    {
        xR = interactable;
    }

    public void grabOff()
    {
        xR = null;
        keepGrabbed = false;
    }

    public void grabOn()
    {
        keepGrabbed = true;
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        
            return base.CanSelect(interactable) && MatchUsingTag(interactable) && GrabState(interactable);
        

    }
    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        
        if (targetTag != string.Empty)
            return interactable.CompareTag(targetTag);
        else
            return true;
       
    }

    private bool GrabState(XRBaseInteractable interactable)
    {
        if (interactable.gameObject.GetComponent<ObjectGrabbed>() != null)
            return interactable.gameObject.GetComponent<ObjectGrabbed>().grabState();
        else
            return true;
    }
    
    public void ForceEjection(XRBaseInteractor interactor)
    {
        //forces the current object in interactor to be unselected and to start falling on button press
        XRBaseInteractorExtension.ForceDeselect(interactor);
    }
    public void tabletPresent()
    {
        canGrab = false;
    }
    public void tabletGone()
    {
        canGrab = true;
    }
}
