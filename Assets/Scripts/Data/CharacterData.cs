using UnityEngine;

[CreateAssetMenu(fileName = "New Chara Data", menuName = "ScriptableObjects/CharaData", order = 51)]
public class CharacterData : ScriptableObject
{
    public string Name;
    public GameObject spritePrefab;
    public WeaponData startingWeapon;

    public int Level;
    public int Health;
    [Range(0.8f, 1.2f)]public float Damage;
}