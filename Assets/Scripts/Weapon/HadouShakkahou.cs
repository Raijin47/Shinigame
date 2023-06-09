using UnityEngine;

public class HadouShakkahou : WeaponBase
{
    [SerializeField] GameObject hadouPrefab;
    [SerializeField] float spread = 0.5f;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject shakkahou = Instantiate(hadouPrefab);

            Vector2 newPosition = transform.position;
            if(weaponStats.numberOfAttacks > 1)
            {
                newPosition.y -= (spread * (weaponStats.numberOfAttacks-1)) / 2;
                newPosition.y += i * spread;
            }

            shakkahou.transform.position = newPosition;
            HadouShakkahouProjectile hadouShakkahouProjectile = shakkahou.GetComponent<HadouShakkahouProjectile>();

            hadouShakkahouProjectile.SetDirection(vectorOfAttack.x, vectorOfAttack.y);
            hadouShakkahouProjectile.damage = GetDamage();
        }
    }
}