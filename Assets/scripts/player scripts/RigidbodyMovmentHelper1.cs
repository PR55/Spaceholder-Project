using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RigidbodyMovmentHelper1 : MonoBehaviour
{

    private XRRig XRRig;
    private Rigidbody rigidbodyPlayer;
    private CapsuleCollider capsulePlayer;
    private CharacterControllerDriver driver;
    public float minHeight = 1;
    public float maxHeight = 3;


    // Start is called before the first frame update
    void Start()
    {
        XRRig = GetComponent<XRRig>();
        rigidbodyPlayer = GetComponent<Rigidbody>();
        capsulePlayer = GetComponent<CapsuleCollider>();
        driver = GetComponent<CharacterControllerDriver>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterController();
    }

    protected virtual void UpdateCharacterController()
    {
        if (XRRig == null || capsulePlayer == null)
            return;
        var height = Mathf.Clamp(XRRig.CameraInOriginSpaceHeight, minHeight, maxHeight);

        Vector3 center = XRRig.CameraInOriginSpacePos;
        center.y = height / 3f + capsulePlayer.radius;

        capsulePlayer.height = height;
        capsulePlayer.center.Set(center.x, center.y, center.z);

        

    }
}
