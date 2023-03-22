using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunReport : MonoBehaviour
{
    public int mainLevelIndex = 1;
    public int reportScreenIndex = 2;

    bool sceneChecked = false;

    bool reportComplete = false;

    bool levelRunning = false;

    TimeSpan timePlaying;

    //[SerializeField]
    private float elapsedTime = 0;
    //[SerializeField]
    private int totalScore = 0;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        sceneChecked = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!sceneChecked)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                ResetVals();
                sceneChecked = true;
                Resources.UnloadUnusedAssets();
            }
            else if (SceneManager.GetActiveScene().buildIndex == reportScreenIndex && sceneChecked == false)
            {
                levelRunning = false;
                if(reportComplete)
                {
                    sceneChecked = true;
                }
                Resources.UnloadUnusedAssets();
            }
            else if (SceneManager.GetActiveScene().buildIndex == mainLevelIndex && sceneChecked == false)
            {
                ResetVals();
                sceneChecked = true;
                levelRunning = true;
                Resources.UnloadUnusedAssets();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                sceneChecked = true;
            }
        }

        if(levelRunning)
        {
            elapsedTime += Time.fixedDeltaTime;
        }

    }

    

    public void checkScene()
    {
        if(sceneChecked)
        {
            Debug.Log("Checking Scene");
            sceneChecked = false;
        }
    }

    public bool reportProgress()
    {
        return reportComplete;
    }

    public TimeSpan runTime()
    {
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        return timePlaying;
    }
    
    public void addToScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
    }

    public int scoreRetrieve()
    {
        return totalScore;
    }

    public void reportCompleted()
    {
        reportComplete = true;
    }

    public void ResetVals()
    {
        reportComplete = false;
        totalScore = 0;
        timePlaying = TimeSpan.Zero;
        elapsedTime = 0;
    }

}
