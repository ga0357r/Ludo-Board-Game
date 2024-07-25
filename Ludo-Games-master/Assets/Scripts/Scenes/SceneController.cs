using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
