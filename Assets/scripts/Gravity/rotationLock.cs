using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationLock : MonoBehaviour
{
    public Quaternion startRotation;
    // Start is called before the first frame update
    void Awake()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = startRotation;
    }
}
