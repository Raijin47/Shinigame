using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
    public void GoToMenu()
    {
        SaveService.SaveGame();
        SceneManager.LoadScene("Menu");
    }
}