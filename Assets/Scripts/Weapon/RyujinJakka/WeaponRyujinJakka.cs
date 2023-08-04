using UnityEngine;

public class WeaponRyujinJakka : WeaponBase
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask layer;
    private Collider2D[] colliders;
    private float _curRadius;
    public override void Attack()
    {
        particle.Play();
        colliders = new Collider2D[50];
        Physics2D.OverlapCircleNonAlloc(transform.position, _curRadius, colliders, layer);
        ApplyDamage(colliders);
    }
    public override void Recalculate()
    {
        base.Recalculate();
        transform.localScale = new Vector2(size, size);
        _curRadius = _radius * size;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _curRadius);
    }
}