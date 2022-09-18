using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GravityBodySpaceShip : MonoBehaviour
{
    public Rigidbody rigidbody;
    GravityAttractorSpaceShip Planet;

    private void Awake()
    {
        Planet = GameObject.FindGameObjectWithTag("planet").GetComponent<GravityAttractorSpaceShip>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

    }

    private void FixedUpdate()
    {
        Planet.Attract(this.transform);
    }

}
