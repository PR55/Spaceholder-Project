using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControl : MonoBehaviour
{
    public float positionBuffer = .3f;

    public float rotationBuffer = 30f;

    public Transform handle;

    public float maxLimit;

    GameObject home;
    GameObject subtract;

    Vector3 homePosition;
    Quaternion homeRotation;

    bool positive = false;
    bool negative = false;

    // Start is called before the first frame update
    void Start()
    {
        homePosition = transform.position;
        homeRotation = transform.rotation;

        home = new GameObject("Home");
        subtract = new GameObject("Subtract");

        home.transform.position = handle.position;
        subtract.transform.position = handle.position;

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(CalculateLimitRotation().ToString("F3"));

        if(CalculateLimitRotation().x > rotationBuffer && CalculateLimitPosition().y > positionBuffer)
        {
            Debug.Log("Positive, " + CalculateLimitPosition().ToString("F3"));
            Debug.Log("Percantage of Power: " + CalculatePowerPercentage().ToString("F3"));
            positive = true;
            negative = false;
        }
        else if (CalculateLimitRotation().x > rotationBuffer && CalculateLimitPosition().y < -positionBuffer)
        {
            Debug.Log("Negative, " + CalculateLimitPosition().y.ToString("F3"));
            Debug.Log("Percantage of Power: " + CalculatePowerPercentage().ToString("F3"));
            positive = false;
            negative = true;
        }
        else
        {
            positive = false;
            negative = false;
        }

        subtract.transform.position = handle.position;
    }

    public Vector3 CalculatePowerPercentage()
    {
        return new Vector3((CalculateLimitPosition().x / maxLimit),(CalculateLimitPosition().y / maxLimit), (CalculateLimitPosition().z / maxLimit));
    }

    public Vector3 CalculateLimitPosition()
    {
        return new Vector3(subtract.transform.position.x - home.transform.position.x, subtract.transform.position.y - home.transform.position.y, subtract.transform.position.z - home.transform.position.z);
    }

    public Quaternion CalculateLimitRotation()
    {
        return new Quaternion(homeRotation.eulerAngles.x + -transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + -homeRotation.eulerAngles.y, transform.rotation.eulerAngles.z + homeRotation.eulerAngles.z, 1);
    }

    public bool positiveControl()
    {
        return positive;
    }
    public bool negativeControl()
    {
        return negative;
    }

}
