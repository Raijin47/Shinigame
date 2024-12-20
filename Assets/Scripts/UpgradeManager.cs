using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }
    public void OpenPanel()
    {
        pauseManager.PauseGame();
        panel.SetActive(true);
    }


    public void ClosePanel()
    {
        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }
}
