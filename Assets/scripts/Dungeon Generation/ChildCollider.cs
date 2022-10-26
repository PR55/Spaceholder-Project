using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollider : MonoBehaviour
{
    bool collide = false;

    HallwayGeneration hallwayGeneration;

    private void Awake()
    {
        hallwayGeneration = FindObjectOfType<HallwayGeneration>();
    }

    public bool collideCheck()
    {
        return collide;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null && (other.transform.parent.gameObject.tag == "HallX" || other.transform.parent.gameObject.tag == "HallZ") && hallwayGeneration.hallwayColelction().Contains(other.transform.parent.gameObject) && other.transform.parent.gameObject != this.transform.parent.gameObject)
        {
            collide = true;
        }
        else
        {
            collide= false;
        }
    }
}
