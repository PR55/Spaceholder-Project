using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flighttesting : MonoBehaviour
{
    

    public Vector3 force;

    public Vector3 collisionForce;

    public Vector3 thrustForce;
    
    private Rigidbody bodyMain;

    public bool testingRangesActive;
    public bool forceapplied;

    bool hitGorund = false;

    public GameObject thrustController;
    public GameObject torqueController;

    [Header("Testing")]
    [SerializeField, Range(-30f, 30f)]
    private float moveActivator = 0f;
    [SerializeField, Range(-45f, 45f)]
    private float pitchActivator = 0f;
    [SerializeField, Range(-45f, 45f)]
    private float yawActivator = 0f;
    public float moveBuffer = 10f;
    public float pitchBuffer = 15f;
    public float yawBuffer = 15f;
    public float maximumPitch = 45f;
    public float maximumYaw = 45f;

    float maximumMove = 30f;

    


    [Header("GameObject Thrust")]
    public GameObject thrustObject;
    public float moveObjectBuffer = 30f;
    public float maximumMoveObject = 30f;

    Vector3 thrustObjectHome;

    [Header("Player Limiters")]
    public float zRotationLocalLimit = 5.1f;
    public float xRotationLocalLimit = 6.25f;

    bool moving;

    // Start is called before the first frame update
    void Start()
    {
        thrustObjectHome = thrustObject.transform.localPosition;

        forceapplied = false;
        bodyMain = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        
        if (forceapplied)
        {
            bodyMain.AddForce(force, ForceMode.Impulse);

            if(bodyMain.useGravity)
            {
                bodyMain.useGravity = false;
            }
        }
        else
        {
            if (!bodyMain.useGravity)
            {
                bodyMain.useGravity = true;
            }
        }
        if (forceapplied)
        {
            if (hitGorund)
            {
                return;
            }
            else
            {
                if (testingRangesActive)
                {
                    if (moveActivator > moveBuffer)
                    {
                        bodyMain.AddRelativeForce(new Vector3((moveActivator / maximumMove) * thrustForce.x, (moveActivator / maximumMove) * thrustForce.y, (moveActivator / maximumMove) * thrustForce.z), ForceMode.Acceleration);
                    }
                    else if (moveActivator < -moveBuffer)
                    {
                        bodyMain.AddRelativeForce(new Vector3((moveActivator / maximumMove) * thrustForce.x, thrustForce.y, (moveActivator / maximumMove) * thrustForce.z), ForceMode.Acceleration);
                    }
                    if (pitchActivator > pitchBuffer)
                    {
                        this.gameObject.transform.Rotate(new Vector3((pitchActivator / maximumPitch) * (100 * Time.deltaTime), 0, 0), Space.Self);
                    }
                    else if (pitchActivator < -pitchBuffer)
                    {
                        this.gameObject.transform.Rotate(new Vector3((pitchActivator / maximumPitch) * (100 * Time.deltaTime), 0, 0), Space.Self);
                    }
                    if (yawActivator > yawBuffer)
                    {
                        this.gameObject.transform.Rotate(new Vector3(0, (yawActivator / maximumYaw) * (100 * Time.deltaTime), 0), Space.Self);
                    }
                    else if (yawActivator < -yawBuffer)
                    {
                        this.gameObject.transform.Rotate(new Vector3(0, (yawActivator / maximumYaw) * (100 * Time.deltaTime), 0), Space.Self);
                    }


                }
                else
                {
                    if (thrustObject.transform.localPosition.z - thrustObjectHome.z > moveObjectBuffer && thrustObject.transform.localPosition.z - thrustObjectHome.z< maximumMoveObject)
                    {
                        bodyMain.AddRelativeForce(new Vector3(((thrustObject.transform.localPosition.x - thrustObjectHome.x) / maximumMoveObject) * thrustForce.x, ((thrustObject.transform.localPosition.x - thrustObjectHome.x) / maximumMoveObject) * thrustForce.y, ((thrustObject.transform.localPosition.z - thrustObjectHome.z)/maximumMoveObject) * thrustForce.z), ForceMode.Acceleration);
                    }
                    else if (thrustObject.transform.localPosition.z - thrustObjectHome.z < -moveObjectBuffer && thrustObject.transform.localPosition.z - thrustObjectHome.z > -maximumMoveObject)
                    {
                        bodyMain.AddRelativeForce(new Vector3(((thrustObject.transform.localPosition.x - thrustObjectHome.x) / maximumMoveObject) * thrustForce.x, thrustForce.y, ((thrustObject.transform.localPosition.z - thrustObjectHome.z) / maximumMoveObject) * thrustForce.z), ForceMode.Acceleration);
                    }
                    if (torqueController.transform.localEulerAngles.x > 180)
                    {
                        if (torqueController.transform.localEulerAngles.x - 360 < -pitchBuffer && torqueController.transform.localEulerAngles.x - 360 > -maximumPitch)
                            bodyMain.AddRelativeTorque(new Vector3(((torqueController.transform.localEulerAngles.x - 360) / maximumPitch) * (100 * Time.deltaTime), 0, 0), ForceMode.Acceleration );
                            
                    }
                    else if (torqueController.transform.localEulerAngles.x < 180)
                    {
                        if (torqueController.transform.localEulerAngles.x > pitchBuffer && torqueController.transform.localEulerAngles.x < maximumPitch)
                            bodyMain.AddRelativeTorque(new Vector3(((torqueController.transform.localEulerAngles.x) / maximumPitch) * (100 * Time.deltaTime), 0, 0), ForceMode.Acceleration);
                    }
                    
                    if (torqueController.transform.localEulerAngles.z > 180)
                    {
                        if (torqueController.transform.localEulerAngles.z - 360 < -yawBuffer && torqueController.transform.localEulerAngles.z - 360 > -maximumYaw)
                            bodyMain.AddRelativeTorque(new Vector3(0, ((torqueController.transform.localEulerAngles.z - 360) / -maximumYaw) * (100 * Time.deltaTime), 0), ForceMode.Acceleration);
                        
                    }
                    else if (torqueController.transform.localEulerAngles.z < 180)
                    {
                        if (torqueController.transform.localEulerAngles.z > yawBuffer && torqueController.transform.localEulerAngles.z < maximumYaw)
                            bodyMain.AddRelativeTorque(new Vector3(0, ((torqueController.transform.localEulerAngles.z) / -maximumYaw) * (100 * Time.deltaTime), 0), ForceMode.Acceleration);
                    }
                }

            }

        }

    }

    public void FlightActivator(AudioSource confirm)
    {
        if (forceapplied)
        {
            forceapplied = false;

            confirm.pitch = .8f;
            confirm.Play();
            
        }
        else
        {
            forceapplied = true;
            confirm.pitch = 1.2f;
            confirm.Play();

        }

        

    }

    public bool CanMove()
    {

        if (transform.eulerAngles.z > 180)
        {
            if (transform.eulerAngles.z - 360 > -zRotationLocalLimit)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else if(transform.eulerAngles.z < 180)
        {
            if(transform.eulerAngles.z > zRotationLocalLimit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (transform.eulerAngles.x > 180)
        {
            if (transform.eulerAngles.x - 360 > -xRotationLocalLimit)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else if (transform.eulerAngles.x < 180)
        {
            if (transform.eulerAngles.z > xRotationLocalLimit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (thrustObject.transform.localPosition.z - thrustObjectHome.z > 0f)
        {
            
                return true;
            
            
        }
        else
        {
            return false;
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
