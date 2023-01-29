using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicList : MonoBehaviour
{
    public ArrayTestingTablet tabletArrayTesting;
    AudioClip[] audioClipsHolder;
    float[] frequencies;
    int index = 0;

    ArrayTesting transfer;
    public void setArray(ArrayTesting arrayTesting)
    {
        transfer = arrayTesting;
        audioClipsHolder = arrayTesting.currentSongList();
        index = arrayTesting.currentListIndex();
        StartTransfer();
    }

    
    public AudioClip[] currentList()
    {
        return audioClipsHolder;
    }
    public int currentIndex()
    {
        return index;
    }

    public float[] currentFrequencies()
    {
        return frequencies;
    }

    public void StartTransfer()
    {
        tabletArrayTesting.transferTablet(currentList(), currentIndex(), currentFrequencies());
    }

}
