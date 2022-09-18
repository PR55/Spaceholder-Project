using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StereoManager : MonoBehaviour
{
    public SocketWithTagCheck socketInteractor;
    public ArrayTesting arrayTesting;

    GameObject currentCD;

    public bool insertCD;
    
    // Start is called before the first frame update
    void Start()
    {
        insertCD = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(insertCD)
        {
            if(currentCD == null)
            {
                if(socketInteractor.selectTarget.gameObject!= null)
                {
                    currentCD = socketInteractor.selectTarget.gameObject;
                    currentCD.GetComponent<InteractableGravity>().beingHeld();
                    AssignObject(socketInteractor.selectTarget.gameObject.GetComponent<ArrayTesting>());
                }
                
            }
            
        }
        
    }

    public void Inserted()
    {
        insertCD = true;
    }

    public void Removed()
    {
        if(arrayTesting != null)
        arrayTesting.StopMusic();

        arrayTesting = null;
        currentCD = null;
        insertCD = false;
    }

    public void AssignObject(ArrayTesting CD)
    {
        arrayTesting = CD;
        arrayTesting.SetSongs();
        insertCD = false;
    }

    public void PlayMusic()
    {
        if (arrayTesting != null)
            arrayTesting.PlayMusic();
    }

    public void PauseMusic()
    {
        if (arrayTesting != null)
            arrayTesting.StopMusic();
    }

    public void restartMusic()
    {
        if (arrayTesting != null)
            arrayTesting.RestartSong();
    }

    public void NextMusic()
    {
        if (arrayTesting != null)
            arrayTesting.NextSong();
    }

    public void PreviousMusic()
    {
        if (arrayTesting != null)
            arrayTesting.PreviousSong();
    }

    public ArrayTesting returnCurrent()
    {
        return arrayTesting;
    }

}
