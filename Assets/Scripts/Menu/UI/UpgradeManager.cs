using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] UpgradeDescriptionPanel upgradeDescriptionPanel;
    private PauseManager pauseManager;
    [SerializeField] private Button rerollButton;
    [SerializeField] List<UpgradeButton> upgradeButtons;

    Level characterLevel;
    private int selectedUpgradeID;
    List<UpgradeData> upgradeData;
    private int rerollCount = 0;
    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        characterLevel = GameManager.instance.playerTransform.GetComponent<Level>();
    }

    private void Start()
    {
        HideButtons();
        selectedUpgradeID = -1;
        rerollCount = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.Reroll);
    }

    public void RerollPanel(List<UpgradeData> upgradeDatas)
    {
        rerollCount--;
        UpdateRerollButton();
        selectedUpgradeID = -1;
        HideDescription();
        Clean();

        this.upgradeData = upgradeDatas;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }
    private void UpdateRerollButton()
    {
        if (rerollCount == 0) rerollButton.interactable = false;
        else rerollButton.interactable = true;
    }
    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        UpdateRerollButton();
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
    public void SetRerollCount(int count)
    {
        rerollCount = count;
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
