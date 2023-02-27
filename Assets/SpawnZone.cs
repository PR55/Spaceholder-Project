using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class SpawnZone : XRGrabInteractable
{
    public IgnoreColliders rifleCollider;
    public GameObject nullMag;
    GameObject magPrefab;

    bool magSpawned;

    private void Start()
    {
        magSpawned = false;
    }

    public void ForceEjection(XRBaseInteractor interactor)
    {
        //forces the current object in interactor to be unselected and to start falling on button press
        XRBaseInteractorExtension.ForceDeselect(interactor);
        if(magPrefab == null)
        {
            magPrefab = nullMag;
        }
        GameObject newMag;
        if (!magSpawned)
        {
            newMag = SpawnMag();
            rifleCollider.ignoreNew(newMag.GetComponent<XRGrabInteractable>().colliders);
            magSpawned = true;
        }
        

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

    public void leaveZone()
    {
        magSpawned = false;
    }

}
