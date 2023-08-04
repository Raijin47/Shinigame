using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<ItemData> items;
    private Character character;
    [SerializeField] private Image[] img;
    [SerializeField] private TextMeshProUGUI[] lvl;
    private Level level;

    private void Awake()
    {
        character = GetComponent<Character>();
        level = GetComponent<Level>();
    }
    public void Equip(ItemData itemToEquip)
    {
        if(items == null)
        {
            items = new List<ItemData>();
        }
        ItemData newItemInstance = new ItemData();
        newItemInstance.Init(itemToEquip.Name);
        newItemInstance.stats.Sum(itemToEquip.stats);

        items.Add(newItemInstance);
        newItemInstance.Equip(character);

        int i = items.IndexOf(newItemInstance);
        img[i].sprite = itemToEquip.icon;
        lvl[i].text = itemToEquip.level;

        if (items.Count == 6) level.RemoveItemList();
    }
    public void UnEquip(ItemData itemToEquip)
    {

    }
    internal void UpgradeItem(UpgradeData upgradeData)
    {
        ItemData itemToUpgrade = items.Find(id => id.Name == upgradeData.item.Name);
        itemToUpgrade.UnEquip(character);
        itemToUpgrade.stats.Sum(upgradeData.itemStats);
        itemToUpgrade.Equip(character);

        int i = items.IndexOf(itemToUpgrade);
        lvl[i].text = upgradeData.level;
    }
}