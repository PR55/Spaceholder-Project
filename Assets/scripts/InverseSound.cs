using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseSound : MonoBehaviour
{
    public AudioSource effect;

    float originalStart;

    float originalEnd;

    float originalPitch;

    private void Start()
    {
        originalEnd = effect.clip.length;
        originalStart = effect.clip.length - originalEnd;

        originalPitch = effect.pitch;
    }

    // Start is called before the first frame update
    public void InverseEffect()
    {
        effect.pitch = Random.Range(1.1f, 1.2f);
        effect.Play();
    }

    public void NormalEffect()
    {
        effect.pitch = originalPitch;
        effect.Play();
    }
}
