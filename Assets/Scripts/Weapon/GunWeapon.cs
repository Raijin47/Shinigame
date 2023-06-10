using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : WeaponBase
{

    [SerializeField] PoolObjectData prefab;

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