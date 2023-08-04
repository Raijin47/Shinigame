using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.SimpleLocalization;
using YG;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private PauseManager pauseManager;
    [SerializeField] private Button rerollButton;
    [SerializeField] List<UpgradeButton> upgradeButtons;
    Level characterLevel;
    List<UpgradeData> upgradeData;
    [SerializeField] TextMeshProUGUI rerollCountText;
    [SerializeField] LocalizedDynamic text;
    private int rerollCount;

    private void OnEnable() => YandexGame.RewardVideoEvent += RewardedAds;
    private void OnDisable() => YandexGame.RewardVideoEvent -= RewardedAds;
    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        characterLevel = GameManager.instance.playerTransform.GetComponent<Level>();
    }
    private void Start()
    {
        HideButtons();
        rerollCount = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.Reroll);
    }
    private void RewardedAds(int x)
    {
        characterLevel.Reroll();
    }
    public void RerollPanel()
    {
        if(rerollCount == 0)
        {
            YandexGame.RewVideoShow(0);
        }
        else
        {
            rerollCount--;
            UpdateRerollStatus();
            characterLevel.Reroll();
        }
    }
    public void Reroll(List<UpgradeData> upgradeDatas)
    {
        Clean();

        this.upgradeData = upgradeDatas;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }
    private void UpdateRerollStatus()
    {
        if (rerollCount == 0)
        {
            text.Localize("NoRerollCount");
        }
        else
        {
            rerollCountText.text = rerollCount.ToString();
        }
    }
    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        UpdateRerollStatus();
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
        characterLevel.Upgrade(pressedButtonID);
        ClosePanel();
    }
    public void ClosePanel()
    {
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