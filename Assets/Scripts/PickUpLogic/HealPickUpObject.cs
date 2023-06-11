using UnityEngine;

public class HealPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] private int healAmount;
    public void OnPickUp(Character character)
    {
        character.Heal(healAmount);
    }
}