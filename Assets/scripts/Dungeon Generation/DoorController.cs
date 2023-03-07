using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public pointProperties childPoint;

    Animator doorAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        doorAnimation = GetComponent<Animator>();

        childPoint = GetComponentInChildren<pointProperties>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            doorAnimation.SetBool("NearPlayer", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            doorAnimation.SetBool("NearPlayer", false);
        }
    }

}
