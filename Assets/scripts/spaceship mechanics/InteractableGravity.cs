using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGravity : MonoBehaviour
{
    public enum gravityDirectionLocal
    {
        transformUp,
        transformForward

    }

    public gravityDirectionLocal gravityDirectionLocalChoice;

    public Gravity gravity;
    private Rigidbody rb;

    public float rotationSpeed = 20f;


    bool isHeld = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent != null)
        {
            if(transform.parent.GetComponent<Gravity>() != null )
            {
                gravity = transform.parent.GetComponent<Gravity>();
                rb.useGravity = false;
            }
        }
        else if (transform.parent == null)
        {
            gravity = null;
            rb.useGravity = true;
        }
            
        if(gravity != null && !isHeld)
        {
            Vector3 gravityUp = Vector3.zero;

            if (gravity.fixedDirection)
            {
                gravityUp = gravity.transform.forward;
            }
            else
            {
                gravityUp = (transform.position - transform.parent.transform.position).normalized;
            }
            Vector3 localUp = Vector3.zero;
            if (gravityDirectionLocalChoice == gravityDirectionLocal.transformUp)
            {
                localUp = transform.up;
            }
            else if(gravityDirectionLocalChoice == gravityDirectionLocal.transformForward)
            {
                localUp = transform.forward;
            }
            

           Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

            //if (gravityDirectionLocalChoice == gravityDirectionLocal.transformUp)
            //{
            //    transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.fixedDeltaTime);
            //}
            //else if (gravityDirectionLocalChoice == gravityDirectionLocal.transformForward)
            //{
            //    transform.forward = Vector3.Lerp(transform.forward, gravityUp, rotationSpeed * Time.fixedDeltaTime);
            //}
            
            
            rb.AddForce((-gravityUp * gravity.gravity) * rb.mass);
        }
    }

    public void beingHeld()
    {
        isHeld = true;
    }

    public void releasedHold()
    {
        isHeld = false;
        if (GameObject.FindGameObjectWithTag("Player").transform.parent != null)
            transform.parent = GameObject.FindGameObjectWithTag("Player").transform.parent;
        else
            transform.parent = null;
    }


}
