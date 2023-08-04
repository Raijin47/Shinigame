using System;
using UnityEngine;

public enum UpgradeType
{
    WeaponUpgrade,
    ItemUpgrade,
    WeaponUnlock,
    ItemUnlock,
    Persistance
}

[Serializable]
public class PersistanceStats
{
    public int HealAmount;
    public int SoulAmount;
}


[CreateAssetMenu(fileName = "New Upgrade Data", menuName = "ScriptableObjects/UpgradeData", order = 51)]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string Name;
    public string Description;
    public Sprite icon;
    public string level;

    public WeaponData weaponData;
    public WeaponStats weaponUpgradeStats;
    public ItemData item;
    public ItemStats itemStats;
    public PersistanceStats persistanceStats;
    public UpgradeData nextUpgrade;
}