using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int levelReportIndex = 2;

    RunReport runReport;

    private void Start()
    {
        runReport = GameObject.FindObjectOfType<RunReport>();
    }

    public void SceneManage(int Scene)
    {
        if(SceneManager.GetActiveScene().buildIndex == levelReportIndex && runReport != null)
        {
            Debug.Log("Report not Finished");
            if(runReport.reportProgress())
            {
                SceneManager.LoadScene(Scene);
            }
        }
        else
        {
            SceneManager.LoadScene(Scene);
        }
    }
}
