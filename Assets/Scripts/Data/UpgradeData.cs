using UnityEngine;

public enum UpgradeType
{
    WeaponUpgrade,
    ItemUpgrade,
    WeaponUnlock,
    ItemUnlock
}

[CreateAssetMenu(fileName = "New Upgrade Data", menuName = "ScriptableObjects/UpgradeData", order = 51)]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string Name;
    public string Description;
    public Sprite icon;

    public WeaponData weaponData;
    public WeaponStats weaponUpgradeStats;

    public Item item;
    public ItemStats itemStats;
}