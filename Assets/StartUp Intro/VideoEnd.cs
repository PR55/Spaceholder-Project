using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEnd : MonoBehaviour
{

    public SceneTransition sceneTransition;
    public int sceneIndex = 1;

    public VideoPlayer videoPlayer;
    double videoLength;

    public float waitTime = 2f;

    float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        videoLength = videoPlayer.length - 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(videoPlayer.time >= videoLength)
        {
            if(timer < waitTime)
            {
                timer += Time.fixedDeltaTime;
            }
            else
            {
                sceneTransition.SceneManage(sceneIndex);
            }
        }
    }
}
