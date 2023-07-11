using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<ItemData> items;
    private Character character;
    [SerializeField] private Image[] img;
    [HideInInspector] public int id;
    private void Awake()
    {
        character = GetComponent<Character>();
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

        img[id].sprite = itemToEquip.icon;
        id++;
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
    }
}