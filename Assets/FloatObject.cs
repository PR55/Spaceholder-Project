using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    public Vector3 offsetFloat;

    public Vector3 rotateAngle;

    public GameObject raritySphere;

    public float rotateSpeed;

    public bool disableFloat = false;

    Rigidbody rigidbody;
    bool floatNow;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        raritySphere.SetActive(false);
    }

    void startFloating()
    {
        rigidbody.isKinematic = true;
        transform.rotation = Quaternion.identity;
        transform.position = transform.position + offsetFloat;
        raritySphere.SetActive(true);
        floatNow = true;
    }

    private void Update()
    {
        if (floatNow)
        {
            transform.Rotate(rotateAngle, Space.World);
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if(!disableFloat)
            {
                startFloating();
            }
        }
    }
}
