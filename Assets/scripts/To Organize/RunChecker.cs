using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunChecker : MonoBehaviour
{
    [SerializeField]
    RunReport runReport;

    public GameObject reportPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Reporter") != null)
        {
            if (GameObject.FindGameObjectWithTag("Reporter").GetComponent<RunReport>() != null)
            {
                runReport = GameObject.FindGameObjectWithTag("Reporter").GetComponent<RunReport>();
                runReport.checkScene();
            }
        }
        
    }

    public RunReport runTracker()
    {
        return runReport;
    }
}
