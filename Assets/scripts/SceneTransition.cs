using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int levelReportIndex = 2;

    RunReport runReport;

    IOCcam iOCcam;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag(Camera.main.tag).GetComponent<IOCcam>() != null)
        {
            iOCcam = GameObject.FindGameObjectWithTag(Camera.main.tag).GetComponent<IOCcam>();
        }
        runReport = FindObjectOfType<RunReport>();
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

            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene(Scene);
        }
    }
}
