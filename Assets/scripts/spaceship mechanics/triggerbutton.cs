using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerbutton : MonoBehaviour
{


    public HingeMovement hingeMovement;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player Hand"))
        {
            hingeMovement.DoorOperation();
        }
    }

}
