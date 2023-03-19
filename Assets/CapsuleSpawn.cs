using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleSpawn : MonoBehaviour
{
    public GameObject toSpawn;

    public MeshRenderer meshRenderer;

    public ObjectGrabbed objectGrabbed;

    public Vector3 spawnOffset;

    public Quaternion spawnRotation;

    Material material;

    bool isActivated = false;
    private void Start()
    {
        material = meshRenderer.material;
    }

    public void stateChange()
    {
        isActivated = !isActivated;
        if(isActivated)
        {
            material.SetInt("_UseEmission",1);
        }
        else if (!isActivated)
        {
            material.SetInt("_UseEmission", 0);
        }
    }

    void spawnItem()
    {
        Instantiate(toSpawn, transform.position + spawnOffset, spawnRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isActivated && objectGrabbed)
        {
            if (!objectGrabbed.grabState())
            {
                spawnItem();
                Destroy(this.gameObject);
                Debug.Log("Ow");
            }
        }
    }

}
