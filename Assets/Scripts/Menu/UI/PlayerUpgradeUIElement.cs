using UnityEngine;
using TMPro;
using Assets.SimpleLocalization;
using UnityEngine.UI;

public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPersisrentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] private Button button;
    [SerializeField] private LocalizedDynamic buttonText;

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
            level.text = playerUpgrades.level.ToString();
            price.GetComponent<LocalizedDynamic>().Localize("Sold");
            buttonText.Localize("Max");
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
            buttonText.Localize("Improve");
            level.text = playerUpgrades.level.ToString();
            price.text = playerUpgrades.costToUpgrade[playerUpgrades.level].ToString();
        }

    }
}
