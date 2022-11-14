using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ArrayTesting : MonoBehaviour
{


    int audioPlayerIndex = 0;
    public GameObject[] audiosources;
    public AudioSource[] audioPlayer;
    

    [SerializeField]
    private List<AudioClip> Playlist;
    [SerializeField, Range(10,22000)]
    private float[] songFrequencyAmounts;
    [SerializeField, Range(1, 10)]
    private float[] songFrequencyQAmounts;

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
        audiosources = GameObject.FindGameObjectsWithTag("SpeakersShip");
        audioPlayer = new AudioSource[audiosources.Length];
        foreach (GameObject a in audiosources)
        {

            audioPlayer[audioPlayerIndex] = a.GetComponent<AudioSource>();
            audioPlayerIndex++;
        }

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
                foreach (AudioSource Player in audioPlayer)
                {
                    Player.clip = playlistReal[index];
                    Player.gameObject.GetComponent<AudioHighPassFilter>().cutoffFrequency = songFrequencyAmounts[index];
                    Player.gameObject.GetComponent<AudioHighPassFilter>().highpassResonanceQ = songFrequencyQAmounts[index];
                    Player.Play();
                }
                audioTimer = playlistReal[index].length + Time.deltaTime;

            }
            else
            {
                index++;
                foreach (AudioSource Player in audioPlayer)
                {
                    Player.clip = playlistReal[index];
                    Player.gameObject.GetComponent<AudioHighPassFilter>().cutoffFrequency = songFrequencyAmounts[index];
                    Player.gameObject.GetComponent<AudioHighPassFilter>().highpassResonanceQ = songFrequencyQAmounts[index];
                    Player.Play();
                }
                audioTimer = playlistReal[index].length + Time.deltaTime;
            }

            nextSong = false;

        }
        
        if(previousSong || restartSong)
        {
            if(restartSong)
            {
                foreach (AudioSource Player in audioPlayer)
                {
                    Player.Stop();
                    Player.time = 0f;
                    Player.Play();
                    audioTimer = playlistReal[index].length + Time.deltaTime;
                }
                restartSong = false;

            }
            else
            {
                if (index <= 0)
                {
                    index = maxDiscSize;
                    foreach (AudioSource Player in audioPlayer)
                    {
                        Player.clip = playlistReal[index];
                        Player.gameObject.GetComponent<AudioHighPassFilter>().cutoffFrequency = songFrequencyAmounts[index];
                        Player.gameObject.GetComponent<AudioHighPassFilter>().highpassResonanceQ = songFrequencyQAmounts[index];
                        Player.Play();
                    }
                    audioTimer = playlistReal[index].length + Time.deltaTime;
                }
                else
                {
                    index--;
                    foreach (AudioSource Player in audioPlayer)
                    {
                        Player.clip = playlistReal[index];
                        Player.gameObject.GetComponent<AudioHighPassFilter>().cutoffFrequency = songFrequencyAmounts[index];
                        Player.gameObject.GetComponent<AudioHighPassFilter>().highpassResonanceQ = songFrequencyQAmounts[index];
                        Player.Play();
                    }
                    audioTimer = playlistReal[index].length + Time.deltaTime;
                }
                previousSong = false;
            }
            
        }

        if(playMusic)
        {
            if(songIsPaused = true)
            {
                foreach (AudioSource Player in audioPlayer)
                {
                    Player.Play();
                }
                songIsPaused = false;
            }
            playMusic = false;
        }

        if(stopMusic)
        {
            foreach (AudioSource Player in audioPlayer)
            {
                Player.Pause();
            }
            
            songIsPaused = true;
            stopMusic = false;
        }

        if(!songIsPaused)
        {
            audioTimer -= Time.deltaTime;
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
        foreach (AudioSource Player in audioPlayer)
        {
            Player.clip = playlistReal[index];
            Player.gameObject.GetComponent<AudioHighPassFilter>().cutoffFrequency = songFrequencyAmounts[index];
            Player.gameObject.GetComponent<AudioHighPassFilter>().highpassResonanceQ = songFrequencyQAmounts[index];

        }
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
    public float[] frequencyAmounts()
    {
        return songFrequencyAmounts;
    }
    

}
