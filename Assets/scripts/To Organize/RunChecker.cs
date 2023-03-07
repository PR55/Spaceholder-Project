using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunChecker : MonoBehaviour
{
    RunReport runReport;

    public GameObject reportPrefab;
    // Start is called before the first frame update
    void Start()
    {


        if (FindObjectOfType<RunReport>() != null)
        {
            runReport = FindObjectOfType<RunReport>();
            runReport.checkScene();
        }

    }

    public RunReport runTracker()
    {
        return runReport;
    }
}
