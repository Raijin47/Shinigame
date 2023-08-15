using System.Collections;
using UnityEngine;

public class WeaponKunai : WeaponBase
{
    [SerializeField] PoolObjectData projectile;

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }
    IEnumerator AttackProcess()
    {
        var timer = new WaitForSeconds(0.1f);
        for (int i = 0; i < numberOfAttacks; i++)
        {
            Vector2 newPos = transform.position;
            UpdateVectorOfAttack();
            SpawnProjectile(projectile, newPos);
            yield return timer;
        }
    }
}