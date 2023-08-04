using UnityEngine;

public class WeaponTobiume: WeaponBase
{
    [SerializeField] private PoolObjectData fireballPrefab;
    public override void Attack()
    {
        for(int i = 0; i < numberOfAttacks; i++)
        {
            UpdateVectorOfAttack();
            SpawnProjectile(fireballPrefab, transform.position);
        }
    }
}