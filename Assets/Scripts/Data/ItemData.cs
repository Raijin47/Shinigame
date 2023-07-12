using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStats
{
    public float damage;
    public float attackSpeed;
    public float attackArea;
    public float projectileSpeed;
    public float movementSpeed;
    public float experience;
    public float souls;
    public float health;
    public int recovery;
    public int projectileCount;
    public int armor;
    public float duration;

    internal void Sum(ItemStats stats)
    {
        this.damage += stats.damage;
        this.attackSpeed += stats.attackSpeed;
        this.attackArea += stats.attackArea;
        this.projectileSpeed += stats.projectileSpeed;
        this.movementSpeed += stats.movementSpeed;
        this.experience += stats.experience;
        this.souls += stats.souls;
        this.health += stats.health;
        this.recovery += stats.recovery;
        this.projectileCount += stats.projectileCount;
        this.armor += stats.armor;
        this.duration += stats.duration;
    }
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "ScriptableObjects/ItemData", order = 51)]
public class ItemData: ScriptableObject
{
    public Sprite icon;
    public string Name;
    public ItemStats stats;
    public List<UpgradeData> upgrades;

    public void Init(string Name)
    {
        this.Name = Name;
        stats = new ItemStats();
        upgrades = new List<UpgradeData>();
    }
    public void Equip(Character character)
    {        
        character.damageItem += stats.damage;
        character.attackSpeedItem += stats.attackSpeed;
        character.attackAreaSizeItem += stats.attackArea;
        character.projectileSpeedItem += stats.projectileSpeed;
        character.movementSpeedItem += stats.movementSpeed;
        character.experienceItem += stats.experience;
        character.soulsItem += stats.souls;
        character.healthItem += stats.health;
        character.recoveryItem += stats.recovery;
        character.projectileCountItem += stats.projectileCount;
        character.armorItem += stats.armor;
        character.durationItem += stats.duration;
        character.CalculateStats();
    }

    public void UnEquip(Character character)
    {
        character.damageItem -= stats.damage;
        character.attackSpeedItem -= stats.attackSpeed;
        character.attackAreaSizeItem -= stats.attackArea;
        character.projectileSpeedItem -= stats.projectileSpeed;
        character.movementSpeedItem -= stats.movementSpeed;
        character.experienceItem -= stats.experience;
        character.soulsItem -= stats.souls;
        character.healthItem -= stats.health;
        character.recoveryItem -= stats.recovery;
        character.projectileCountItem -= stats.projectileCount;
        character.armorItem -= stats.armor;
        character.durationItem -= stats.duration;
    }
}