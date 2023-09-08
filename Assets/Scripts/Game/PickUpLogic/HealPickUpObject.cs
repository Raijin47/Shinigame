using UnityEngine;

public class HealPickUpObject : MonoBehaviour, IPickUpObject, IPoolMember
{
    [SerializeField] private int healAmount;
    private PoolMember poolMember;

    public void OnPickUp(Character character)
    {
        character.Heal(healAmount);
        DestroyObj();
    }
    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
    public void DestroyObj()
    {
        if (poolMember == null)
        {
            Destroy(gameObject);
        }
        else
        {
            poolMember.ReturnToPool();
        }
    }
}