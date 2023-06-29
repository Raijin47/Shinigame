using UnityEngine;

public class WeaponSenbonzakura: WeaponBase
{
    [SerializeField] private PoolObjectData sakuraPrefab;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Vector2 newPosition = transform.position;
            SpawnProjectile(sakuraPrefab, newPosition);
        }
    }
}