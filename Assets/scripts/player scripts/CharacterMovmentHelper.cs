using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterMovmentHelper : MonoBehaviour
{
    public Gravity gravity;

    private XRRig XRRig;
    private CharacterController CharacterController;
    private CharacterControllerDriver driver;

    public float rotationSpeed = 5f;


    Vector3 motion;
    Vector3 m_VerticalVelocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        XRRig = GetComponent<XRRig>();
        CharacterController = GetComponent<CharacterController>();
        driver = GetComponent<CharacterControllerDriver>();
    }

    private void FixedUpdate()
    {
        if (transform.parent == null)
        {
            if(gravity != null)
            gravity = null;
            m_VerticalVelocity += Physics.gravity * Time.fixedDeltaTime;
        }
            
        else
        {
            if(gravity == null)
            gravity = transform.parent.GetComponent<Gravity>();


            Vector3 gravityUp = Vector3.zero;

            if(gravity.fixedDirection)
            {
                gravityUp = gravity.transform.forward;
            }
            else
            {
                gravityUp = (transform.position - transform.parent.transform.position).normalized;
            }    

            Vector3 localUp = transform.up;

            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

            transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed*Time.fixedDeltaTime);


            if (transform.parent.eulerAngles.x > 180 || transform.parent.eulerAngles.z > 180)
                m_VerticalVelocity += Vector3.Scale(Physics.gravity, (gravityUp * gravity.gravity)) * Time.fixedDeltaTime;
            else
                m_VerticalVelocity += Vector3.Scale(Physics.gravity, (gravityUp * gravity.gravity)) * Time.fixedDeltaTime;


            
        }
            
        motion += m_VerticalVelocity * Time.fixedDeltaTime;
        CharacterController.Move(motion);
        transform.localRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterController();

        

    }

    protected virtual void UpdateCharacterController()
    {
        if (XRRig == null || CharacterController == null)
            return;
        var height = Mathf.Clamp(XRRig.cameraInRigSpaceHeight, driver.minHeight, driver.maxHeight);

        Vector3 center = XRRig.cameraInRigSpacePos;
        center.y = height / 3f + CharacterController.skinWidth;

        CharacterController.height = height;
        CharacterController.center = center;
    }
}
