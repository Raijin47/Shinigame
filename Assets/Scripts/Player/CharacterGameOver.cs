using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject weaponParent;
    public void GameOver()
    {
        GetComponent<PlayerMovement>().enabled = false;
        gameOverPanel.SetActive(true);
        weaponParent.SetActive(false);
        pauseManager.PauseGame(true);
        SaveService.SaveGame();
    }
}