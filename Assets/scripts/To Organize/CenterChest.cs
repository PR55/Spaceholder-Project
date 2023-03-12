using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterChest : MonoBehaviour
{

    public GameObject XRCamera;

    public float divisor = 2;

    [SerializeField]
    private bool negativeRotation = false;

    [Range(0.1f, 1)]
    public float turnSpeed;

    Quaternion rotGoal;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.transform.position.y != XRCamera.transform.position.y / divisor)
        {
            this.gameObject.transform.localPosition = new Vector3(XRCamera.transform.localPosition.x, XRCamera.transform.localPosition.y / divisor, XRCamera.transform.localPosition.z);
        }
        if (negativeRotation)
            rotGoal = new Quaternion(0, -XRCamera.transform.localRotation.y, 0, 1);
        else
            rotGoal = new Quaternion(0, XRCamera.transform.localRotation.y, 0, 1);
        this.gameObject.transform.localRotation = Quaternion.Slerp(this.gameObject.transform.localRotation, rotGoal, turnSpeed);
    }
}
