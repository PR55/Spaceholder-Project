using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HingeMovement : MonoBehaviour
{
    public GameObject doorBone;
    public float closeSpeed = 1f;
    [Header("Limits")]
    public float closeAngle =35.13f;
    public float openAngle = -35;
    //public float ColliderSmoothing = 5;

    public Material openMaterial;
    public Material closeMaterial;
    public Material betweenMaterial;

    public Image interiorScreen;
    public Image exteriorScreen;
    

    Vector3 homePosition;

    Quaternion startRotation;
    BoxCollider box;
    public bool open;
    public bool close;
    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        open = false;
        moving = false;
        close = true;

        homePosition = doorBone.transform.localPosition;

        startRotation = doorBone.transform.localRotation;

        
        
    }

    // Update is called once per frame
    void Update()
    {
       


        if (moving)
        {
            if(open)
            {

                if(doorBone.transform.localEulerAngles.x > 180)
                {
                    if (doorBone.transform.localEulerAngles.x - 360 > openAngle)
                    {
                        doorBone.transform.Rotate(new Vector3(-closeSpeed * Time.deltaTime, 0, 0), Space.Self);
                    }
                    else if (doorBone.transform.localEulerAngles.x -360 < openAngle)
                    {
                        exteriorScreen.material = closeMaterial;
                        interiorScreen.material = closeMaterial;
                        moving = false;
                    }
                }
                else
                {
                    if(doorBone.transform.localEulerAngles.x > openAngle)
                    {
                        doorBone.transform.Rotate(new Vector3(-closeSpeed * Time.deltaTime, 0, 0), Space.Self);
                    }
                    else if (doorBone.transform.localEulerAngles.x < openAngle)
                    {
                        exteriorScreen.material = closeMaterial;
                        interiorScreen.material = closeMaterial;
                        moving = false;
                    }
                }
            }
            else if (close)
            {

                if (doorBone.transform.localEulerAngles.x > 180)
                {
                    if (doorBone.transform.localEulerAngles.x - 360 < closeAngle)
                    {
                        doorBone.transform.Rotate(new Vector3(closeSpeed * Time.deltaTime, 0, 0), Space.Self);
                    }
                    else if (doorBone.transform.localEulerAngles.x - 360 > closeAngle)
                    {
                        exteriorScreen.material = openMaterial;
                        interiorScreen.material = openMaterial;
                        moving = false;
                    }
                }
                else
                {
                    if (doorBone.transform.localEulerAngles.x < closeAngle)
                    {
                        doorBone.transform.Rotate(new Vector3(closeSpeed * Time.deltaTime, 0, 0), Space.Self);
                    }
                    else if (doorBone.transform.localEulerAngles.x  > closeAngle)
                    {
                        exteriorScreen.material = openMaterial;
                        interiorScreen.material = openMaterial;
                        moving = false;
                    }
                }

            }
        }
        if(Input.GetKey(KeyCode.U))
        {
            if(close)
            {
                OpenDoor();
            }
            else if(open)
            {
                CloseDoor();
            }    
        }
    }

    public void OpenDoor()
    {
        if (!moving && close)
        {
            exteriorScreen.material = betweenMaterial;
            interiorScreen.material = betweenMaterial;
            open = true;
            moving = true;
            close = false;
        }
    }

    public void CloseDoor()
    {
        if(!moving)
        {
            exteriorScreen.material = betweenMaterial;
            interiorScreen.material = betweenMaterial;
            close = true;
            moving = true;
            open = false;
        }
    }

    public void DoorOperation()
    {
        if (close)
        {
            OpenDoor();
        }
        else if (open)
        {
            CloseDoor();
        }
    }

}
