using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kiddyCageHandler : MonoBehaviour
{
    public bool onOff = false;

    public Transform xrOrigin;
    
    GameObject[] supports;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider[] transforms = GetComponentsInChildren<BoxCollider>();
        supports= new GameObject[transforms.Length];
        foreach(BoxCollider trans in transforms)
        {
            supports[i] = trans.gameObject;
            i++;
        }
        isActive(onOff);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isActive(bool choice)
    {
        if(choice == true)
        {
            foreach(GameObject sup in supports)
            {
                sup.SetActive(choice);
            }
            this.transform.position = new Vector3(xrOrigin.position.x, this.transform.position.y, xrOrigin.position.z);
        }
        else if(choice == false)
        {
            foreach (GameObject sup in supports)
            {
                sup.SetActive(choice);
            }
        }
    }

}
