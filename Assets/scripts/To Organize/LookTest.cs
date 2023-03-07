using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTest : MonoBehaviour
{

    public Transform parent;

    public Transform target;

    public Vector3 lookOffset;

    public bool targetFound;

    public Transform weaponPosition;

    public Transform restPoint;

    public float speedMove = 5f;

    bool notReadied;

    // Start is called before the first frame update
    void Start()
    {
        notReadied = true;
        targetFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetFound)
        {
            if(weaponPosition != null && notReadied)
            {
                if (parent.localPosition == weaponPosition.localPosition && parent.rotation == weaponPosition.rotation)
                {
                    notReadied = false;
                }
                else
                {
                    parent.localPosition = Vector3.Lerp(parent.localPosition, weaponPosition.localPosition, Time.deltaTime* speedMove);
                    parent.rotation = Quaternion.Lerp(parent.rotation, weaponPosition.rotation, Time.deltaTime * speedMove);
                }
                    
            }
            else
            {
                Transform targ = target;
                targ.position = target.position + lookOffset;
                targ.rotation = target.rotation;
                targ.localScale = target.localScale;
                parent.LookAt(targ);
            }

            
        }
        else
        {
            if(restPoint != null)
            {
                parent.localPosition = Vector3.Lerp(parent.localPosition, restPoint.localPosition,Time.deltaTime * speedMove);
                parent.rotation = Quaternion.Slerp(parent.rotation, restPoint.rotation, Time.deltaTime*speedMove);
                notReadied = true;
            }
            
        }
    }
}
