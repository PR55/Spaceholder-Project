using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alignLever : MonoBehaviour
{
    public bool alignPosition = false;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(alignPosition)
        transform.position = obj.transform.position;
        
        transform.rotation = obj.transform.rotation;
    }
}
