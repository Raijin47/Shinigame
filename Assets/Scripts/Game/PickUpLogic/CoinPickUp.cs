using UnityEngine;

public class CoinPickUp : MonoBehaviour, IPickUpObject, IPoolMember
{
    [SerializeField] int count;
    private PoolMember poolMember;
    public void OnPickUp(Character character)
    {
        character.coins.Add(count);
        DestroyObj();
    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
    private void DestroyObj()
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