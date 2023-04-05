using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    public Vector3 offsetFloat;

    public Vector3 rotateAngleSpeed = new Vector3(0,0.5f,0);

    public float hoverVariation = .15f;

    public GameObject raritySpherePrefab;

    public float floatSpeed;

    public bool disableFloat = false;

    Rigidbody rigidbody;
    GameObject curSphere = null;

    bool floatUp = true;

    Vector3 midHover;

    bool floatNow;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void floatPossible()
    {
        disableFloat = false;
    }

    public void floatDisable()
    {
        disableFloat = true;
    }

    void startFloating(GameObject floor)
    {
        rigidbody.isKinematic = true;
        transform.rotation = Quaternion.identity;
        midHover = new Vector3(transform.position.x, floor.transform.position.y + offsetFloat.y, transform.position.z);
        transform.position = midHover;
        if(curSphere == null)
            curSphere = Instantiate(raritySpherePrefab,midHover,Quaternion.identity);
        floatUp = true;
        floatNow = true;
    }

    void stopFloating()
    {
        rigidbody.isKinematic = false;
        if (curSphere != null)
        {
            Destroy(curSphere);
            curSphere = null;
        }
        floatNow = false;
    }

    private void Update()
    {
        if (floatNow)
        {
            transform.Rotate(rotateAngleSpeed, Space.World);

            float initialY = midHover.y;
            float newY = initialY + (Mathf.Sin(Time.time * floatSpeed) * hoverVariation);

            curSphere.transform.position = midHover;

            transform.position = new Vector3(midHover.x, newY, midHover.z);

        }
        if(disableFloat && rigidbody.isKinematic == true)
        {
            stopFloating();
            
        }
        else if(rigidbody.isKinematic == true && floatNow == false)
        {
            rigidbody.isKinematic = false;
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            if(!disableFloat)
            {
                startFloating(collision.gameObject);
            }
        }
    }
}
