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
        foreach (Collider collider in collidersToIgnore.colliders)
        {
            foreach (Collider col2 in mainBody.colliders)
            {
                Physics.IgnoreCollision(col2, collider);
            }
        }
    }
}
