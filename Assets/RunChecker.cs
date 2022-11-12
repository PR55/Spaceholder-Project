using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunChecker : MonoBehaviour
{
    RunReport runReport;
    // Start is called before the first frame update
    void Start()
    {
        runReport = FindObjectOfType<RunReport>();

        runReport.checkScene();

    }
}
