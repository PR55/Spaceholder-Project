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

    public override bool CanHover(XRBaseInteractable interactable)
    {
      
            return base.CanHover(interactable) && MatchUsingTag(interactable);
        
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        
            return base.CanSelect(interactable) && MatchUsingTag(interactable);
        

    }

    private bool MatchUsingTag(XRBaseInteractable interactable)
    {
        
            return interactable.CompareTag(targetTag);
       
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
