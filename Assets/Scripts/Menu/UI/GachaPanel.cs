using Assets.SimpleLocalization;
using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

[Serializable]
public class PoolGacha
{
    public Sprite bunnerImage;
    public CharacterData[] data;
    public string nameBanner;
}

public class GachaPanel : MonoBehaviour
{
    [SerializeField] private Image bunnerSprite;
    [SerializeField] private PoolGacha[] gacha;
    [SerializeField] private int price;
    [SerializeField] private DataContainer data;
    [SerializeField] private Image image;
    [SerializeField] private LoadingData saveService;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private LocalizedDynamic text;
    private int id;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += RewardedAds;
        YandexGame.PurchaseSuccessEvent += Pay;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= RewardedAds;
        YandexGame.PurchaseSuccessEvent -= Pay;
    }
    private void Start()
    {
        id = 0;
        UpdateUI();
    }
    public void LeftButton()
    {
        id--;
        if(id < 0)
        {
            id = gacha.Length - 1;
        }
        UpdateUI();
    }
    public void RightButton()
    {
        id++;
        if(id >= gacha.Length)
        {
            id = 0;
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        bunnerSprite.sprite = gacha[id].bunnerImage;
        text.Localize(gacha[id].nameBanner);
    }
    public void TwistButton()
    {
        if(data.butterflies >= price)
        {
            data.butterflies -= price;
            menuManager.SubPanelAnim(1);
        }
        else
        {
            menuManager.SubPanelAnim(0);
        }
    }
    private void RewardedAds(int x)
    {
        menuManager.SubPanelAnim(1);
    }
    public void Ads(int id)
    {
        YandexGame.RewVideoShow(id);
    }

    private void Pay(string id)
    {
        menuManager.SubPanelAnim(1);
    }
    public void Reward()
    {
        int a = UnityEngine.Random.Range(0, gacha[id].data.Length);

        image.sprite = gacha[id].data[a].icon;
        gacha[id].data[a].Level++;

        saveService.SaveData();
    }
}