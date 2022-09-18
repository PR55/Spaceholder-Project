using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SetVideoOnOff : MonoBehaviour
{
    public GameObject COOLBUGFACTSS;
    public bool videoplay = false;
    public Material standardmaterial;
    public Material videomaterial;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = standardmaterial;
    }

    public void PlayVideo()
    {
        if(!videoplay)
        {
            GetComponent<MeshRenderer>().material = videomaterial;
            COOLBUGFACTSS.GetComponent<VideoPlayer>().Play();
            videoplay = true;
        }
        else if(videoplay)
        {
            GetComponent<MeshRenderer>().material = standardmaterial;
            COOLBUGFACTSS.GetComponent<VideoPlayer>().Stop();
            videoplay = false;
        }
    }
}
