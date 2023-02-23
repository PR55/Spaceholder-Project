using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrayTesting : MonoBehaviour
{


    int audioPlayerIndex = 0;
    public AudioSource audioPlayer;
    

    [SerializeField]
    private List<AudioClip> Playlist;

    int index = 0;

    private int maxDiscSize = 27;

    private AudioClip[] playlistReal;

    private float audioTimer = 0;

    bool songIsPaused;

    bool playMusic = false;

    bool stopMusic = false;

    bool nextSong = false;
    bool previousSong = false;
    bool restartSong = false;


    // Start is called before the first frame update
    void Awake()
    {
        SetSongs();
        songIsPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextSong || audioTimer <= 0 && !songIsPaused)
        {
            if(index >= maxDiscSize)
            {
                index = 0;
                audioPlayer.clip = playlistReal[index];
                audioPlayer.Play();
                audioTimer = playlistReal[index].length + Time.deltaTime;

            }
            else
            {
                index++;
                audioPlayer.clip = playlistReal[index];
                audioPlayer.Play();
                audioTimer = playlistReal[index].length + Time.deltaTime;
            }

            nextSong = false;

        }
        
        if(previousSong || restartSong)
        {

            if(restartSong)
            {
                audioPlayer.Stop();
                audioPlayer.time = 0f;
                audioPlayer.Play();
                audioTimer = playlistReal[index].length + Time.deltaTime;
                restartSong = false;

            }
            else
            {
                audioPlayer.Stop();
                if (index <= 0)
                {
                    index = playlistReal.Length-1;
                    audioPlayer.clip = playlistReal[index];
                    audioPlayer.Play();
                    audioTimer = playlistReal[index].length + Time.deltaTime;

                }
                else
                {
                    index--;
                    audioPlayer.clip = playlistReal[index];
                    audioPlayer.Play();
                    audioTimer = playlistReal[index].length + Time.deltaTime;
                }
                previousSong = false;
            }
            
        }

        if(playMusic)
        {
            if(songIsPaused == true)
            {
                audioPlayer.Play();
                songIsPaused = false;
            }
            playMusic = false;
        }

        if(stopMusic)
        {
            audioPlayer.Pause();
            
            songIsPaused = true;
            stopMusic = false;
        }

        if(!songIsPaused)
        {
            audioTimer -= Time.deltaTime;
        }
    }

    public void changeState()
    {
        if(songIsPaused)
        {
            PlayMusic(); 
        }
        else if (!songIsPaused)
        {
            StopMusic();
        }
    }

    public void PlayMusic()
    {
        playMusic = true;
        stopMusic = false;
    }

    public void StopMusic()
    {
        playMusic = false;
        stopMusic = true;
    }

    public void NextSong()
    {
        nextSong = true;
    }

    public void PreviousSong()
    {
        previousSong = true;
    }

    public void RestartSong()
    {
        restartSong = true;
    }

    public void SetSongs()
    {
        playlistReal = null;
        index = 0;
        if (Playlist.Count > maxDiscSize+1)
        {
            Playlist.RemoveRange(maxDiscSize + 1, Playlist.Count - maxDiscSize + 1);
        }
            
        playlistReal = Playlist.ToArray();
        maxDiscSize = playlistReal.Length - 1;
        audioPlayer.clip = playlistReal[index];
        audioTimer = playlistReal[index].length + Time.deltaTime;
    }
    public AudioClip[] currentSongList()
    {
        return playlistReal;
    }
    public int currentListIndex()
    {
        return index;
    }
    

}
