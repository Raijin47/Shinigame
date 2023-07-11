using UnityEngine;

public class WeaponTobiume: WeaponBase
{
    [SerializeField] private PoolObjectData fireballPrefab;
    public override void Attack()
    {
        for(int i = 0; i < weaponStats.numberOfAttacks + wielder.projectileCountBonus; i++)
        {
            UpdateVectorOfAttack();
            SpawnProjectile(fireballPrefab, transform.position);
        }
    }
}