using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectContainer;
    [SerializeField] PoolManager poolManager;

    [SerializeField] public List<WeaponBase> weapons;
    [SerializeField] private Image[] img;
    [SerializeField] private TextMeshProUGUI[] lvl;
    Character character;
    Level level;
    private void Awake()
    {
        weapons = new List<WeaponBase>();
        character = GetComponent<Character>();
        level = GetComponent<Level>();
    }
    public void RecalculateWeapons()
    {
        for(int i = 0; i < weapons.Count; i ++)
        {
            weapons[i].Recalculate();
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

        level.AddUpgrade(weaponData.upgrade);

        int i = weapons.IndexOf(weaponBase);
        img[i].sprite = weaponData.icon;
        lvl[i].text = weaponData.level;

        if (weapons.Count == 6) level.RemoveWeaponList();
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgradeData.weaponData);
        weaponToUpgrade.Upgrade(upgradeData);

        if(upgradeData.nextUpgrade != null) level.AddUpgrade(upgradeData.nextUpgrade);

        int i = weapons.IndexOf(weaponToUpgrade);
        lvl[i].text = upgradeData.level;

        weaponToUpgrade.Recalculate();// возможно этому тут не место
    }
}