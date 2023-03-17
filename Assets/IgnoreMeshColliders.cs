using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreMeshColliders : MonoBehaviour
{
    public Collider[] ignoreColliders;
    Collider selfCollider;
    // Start is called before the first frame update
    void Start()
    {
        selfCollider = this.GetComponent<Collider>();
        foreach (Collider item in ignoreColliders)
        {
            Physics.IgnoreCollision(selfCollider, item);
        }
    }
}
