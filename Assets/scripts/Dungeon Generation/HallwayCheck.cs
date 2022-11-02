using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayCheck : MonoBehaviour
{
    bool collide = false;

    public bool collideCheck()
    {
        return collide;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HallX" || other.gameObject.tag == "HallZ")
        {
            collide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HallX" || other.gameObject.tag == "HallZ")
        {
            collide = false;
        }
    }
}
