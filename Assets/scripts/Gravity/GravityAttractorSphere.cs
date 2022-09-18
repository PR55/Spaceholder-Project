using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractorSphere : MonoBehaviour
{
    public float gravity = -5f;

    public void Attract(Transform body)
    {
        //Vector3 targetDir = (body.position - transform.position).normalized;
        //Vector3 bodyUp = body.up;

        //body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        //body.gameObject.GetComponent<Rigidbody>().AddForce(targetDir * gravity,ForceMode.Impulse);


        if(body.position.y - transform.position.y > 0 )
        {
            body.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, gravity,0), ForceMode.Impulse);
        }
        else if (body.position.y - transform.position.y < 0)
        {
            body.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, -gravity, 0), ForceMode.Impulse);
        }
        if (body.position.x - transform.position.x > 0)
        {
            body.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(gravity, 0,0), ForceMode.Impulse);
        }
        else if (body.position.x - transform.position.x < 0)
        {
            body.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(-gravity, 0, 0), ForceMode.Impulse);
        }
        if (body.position.z - transform.position.z > 0)
        {
            body.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0,gravity), ForceMode.Impulse);
        }
        else if (body.position.z - transform.position.z < 0)
        {
            body.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, -gravity), ForceMode.Impulse);
        }

    }
}
