using System.Collections;
using UnityEngine;

public class WeaponSenbonzakura: WeaponBase
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;
    [SerializeField] private Transform[] points;
    public override void Attack()
    {
        particle.Play();
        StartCoroutine(AttackProcess());
    }

    protected override void Update()
    {
        base.Update();
    }


    IEnumerator AttackProcess()
    {
        while (particle.isPlaying)
        {
            Collider2D[] colFirst = Physics2D.OverlapCircleAll(points[0].position, _radius);
            ApplyDamage(colFirst);
            yield return new WaitForSeconds(0.1f);
            Collider2D[] colSecond = Physics2D.OverlapCircleAll(points[1].position, _radius);
            ApplyDamage(colSecond);
            yield return new WaitForSeconds(0.1f);
            Collider2D[] colThird = Physics2D.OverlapCircleAll(points[2].position, _radius);
            ApplyDamage(colThird);
            yield return new WaitForSeconds(0.1f);
            Collider2D[] colFour = Physics2D.OverlapCircleAll(points[3].position, _radius);
            ApplyDamage(colFour);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(points[0].position, _radius);
        Gizmos.DrawWireSphere(points[1].position, _radius);
        Gizmos.DrawWireSphere(points[2].position, _radius);
        Gizmos.DrawWireSphere(points[3].position, _radius);
    }
}