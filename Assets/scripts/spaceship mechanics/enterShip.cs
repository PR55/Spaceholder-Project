using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterShip : MonoBehaviour
{

    public GameObject parentTo;

    public Vector3 scaleChange;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var previousScale = collision.gameObject.GetComponent<Transform>().lossyScale;
            var previousRotation = collision.gameObject.GetComponent<Transform>().rotation;
            var previousPosition = collision.gameObject.GetComponent<Transform>().position;
            var enterPoint = collision.contacts[0];
            collision.gameObject.transform.SetParent(transform, false);
            collision.gameObject.transform.position = previousPosition;
            collision.gameObject.transform.rotation = previousRotation;
            collision.gameObject.transform.localScale = scaleChange;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.transform.parent != parentTo.transform)
            {
                var previousScale = other.gameObject.GetComponent<Transform>().lossyScale;
                var previousRotation = other.gameObject.GetComponent<Transform>().rotation;
                var previousPosition = other.gameObject.GetComponent<Transform>().position;
                var enterPoint = other.ClosestPoint(transform.position);
                other.gameObject.transform.SetParent(parentTo.transform, false);
                other.gameObject.transform.position = previousPosition;
                other.gameObject.transform.rotation = previousRotation;
                other.gameObject.transform.localScale = scaleChange;
            }
            
        }
    }
}
