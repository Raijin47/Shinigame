using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] UpgradeDescriptionPanel upgradeDescriptionPanel;
    private PauseManager pauseManager;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    Level characterLevel;
    private int selectedUpgradeID;
    List<UpgradeData> upgradeData;
    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        characterLevel = GameManager.instance.playerTransform.GetComponent<Level>();
    }


    private void Start()
    {
        HideButtons();
        selectedUpgradeID = -1;
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        pauseManager.PauseGame();
        panel.SetActive(true);

        this.upgradeData = upgradeDatas;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        if(selectedUpgradeID != pressedButtonID)
        {
            selectedUpgradeID = pressedButtonID;
            ShowDescription();
        }
        else
        {
            characterLevel.Upgrade(pressedButtonID);
            ClosePanel();
            HideDescription();
        }
    }

    private void HideDescription()
    {
        upgradeDescriptionPanel.gameObject.SetActive(false);
    }

    private void ShowDescription()
    {
        upgradeDescriptionPanel.gameObject.SetActive(true);
        upgradeDescriptionPanel.Set(upgradeData[selectedUpgradeID]);

    }

    public void ClosePanel()
    {
        selectedUpgradeID = -1;
        HideButtons();

        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
