using System.Collections;
using UnityEngine;

public class WeaponZangetsu : WeaponBase
{
    [SerializeField] PoolObjectData getsugaPrefab;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        AttackProcess();
    }

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            Vector2 newPosition = transform.position;
            SpawnProjectile(getsugaPrefab, newPosition);
        }
        yield return new WaitForSeconds(0.3f);
    }
}