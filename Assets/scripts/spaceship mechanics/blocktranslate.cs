using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class blocktranslate : MonoBehaviour
{
    public float moveSpeed = 0.1f;

    Vector3 home;

    public float maxDistance = .75f;

    private Vector3 differencePosition;

    public float timeUntilReturnHome = 3f;

    float countdown;

    bool grabbed;

    public XRGrabInteractable grabInteractable;

    Quaternion homeRotation;

    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
        homeRotation = transform.rotation;
        differencePosition = transform.position - home;
        countdown = timeUntilReturnHome + Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        differencePosition = transform.position - home;
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0), Space.Self);
            countdown = timeUntilReturnHome + Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0), Space.Self);
            countdown = timeUntilReturnHome + Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime), Space.Self);
            countdown = timeUntilReturnHome + Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0, 0, -moveSpeed * Time.deltaTime), Space.Self);
            countdown = timeUntilReturnHome + Time.deltaTime;
        }

        if(differencePosition.x > maxDistance)
        {
            transform.position= home + new Vector3(maxDistance,0,0);
        }
        else if (differencePosition.x < -maxDistance)
        {
            transform.position = home - new Vector3(maxDistance, 0, 0);
        }
        if (differencePosition.z > maxDistance)
        {
            transform.position = home + new Vector3 (0, 0, maxDistance);
        }
        else if (differencePosition.z < -maxDistance)
        {
            transform.position = home - new Vector3(0, 0, maxDistance);
        }

        if (differencePosition.y > maxDistance)
        {
            transform.position = home + new Vector3(0, maxDistance, 0);
        }
        else if (differencePosition.y < -maxDistance)
        {
            transform.position = home - new Vector3(0, maxDistance, 0);
        }

        if (countdown <= 0 && !grabbed)
        {
            transform.position = home;
            transform.rotation = homeRotation;
        }
        else if (grabbed)
        {
            
            if (differencePosition.x > maxDistance)
            {
                countdown = timeUntilReturnHome + Time.deltaTime;
            }
            else if (differencePosition.x < -maxDistance)
            {
                countdown = timeUntilReturnHome + Time.deltaTime;
            }
            if (differencePosition.z > maxDistance)
            {
                countdown = timeUntilReturnHome + Time.deltaTime;
            }
            else if (differencePosition.z < -maxDistance)
            {
                countdown = timeUntilReturnHome + Time.deltaTime;
            }
        }
        else
        {
            if(!grabbed)
            countdown -= Time.deltaTime;
            Debug.Log("Time Left: " + countdown.ToString("F0"));
        }

    }

    public void IsGrabbed()
    {
        grabbed = true;
    }
    public void IsNotGrabbed()
    {
        grabbed = false;
    }
}
