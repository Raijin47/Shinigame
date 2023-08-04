using UnityEngine;

public class ProjectileLightning : ProjectileBase
{
    [SerializeField] private float offset;
    [SerializeField] private LayerMask layer;
    private Collider2D[] colliders;
    [SerializeField] private Vector2 _areaSize;
    private Vector2 areaSize;
    
    public override void SetDirection(float dir_x, float dir_y)
    {
        areaSize = projectileSize * _areaSize;
        base.SetDirection(dir_x, dir_y);
        Attack();

    }
    private void Attack()
    {
        colliders = new Collider2D[10];
        Physics2D.OverlapBoxNonAlloc(transform.position + new Vector3(0, offset), areaSize, 0f, colliders, layer);
        weapon.ApplyDamage(colliders);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0,offset), areaSize);
    }
}