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

    private float elapsedTime = 0;

    private int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        sceneChecked = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!sceneChecked)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                DontDestroyOnLoad(this.gameObject);
                sceneChecked = true;
            }
            else if (SceneManager.GetActiveScene().buildIndex == reportScreenIndex)
            {
                levelRunning = false;
                if(reportComplete)
                {
                    SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
                    sceneChecked = true;
                }
            }
            else if (SceneManager.GetActiveScene().buildIndex == mainLevelIndex)
            {
                sceneChecked = true;
                levelRunning = true;
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

}
