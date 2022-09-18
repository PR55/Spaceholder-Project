using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VRMap
{
    public VRRig access;
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset * (Time.deltaTime * access.turnSmoothness));
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
    public void HeadMap()
    {
        rigTarget.position = vrTarget.TransformPoint(Vector3.Lerp(access.transform.forward, Vector3.ProjectOnPlane(access.headConstraint.up, Vector3.up).normalized, Time.deltaTime * access.turnSmoothness));
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;



    public Transform headConstraint;
    public Vector3 headBodyOffset;

    [Range(0,1)]
    public float turnSmoothness;

    // Start is called before the first frame update
    void Start()
    {
        if(headBodyOffset == Vector3.zero)
        headBodyOffset = transform.position - headConstraint.position;
        
        head.Map();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized, Time.fixedDeltaTime * turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
    }

    
}
    
