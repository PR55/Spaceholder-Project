using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBounds : MonoBehaviour
{

    public HallwayGeneration hallwayGeneration;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.transform.position = Vector3.zero; 
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
