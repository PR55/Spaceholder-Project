using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int levelReportIndex = 2;

    RunReport runReport;

    IOCcam iOCcam;

    [SerializeField]
    private bool changeScene = false;
    [SerializeField]
    private int scene = 1;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag(Camera.main.tag).GetComponent<IOCcam>() != null)
        {
            iOCcam = GameObject.FindGameObjectWithTag(Camera.main.tag).GetComponent<IOCcam>();
        }
        runReport = FindObjectOfType<RunReport>();
    }

    private void FixedUpdate()
    {
        if(changeScene)
        {
            SceneManage(scene);
        }
    }

    public void SceneManage(int Scene)
    {
        if(SceneManager.GetActiveScene().buildIndex == levelReportIndex && runReport != null)
        {
            Debug.Log("Report not Finished");
            if(runReport.reportProgress())
            {
                Resources.UnloadUnusedAssets();
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
