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
            data.souls -= price;
            gacha[id].data[UnityEngine.Random.Range(0, gacha[id].data.Length)].Level++;
        }
    }

    private void UpdateUI()
    {
        bunnerSprite.sprite = gacha[id].bunnerImage;
    }
}