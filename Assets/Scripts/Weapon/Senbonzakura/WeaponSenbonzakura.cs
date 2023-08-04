using System.Collections;
using UnityEngine;

public class WeaponSenbonzakura: WeaponBase
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private LayerMask layer;

    [SerializeField] private float _radius;
    [SerializeField] private float _exceptionRadius;
    private float _curRadius;
    private Collider2D[] colliders;

    public override void Attack()
    {
        particle.Stop();
        var particleDuration = particle.main;
        particleDuration.duration = duration;
        particle.Play();
        StartCoroutine(AttackProcess());
    }
    public override void Recalculate()
    {
        base.Recalculate();
        transform.localScale = new Vector2(size, size);
        _curRadius = _radius * size;
        _exceptionRadius = size;
    }
    IEnumerator AttackProcess()
    {
        while (particle.isPlaying)
        {
            var timer = new WaitForSeconds(0.30f);
            colliders = new Collider2D[50];
            Physics2D.OverlapCircleNonAlloc(transform.position, _curRadius, colliders, layer);
            ApplyDamage(colliders);
            yield return timer;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _curRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _exceptionRadius);
    }
}