using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradeManager upgradeManager;

    [SerializeField] List<UpgradeData> upgadesAvailableOnStart;
    [SerializeField] List<UpgradeData> upgrades;
    [SerializeField] UpgradeData[] persistanceUpgrades;

    [SerializeField] int countUpgrades;

    private WeaponManager weaponManager;
    private PassiveItems passiveItems;
    private Character chara;

    private int level = 1;
    private int experience = 0;
    private float _boostExp;

    List<UpgradeData> selectedUpgrades;
    List<UpgradeData> acquiredUpgrades;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItems = GetComponent<PassiveItems>();
        chara = GetComponent<Character>();
    }
    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }
    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
    }
    public void SetBoost(float boostExp)
    {
        _boostExp = boostExp;
    }
    internal void AddUpgradesIntoTheListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        if(upgradesToAdd == null) { return; }
        this.upgrades.AddRange(upgradesToAdd);
    }

    internal void AddUpgrade(UpgradeData upgradeToAdd)
    {
        if(upgradeToAdd == null) { return; }
        this.upgrades.Add(upgradeToAdd);
    }

    public void RemoveWeaponList()
    {
        for (int i = upgrades.Count - 1; i > -1; i--)
        {
            if (upgrades[i].upgradeType == UpgradeType.WeaponUnlock)
            {
                upgrades.RemoveAt(i);
            }
        }
    }
    public void RemoveItemList()
    {
        for (int i = upgrades.Count - 1; i > -1; i--)
        {
            if (upgrades[i].upgradeType == UpgradeType.ItemUnlock)
            {
                upgrades.RemoveAt(i);
            }
        }
    }
    public void AddExperience(int amount)
    {
        experience += (int)(amount * _boostExp);
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }
    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }
    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];

        if(acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

        switch(upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:
                passiveItems.UpgradeItem(upgradeData);
                if (upgradeData.nextUpgrade != null) AddUpgrade(upgradeData.nextUpgrade);
                break;
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.ItemUnlock:
                passiveItems.Equip(upgradeData.item);
                AddUpgrade(upgradeData.item.upgrade);
                break;
            case UpgradeType.Persistance:
                chara.AddPersistance(upgradeData);
                break;
        }
        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
        weaponManager.RecalculateWeapons();
    }
    private void LevelUp()
    {
        if(selectedUpgrades == null) { selectedUpgrades = new List<UpgradeData>(); }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(countUpgrades));

        upgradeManager.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        experienceBar.SetLevelText(level);
    }
    public void Reroll()
    {
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(countUpgrades));
        upgradeManager.Reroll(selectedUpgrades);
    }
    private void ShuffleUpgrades()
    {
        for(int i = upgrades.Count - 1; i > 0; i--)
        {
            int x = Random.Range(0, i + 1);
            UpgradeData shuffleElement = upgrades[i];
            upgrades[i] = upgrades[x];
            upgrades[x] = shuffleElement;
        }
    }
    public List<UpgradeData> GetUpgrades(int count)
    {
        ShuffleUpgrades();
        List<UpgradeData> upgradeList = new List<UpgradeData>();        

        if(count > upgrades.Count)
        {
            count = upgrades.Count;
        }
        for(int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[i]);
        }

        if(upgrades.Count < countUpgrades)
        {
            upgradeList.Add(persistanceUpgrades[0]);
        }
        if(upgrades.Count < countUpgrades - 1)
        {
            upgradeList.Add(persistanceUpgrades[1]);
        }
        return upgradeList;
    }
    public void AddStartWeapon(UpgradeData data)
    {
        AddUpgradesIntoTheListOfAvailableUpgrades(upgadesAvailableOnStart);

        weaponManager.AddWeapon(data.weaponData);
        upgrades.Remove(data);
        weaponManager.RecalculateWeapons();
    }
}