using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMessagePanel;
    [SerializeField] DataContainer dataContainer;
    private PauseManager pauseManager;

    private void Start()
    {
        pauseManager = GetComponent<PauseManager>();
    }
    public void Win(int stageID)
    {
        winMessagePanel.SetActive(true);
        pauseManager.PauseGame(true);
        dataContainer.StageComplete(stageID);
        dataContainer.butterflies++;
        SaveService.SaveGame();
    }
}