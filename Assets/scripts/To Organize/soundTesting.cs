using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundTesting : MonoBehaviour
{
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        
    }
}
