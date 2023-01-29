using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsControls : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetVolumeEffects(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetVolumeAmbiance(float volume)
    {
        audioMixer.SetFloat("ambianceVolume", volume);
    }
}
