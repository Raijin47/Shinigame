using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void RestartGameplay()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).buildIndex, LoadSceneMode.Single);
        SceneManager.LoadScene(SceneManager.GetSceneAt(1).buildIndex, LoadSceneMode.Additive);
    }
}