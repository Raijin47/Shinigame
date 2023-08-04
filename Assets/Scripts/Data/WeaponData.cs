using System;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public int damage;
    public float timeToAttack;
    public int numberOfAttacks;
    public int numberOfHits;
    public float projectileSpeed;
    public float stun;
    public float knockback;
    public float knockbackTimeWeight;
    public float attackAreaSize;
    public float timeBurn;
    public int damageBurn;
    public float duration;

    public WeaponStats(WeaponStats stats)
    {
        this.damage = stats.damage;
        this.timeToAttack = stats.timeToAttack;
        this.numberOfAttacks = stats.numberOfAttacks;
        this.numberOfHits = stats.numberOfHits;
        this.projectileSpeed = stats.projectileSpeed;
        this.stun = stats.stun;
        this.knockback = stats.knockback;
        this.knockbackTimeWeight = stats.knockbackTimeWeight;
        this.attackAreaSize = stats.attackAreaSize;
        this.timeBurn = stats.timeBurn;
        this.damageBurn = stats.damageBurn;
        this.duration = stats.duration;
    }

    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack -= weaponUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
        this.numberOfHits += weaponUpgradeStats.numberOfHits;
        this.projectileSpeed += weaponUpgradeStats.projectileSpeed;
        this.stun += weaponUpgradeStats.stun;
        this.knockback += weaponUpgradeStats.knockback;
        this.knockbackTimeWeight += weaponUpgradeStats.knockbackTimeWeight;
        this.attackAreaSize += weaponUpgradeStats.attackAreaSize;
        this.timeBurn += weaponUpgradeStats.timeBurn;
        this.damageBurn += weaponUpgradeStats.damageBurn;
        this.duration += weaponUpgradeStats.duration;
    }
}
[CreateAssetMenu(fileName = "New WeaponData", menuName = "ScriptableObjects/WeaponData", order = 51)]
public class WeaponData : ScriptableObject
{
    public Sprite icon;
    public string level;
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public UpgradeData upgrade;
}