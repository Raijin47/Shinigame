using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsCountText;
    private float coinBoost;

    private void Start()
    {
        ApplyPersistantUpgrades();
    }

    private void ApplyPersistantUpgrades()
    {
        float GoldBoostUpgradeLevel = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.GoldBoost);
        coinBoost = 1 + GoldBoostUpgradeLevel * 0.2f;
    }

    public void Add(int count)
    {
        EssentialService.instance.dataContainer.coins += (int)(count * coinBoost);
        coinsCountText.text = "Coins: " + EssentialService.instance.dataContainer.coins.ToString();
    }
}