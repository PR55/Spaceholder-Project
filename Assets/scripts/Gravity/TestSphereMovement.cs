using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphereMovement : MonoBehaviour
{

    Rigidbody rigidbody;

    public float walkspeed = 5f;

    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical"));
        Vector3 targetMoveAmount = moveDir * walkspeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveAmount)*Time.fixedDeltaTime);
    }
}
