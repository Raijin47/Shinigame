using UnityEngine;
using TMPro;

public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPersisrentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;

    [SerializeField] DataContainer dataContainer;

    private void Start()
    {
        UpdateElement();
    }

    public void Upgrade()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];

        if(playerUpgrades.level >= playerUpgrades.maxLevel) { return; }
        if(dataContainer.souls >= playerUpgrades.costToUpgrade[playerUpgrades.level])
        {
            dataContainer.souls -= playerUpgrades.costToUpgrade[playerUpgrades.level];
            playerUpgrades.level += 1;
            UpdateElement();
        }
    }
    void UpdateElement()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];
        if (playerUpgrades.level >= playerUpgrades.maxLevel)
        {
            level.text = "max";
            price.text = "sold";
        }
        else
        {
            level.text = playerUpgrades.level.ToString();
            price.text = playerUpgrades.costToUpgrade[playerUpgrades.level].ToString();
        }

    }
}
