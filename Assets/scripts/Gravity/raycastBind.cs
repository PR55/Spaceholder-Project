using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastBind : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.localPosition, -transform.up);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "Planet")
            transform.position = hit.point;
        }
    }
}
