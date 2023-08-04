using UnityEngine;

public class SpiritualPressure : WeaponBase
{
    [SerializeField] private LayerMask layer;
    private Collider2D[] colliders;

    public override void Attack()
    {
        colliders = new Collider2D[50];
        Physics2D.OverlapCircleNonAlloc(transform.position, size, colliders, layer);
        ApplyDamage(colliders);
    }

    public override void Recalculate()
    {
        base.Recalculate();
        transform.localScale = new Vector2(size, size);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, size);
    }
}