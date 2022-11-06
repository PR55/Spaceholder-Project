using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void SceneManage(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
