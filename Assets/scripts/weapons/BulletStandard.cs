using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStandard : MonoBehaviour
{
    public float projectileSpeed = 5f;

    public float lifetime = 4;

    Rigidbody rigidbody;

    Transform firePont;

    SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifetime);

    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(transform.forward * projectileSpeed); // controls speed on spawn
                                                                 // substitue with the firepoint to do tracking
                                                                 // could also used with the negative forward of object based on where a raycast hits for tracking, where hit point is rotated 180 on the y axis
    }

    public Rigidbody bulletRigid()
    {
        return rigidbody;
    }

    public void ColliderstoIgnore(MeshCollider[] colliders = null)
    {
        if(colliders!=null)
        {
            foreach (MeshCollider collider in colliders)
            {
                Physics.IgnoreCollision(sphereCollider, collider, true);
            }
        }
    }

    public void firePoint(Transform spawn)
    {
        firePont = spawn;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

}
