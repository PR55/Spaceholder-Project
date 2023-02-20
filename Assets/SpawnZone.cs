using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class SpawnZone : XRGrabInteractable
{
    public GameObject nullMag;
    GameObject magPrefab;

    public void ForceEjection(XRBaseInteractor interactor)
    {
        //forces the current object in interactor to be unselected and to start falling on button press
        XRBaseInteractorExtension.ForceDeselect(interactor);
        if(magPrefab == null)
        {
            magPrefab = nullMag;
        }
        GameObject newMag = SpawnMag();

    }

    public GameObject SpawnMag()
    {
        return Instantiate(magPrefab, this.transform.position, Quaternion.identity);
    }

    public void eject()
    {
        ForceEjection(this.selectingInteractor);
    }

    public void setMag(GameObject magject)
    {
        magPrefab = magject;
    }

}
