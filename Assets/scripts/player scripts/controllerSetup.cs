using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerSetup : MonoBehaviour
{
    public bool enable = false;

    CharacterController controller;

    void Start()
    {
        if(enable)
        {
            controller = GetComponent<CharacterController>();
            controller.detectCollisions = false;
        }


    }
}
