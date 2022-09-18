using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GravityBody : MonoBehaviour
{
    public Rigidbody rigidbody;
    GravityAttractor Planet;

    private void Awake()
    {
        Planet = GameObject.FindGameObjectWithTag("planet").GetComponent<GravityAttractor>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

    }

    private void FixedUpdate()
    {
        Planet.Attract(this.transform);
    }

}
