using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsCountText;
    private float coinBoost;

    private void Start()
    {
        ApplyPersistantUpgrades();
        Add(0);
    }

    private void ApplyPersistantUpgrades()
    {
        float GoldBoostUpgradeLevel = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.GoldBoost);
        coinBoost = 1 + GoldBoostUpgradeLevel / 10;
    }

    public void Add(int count)
    {
        EssentialService.instance.dataContainer.coins += (int)(count * coinBoost);
        coinsCountText.text = "Coins: " + EssentialService.instance.dataContainer.coins.ToString();
    }
}