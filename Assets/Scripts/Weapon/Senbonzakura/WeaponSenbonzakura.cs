using System.Collections;
using UnityEngine;

public class WeaponSenbonzakura: WeaponBase
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;
    [SerializeField] private float _cleanRadius;
    [SerializeField] private float _curRadius;
    [SerializeField] private Transform[] points;
    [SerializeField] private LayerMask layer;
    private bool f;
    protected override void Start()
    {
        base.Start();
        f = true;
        StartCoroutine(AttackProcess());
    }
    public override void Attack()
    {
        particle.Stop();
        var particleDuration = particle.main;
        particleDuration.duration = weaponStats.duration * wielder.attackAreaSizeItem;
        particle.Play();
        //StartCoroutine(AttackProcess());
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void Resize()
    {
        float r = wielder.attackAreaSizeItem * weaponStats.attackAreaSize;
        transform.localScale = new Vector2(r, r);
        _curRadius = _radius * r;
    }

    IEnumerator AttackProcess()
    {
        while (f)//(particle.isPlaying)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _curRadius, layer);
            int damage = GetDamage();
            for (int i = 0; i < colliders.Length; i++)
            {
                if(Vector2.Distance(colliders[i].transform.position, transform.position) > _cleanRadius)
                {
                    IDamageable e = colliders[i].GetComponent<IDamageable>();
                    ApplyDamage(colliders[i].transform.position, damage, e);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _curRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _cleanRadius);
    }
}