using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IgnoreColliders : MonoBehaviour
{
    public XRBaseInteractable collidersToIgnore;

    public XRBaseInteractable mainBody;
    private void Awake()
    {
        if(FindObjectOfType<PlayerStats>().spawnAmmoZone != null)
        FindObjectOfType<PlayerStats>().spawnAmmoZone.GetComponent<SpawnZone>().rifleCollider = this;

        if (collidersToIgnore != null && mainBody != null)
        {
            foreach (Collider collider in collidersToIgnore.colliders)
            {
                foreach (Collider col2 in mainBody.colliders)
                {

                    Physics.IgnoreCollision(col2, collider);
                }
            }
        }
            
    }

    public void ignoreNew(List<Collider> collider)
    {
        foreach (Collider collidera in collider)
        {
            foreach (Collider col2 in mainBody.colliders)
            {
                Physics.IgnoreCollision(col2, collidera);
            }
        }
    }
}
