using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingforAudioTest : MonoBehaviour
{
    public Vector3 force;

    public Vector3 collisionForce;

    public Vector3 thrustForce;
    public Transform forcePoint;

    public Vector3 thrustForceLeft;
    public Transform forcePointLeft;

    public Vector3 thrustForceRight;
    public Transform forcePointRight;

    private Rigidbody bodyMain;

    public bool forceapplied;

    bool hitGorund = false;

    // Start is called before the first frame update
    void Start()
    {
        forceapplied = false;
        bodyMain = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (forceapplied)
        {
            bodyMain.AddForce(force, ForceMode.Impulse);
        }
        else
        {

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
        

        if(Input.GetKeyDown(KeyCode.F))
        {
            if (forceapplied)
            {
                forceapplied = false;
            }
            else
            {
                forceapplied = true;

            }
                
        }

        
            if (hitGorund)
            {
                return;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    this.gameObject.transform.Translate(new Vector3(0, 0, thrustForce.z * Time.deltaTime));
                }

                if (Input.GetKey(KeyCode.S))
                {
                    this.gameObject.transform.Translate(new Vector3(0, 0, -1 * thrustForce.z * Time.deltaTime));
                }
                if (Input.GetKey(KeyCode.A))
                {

                    this.gameObject.transform.Rotate(new Vector3(0, -100 * Time.deltaTime, 0), Space.World);
                }

                if (Input.GetKey(KeyCode.D))
                {

                    this.gameObject.transform.Rotate(new Vector3(0, 100 * Time.deltaTime, 0), Space.World);
                }

                
            
            
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(forceapplied)
            {
                hitGorund = true;
                bodyMain.AddForceAtPosition(collisionForce, collision.collider.transform.position);

            }
            
            
        
        }
        else
        {

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(forceapplied)
            {
                hitGorund = false;
            }
            
        }
        else
        {

        }
    }
}
