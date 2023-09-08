using UnityEngine;

[CreateAssetMenu(fileName = "New Chara Data", menuName = "ScriptableObjects/CharaData", order = 51)]
public class CharacterData : ScriptableObject
{
    public int ID;
    public Sprite icon;
    public string Name;
    public string WeaponName;
    public string WeaponDescription;
    public GameObject spritePrefab;
    public UpgradeData startingWeapon;

    public int Level;
    public int Health;
    public float MovementSpeed;
    [Range(0.8f, 1.2f)]public float Damage;
}