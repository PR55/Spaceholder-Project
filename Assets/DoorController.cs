using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public int detectionRange = 5;

    pointProperties childPoint;

    bool playerDetected = false;

    Animator doorAnimation;

    Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
        doorAnimation = GetComponent<Animator>();

        childPoint = GetComponentInChildren<pointProperties>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(playerTransform.position, this.transform.position) < detectionRange && !playerDetected)
        {
            if(childPoint.useCheck())
                playerDetected = true;
        }
        else if (Vector3.Distance(playerTransform.position, this.transform.position) > detectionRange && playerDetected)
        {
            if (childPoint.useCheck())
                playerDetected = false;
        }

        if (playerDetected && !doorAnimation.GetBool("NearPlayer"))
        {
            doorAnimation.SetBool("NearPlayer", true);
        }
        else if (!playerDetected && doorAnimation.GetBool("NearPlayer"))
        {
            doorAnimation.SetBool("NearPlayer", false);
        }
    }

}
