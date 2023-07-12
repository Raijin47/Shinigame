using UnityEngine;

public class ProjectileLightning : ProjectileBase
{
    [SerializeField] private float offset;
    public override void SetDirection(float dir_x, float dir_y)
    {
        base.SetDirection(dir_x, dir_y);
        Attack();
    }
    private void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(0,offset), projectileSize, 0f);
        weapon.ApplyDamage(colliders);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0,offset), projectileSize);
    }
}