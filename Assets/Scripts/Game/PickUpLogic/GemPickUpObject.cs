using UnityEngine;

public class GemPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] private int amount;
    public void OnPickUp(Character character)
    {
        character.level.AddExperience(amount);
    }
}