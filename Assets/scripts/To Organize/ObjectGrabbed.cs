using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbed : MonoBehaviour
{
    bool isGrabbed = false;


    public void Grabbed()
    {
        isGrabbed = true;
    }

    public void Dropped()
    {
        isGrabbed = false;
    }

    public bool grabState()
    {
        return isGrabbed;
    }


}
