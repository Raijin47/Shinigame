using System.Collections;
using UnityEngine;

public class WeaponKunai : WeaponBase
{
    [SerializeField] PoolObjectData projectile;
    [SerializeField] float spread = 0.5f;

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
            //if (numberOfAttacks > 1)
            //{
            //    if (vectorOfAttack.y != 0)
            //    {
            //        newPos.x -= spread * (numberOfAttacks - 1) / 2;
            //        newPos.x += i * spread;
            //    }
            //    else
            //    {
            //        newPos.y -= spread * (numberOfAttacks - 1) / 2;
            //        newPos.y += i * spread;
            //    }
            //}
            SpawnProjectile(projectile, newPos);
            yield return timer;
        }
    }
}