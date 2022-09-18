using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LockPosition : MonoBehaviour
{
    public float moveNow;
    public SetVideoOnOff access;
    public float upperLimit = .15f;
    public float lowerLimit = 0;
    float lastmovenow;
    Vector3 startPosition;
    Vector3 movingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        movingPosition = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(moveNow > lastmovenow || moveNow < lastmovenow)
        {
            float movePosition = moveNow - lastmovenow;
            movingPosition.x += movePosition;
            transform.position = movingPosition;
            lastmovenow = moveNow;
            access.COOLBUGFACTSS.GetComponent<VideoPlayer>().SetDirectAudioVolume(0,lastmovenow);
        }
        else if(moveNow == 0)
        {
            transform.position = startPosition;
            lastmovenow = 0;
        }
        else if(moveNow > upperLimit )
        {
            moveNow = upperLimit;
            float movePosition = moveNow - lastmovenow;
            movingPosition.x += movePosition;
            transform.position = movingPosition;
            lastmovenow = moveNow;
            if (!access.videoplay)
                access.PlayVideo();
        }
        else if (moveNow > lowerLimit)
        {
            moveNow = lowerLimit;
            float movePosition = moveNow - lastmovenow;
            movingPosition.x += movePosition;
            transform.position = movingPosition;
            lastmovenow = moveNow;
            if (access.videoplay)
                access.PlayVideo();
        }

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            moveNow += 0.01f;
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            moveNow -= 0.01f;
        }

    }

}
