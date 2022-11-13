using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelForward : MonoBehaviour
{
    public Transform parentForward;


    private void FixedUpdate()
    {
            transform.LookAt(transform.forward);
    }
}
