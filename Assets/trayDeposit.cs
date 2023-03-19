using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trayDeposit : MonoBehaviour
{
    public vendingMachine vendingMachine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Magazine>() != null && other.gameObject.GetComponentInParent<ObjectGrabbed>() != null)
        {
            if (!other.gameObject.GetComponentInParent<ObjectGrabbed>().grabState())
            {
                vendingMachine.depositAmmo(other.gameObject.GetComponentInParent<Magazine>().ammoValue);
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Magazine>() != null && other.gameObject.GetComponentInParent<ObjectGrabbed>() != null)
        {
            if (!other.gameObject.GetComponentInParent<ObjectGrabbed>().grabState())
            {
                vendingMachine.depositAmmo(other.gameObject.GetComponentInParent<Magazine>().ammoValue);
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
    }
}
