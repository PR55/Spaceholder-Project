using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslateRun : MonoBehaviour
{
    RunReport runReport;

    public Text TimeReport;

    public Text ScoreReport;

    // Start is called before the first frame update
    void Start()
    {
        runReport = FindObjectOfType<RunReport>();
        translateRun();
    }
    void translateRun()
    {
        TimeReport.text = "Run Time:\n" + runReport.runTime().ToString("hh':'mm':'ss'.'ff");
        ScoreReport.text = "Score:\n" + runReport.scoreRetrieve().ToString();
        runReport.reportCompleted();
    }
}
