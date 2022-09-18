using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitShip : MonoBehaviour
{
    public float smoothingBuffer;

    public GameObject parentTo;

    public Vector3 scaleChange;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.transform.parent != null)
            {
                var previousScale = transform.lossyScale;
                Quaternion temp = transform.rotation * transform.localRotation;
                Vector3 tempPos = transform.position;
                other.gameObject.transform.SetParent(null, false);
                other.gameObject.transform.position = tempPos;
                other.gameObject.transform.rotation = temp;
                other.gameObject.transform.localScale = previousScale;
            }
            
        }
    }
}
