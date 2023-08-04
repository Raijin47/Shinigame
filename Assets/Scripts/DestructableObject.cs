using UnityEngine;

public class DestructableObject : MonoBehaviour, IDamageable, IPoolMember
{
    [SerializeField] private PoolObjectData[] drops;
    [SerializeField, Range(0,1f)] private float chance;
    private PoolMember poolMember;
    private DropManager dropManager;
    public void TakeDamage(int damage)
    {
        dropManager.Drop(transform.position, drops, chance);
        DestroyObj();
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
    public void Knockback(Vector2 vector, float force, float time)
    {

    }
    public void Stun(float stun)
    {

    }
    public void Burn(float time, int damage)
    {

    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
        dropManager = EssentialService.instance.dropManager;
    }
}