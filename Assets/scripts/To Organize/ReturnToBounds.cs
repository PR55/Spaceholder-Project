using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBounds : MonoBehaviour
{

    public Vector3 respawnOffset;

    public List<GameObject> objectsToWatchFor;

    public RoomGeneration roomGeneration;

    private void OnTriggerEnter(Collider other)
    {
        if(objectsToWatchFor.Contains(other.gameObject))
        {
            Destroy(other.gameObject);
        }
    }
}
