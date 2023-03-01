using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialVoice : MonoBehaviour
{
    public AudioSource audioSource;

    bool hasPlayed = false;

    public void playTutorial()
    {
        if(!hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}
