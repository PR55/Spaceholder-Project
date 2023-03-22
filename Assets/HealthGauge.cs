using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGauge : MonoBehaviour
{

    public PlayerStats playerStats;

    public GameObject[] healthPhotos = new GameObject[5];

    public int fullHealthIndex = 0;
    public int subHealthIndex = 0;
    public int midHealthIndex = 0;
    public int subCriticalHealthIndex = 0;
    public int criticalHealthIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerStats.CurrentHealthCanvas() > .8f)
        {
                healthPhotos[fullHealthIndex].SetActive(true);
                healthPhotos[subHealthIndex].SetActive(false);
                healthPhotos[midHealthIndex].SetActive(false);
                healthPhotos[subCriticalHealthIndex].SetActive(false);
                healthPhotos[criticalHealthIndex].SetActive(false);
        }
        else if(playerStats.CurrentHealthCanvas() <= .8f && playerStats.CurrentHealthCanvas() > .6f)
        {
                healthPhotos[fullHealthIndex].SetActive(false);
                healthPhotos[subHealthIndex].SetActive(true);
                healthPhotos[midHealthIndex].SetActive(false);
                healthPhotos[subCriticalHealthIndex].SetActive(false);
                healthPhotos[criticalHealthIndex].SetActive(false);
        }
        else if (playerStats.CurrentHealthCanvas() <= .6f && playerStats.CurrentHealthCanvas() > .4f)
        {
                healthPhotos[fullHealthIndex].SetActive(false);
                healthPhotos[subHealthIndex].SetActive(false);
                healthPhotos[midHealthIndex].SetActive(true);
                healthPhotos[subCriticalHealthIndex].SetActive(false);
                healthPhotos[criticalHealthIndex].SetActive(false);
        }
        else if (playerStats.CurrentHealthCanvas() <= .4f && playerStats.CurrentHealthCanvas() > .2f)
        {
                healthPhotos[fullHealthIndex].SetActive(false);
                healthPhotos[subHealthIndex].SetActive(false);
                healthPhotos[midHealthIndex].SetActive(false);
                healthPhotos[subCriticalHealthIndex].SetActive(true);
                healthPhotos[criticalHealthIndex].SetActive(false);
        }
        else if (playerStats.CurrentHealthCanvas() <= .2f)
        {
                healthPhotos[fullHealthIndex].SetActive(false);
                healthPhotos[subHealthIndex].SetActive(false);
                healthPhotos[midHealthIndex].SetActive(false);
                healthPhotos[subCriticalHealthIndex].SetActive(false);
                healthPhotos[criticalHealthIndex].SetActive(true);
        }
    }
}
