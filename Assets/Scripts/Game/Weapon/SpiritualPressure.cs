using UnityEngine;

public class SpiritualPressure : WeaponBase
{
    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, weaponStats.attackAreaSize);
        ApplyDamage(colliders);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponStats.attackAreaSize);
    }
}