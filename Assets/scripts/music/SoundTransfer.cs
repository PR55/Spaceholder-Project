using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTransfer : MonoBehaviour
{
    public StereoManager stereo;
    public AudioSource confirmSound;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tablet")
        {
            if(stereo.returnCurrent() != null)
            {
                other.GetComponent<musicList>().setArray(stereo.returnCurrent());
                Debug.Log("Transferred");
                confirmSound.Play();
            }
            
        }
    }
}
