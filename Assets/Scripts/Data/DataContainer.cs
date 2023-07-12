using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerPersisrentUpgrades
{
    HP,
    Damage,
    MovementSpeed,
    AttackSpeed,
    RecoveryHP,
    Armor,
    ProjectileSpeed,
    ProjectileCount,
    ExperienceBoost,
    GoldBoost,
    AttackAreaSize,
    Reroll,
    Duration
}

[Serializable]
public class PlayerUpgrades
{
    public PlayerPersisrentUpgrades persisrentUpgrades;
    public int level = 0;
    public int maxLevel = 0;
    public int[] costToUpgrade;
}

[CreateAssetMenu(fileName = "New DataContainer", menuName = "ScriptableObjects/DataContainer", order = 51)]
public class DataContainer : ScriptableObject
{
    public int souls;
    public int diamonds;
    public List<bool> stageCompletion;
    public List<PlayerUpgrades> upgrades;
    public CharacterData selectedCharacter;

    public void StageComplete(int i)
    {
        stageCompletion[i] = true;
    }

    public int GetUpgradeLevel(PlayerPersisrentUpgrades persisrentUpgrade)
    {
        return upgrades[(int)persisrentUpgrade].level;
    }

    public void SetSelectedCharacter(CharacterData character)
    {
        selectedCharacter = character;
    }
}