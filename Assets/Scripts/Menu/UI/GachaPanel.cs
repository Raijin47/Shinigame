using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PoolGacha
{
    public Sprite bunnerImage;
    public CharacterData[] data;
}

public class GachaPanel : MonoBehaviour
{
    [SerializeField] private Image bunnerSprite;
    [SerializeField] private PoolGacha[] gacha;
    [SerializeField] private int price;
    [SerializeField] private DataContainer data;
    [SerializeField] private GameObject noSoulsPanel;
    [SerializeField] private GachaFX gachaFX;
    [SerializeField] private GameObject lockPanel;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private Image image;
    private int id;

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

    public void TwistButton()
    {
        if(data.souls >= price)
        {
            lockPanel.SetActive(true);
            gachaFX.Twist();
        }
        else
        {
            noSoulsPanel.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        bunnerSprite.sprite = gacha[id].bunnerImage;
    }

    public void Reward()
    {
        data.souls -= price;
        int a = UnityEngine.Random.Range(0, gacha[id].data.Length);

        rewardPanel.SetActive(true);
        image.sprite = gacha[id].data[a].icon;
        gacha[id].data[a].Level++;
    }
}