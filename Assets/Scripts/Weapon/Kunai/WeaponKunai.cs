using UnityEngine;

public class WeaponKunai : WeaponBase
{
    [SerializeField] PoolObjectData projectile;
    [SerializeField] float spread = 0.5f;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        int countProjectile = weaponStats.numberOfAttacks + wielder.projectileCountBonus;
        for (int i = 0; i < countProjectile; i++)
        {
            Vector2 newPosition = transform.position;
            if (countProjectile > 1)
            {
                newPosition.y -= (spread * (weaponStats.numberOfAttacks - 1)) / 2;
                newPosition.y += i * spread;
            }

            SpawnProjectile(projectile, newPosition);
        }
    }
}