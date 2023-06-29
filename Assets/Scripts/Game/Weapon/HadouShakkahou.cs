using UnityEngine;

public class HadouShakkahou : WeaponBase
{
    [SerializeField] PoolObjectData hadouPrefab;
    [SerializeField] float spread = 0.5f;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Vector2 newPosition = transform.position;
            if(weaponStats.numberOfAttacks > 1)
            {
                newPosition.y -= (spread * (weaponStats.numberOfAttacks-1)) / 2;
                newPosition.y += i * spread;
            }

            SpawnProjectile(hadouPrefab, newPosition);
        }
    }
}