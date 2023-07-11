using UnityEngine;

public class SpiritualPressure : WeaponBase
{
    [SerializeField] private float radius;
    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        ApplyDamage(colliders);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public override void Resize()
    {
        float r = wielder.attackAreaSizeItem * weaponStats.attackAreaSize;
        transform.localScale = new Vector2(r, r);
        radius = r;
    }
}