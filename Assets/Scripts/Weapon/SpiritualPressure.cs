using UnityEngine;

public class SpiritualPressure : WeaponBase
{
    [SerializeField] float attackAreaSize = 3f;

    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackAreaSize);
        ApplyDamage(colliders);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackAreaSize);
    }
}