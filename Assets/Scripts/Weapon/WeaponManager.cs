using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectContainer;
    [SerializeField] PoolManager poolManager;

    [SerializeField] public List<WeaponBase> weapons;
    [SerializeField] private Image[] img;
    Character character;
    [HideInInspector] public int id;
    private void Awake()
    {
        weapons = new List<WeaponBase>();
        character = GetComponent<Character>();
    }
    public void ResizeWeapons()
    {
        for(int i = 0; i < weapons.Count; i ++)
        {
            weapons[i].Resize();
        }
    }
    public void AddWeapon(WeaponData weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectContainer);

        WeaponBase weaponBase = weaponGameObject.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weaponBase.SetPoolManager(poolManager);
        weapons.Add(weaponBase);
        weaponBase.AddOwnerCharacter(character);

        Level level = GetComponent<Level>();
        if(level != null)
        {
            level.AddUpgradesIntoTheListOfAvailableUpgrades(weaponData.upgrades);
        }
        img[id].sprite = weaponData.icon;
        id++;
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgradeData.weaponData);
        weaponToUpgrade.Upgrade(upgradeData);
    }
}