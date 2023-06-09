using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum PlayerPersisrentUpgrades
{
    HP,
    Damage
}

[Serializable]
public class PlayerUpgrades
{
    public PlayerPersisrentUpgrades persisrentUpgrades;
    public int level = 0;
    public int maxLevel = 0;
    public int[] costToUpgrade;
}


[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int coins;

    public List<bool> stageCompletion;

    public List<PlayerUpgrades> upgrades;

    public void StageComplete(int i)
    {
        stageCompletion[i] = true;
    }

    public int GetUpgradeLevel(PlayerPersisrentUpgrades persisrentUpgrade)
    {
        return upgrades[(int)persisrentUpgrade].level;
    }
}