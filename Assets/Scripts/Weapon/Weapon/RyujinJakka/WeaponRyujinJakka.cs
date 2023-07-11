using UnityEngine;

public class WeaponRyujinJakka : WeaponBase
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float _radius;
    public override void Attack()
    {
        particle.Play();
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, _radius);
        ApplyDamage(col);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public override void Resize()
    {

    }
}