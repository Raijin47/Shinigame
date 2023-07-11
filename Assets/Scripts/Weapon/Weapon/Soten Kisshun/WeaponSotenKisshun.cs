using UnityEngine;

public class WeaponSotenKisshun : WeaponBase
{
    [SerializeField] private PoolObjectData prefab;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Vector2 newPosition = transform.position;
            SpawnProjectile(prefab, newPosition);
        }
    }
}