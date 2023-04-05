using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overallLook : MonoBehaviour
{
    public bool targetFound ;

    public Transform target;

    Quaternion restRotation;

    // Start is called before the first frame update
    void Start()
    {
        targetFound = false;
        restRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetFound)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }
    public void targetLocated(Transform Dest)
    {
        target = Dest;
        targetFound = true;
    }

    public Transform checkTarg()
    {
        return target;
    }

}
