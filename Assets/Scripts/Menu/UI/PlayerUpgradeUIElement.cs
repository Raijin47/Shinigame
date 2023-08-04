using UnityEngine;
using TMPro;

public class PlayerUpgradeUIElement : MonoBehaviour
{
    [SerializeField] PlayerPersisrentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI price;
    [SerializeField] private GameObject[] level;
    [SerializeField] private GameObject maximum;
    [SerializeField] private GameObject button;

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
            price.gameObject.SetActive(false);
            button.SetActive(false);
            maximum.SetActive(true);
        }
        else
        {
            price.text = playerUpgrades.costToUpgrade[playerUpgrades.level].ToString();
        }
        for (int i = 0; i < playerUpgrades.level; i++)
        {
            level[i].SetActive(true);
        }
    }
}
