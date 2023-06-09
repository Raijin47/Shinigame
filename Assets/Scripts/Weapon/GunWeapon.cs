using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : WeaponBase
{

    [SerializeField] GameObject prefab;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            GameObject shakkahou = Instantiate(prefab);

            Vector2 newPosition = transform.position;

            shakkahou.transform.position = newPosition;
            HadouShakkahouProjectile hadouShakkahouProjectile = shakkahou.GetComponent<HadouShakkahouProjectile>();

            hadouShakkahouProjectile.SetDirection(vectorOfAttack.x, vectorOfAttack.y);
            hadouShakkahouProjectile.damage = GetDamage();
        }
    }
}
