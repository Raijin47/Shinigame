using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMessagePanel;
    PauseManager pauseManager;
    [SerializeField] DataContainer dataContainer;

    private void Start()
    {
        pauseManager = GetComponent<PauseManager>();
    }
    public void Win(int stageID)
    {
        winMessagePanel.SetActive(true);
        pauseManager.PauseGame();
        dataContainer.StageComplete(stageID);
    }
}