using System.Collections;
using UnityEngine;

public class WeaponCero : WeaponBase
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Transform obj;
    [SerializeField] private LayerMask layer;
    [SerializeField] float distance;
    RaycastHit2D[] colliders;
    float attackTimer;

    public override void Attack()
    {
        AudioPlay();
        attackTimer = 0;
        particle.Play();
        StartCoroutine(AttackProcess());
    }

    protected override void Update()
    {
        base.Update();
        obj.Rotate(0, 0, projectileSpeed * Time.deltaTime);
        attackTimer += Time.deltaTime;
        if(attackTimer > duration)
        {
            particle.Stop();
        }
    }

    IEnumerator AttackProcess()
    {
        while(particle.isPlaying)
        {         
            float angle = obj.rotation.eulerAngles.z * Mathf.Deg2Rad;
            colliders = new RaycastHit2D[100];
            Physics2D.LinecastNonAlloc(obj.position, new Vector2(
            obj.position.x + distance * Mathf.Cos(angle),
            obj.position.y + distance * Mathf.Sin(angle)
            ), colliders, layer);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].collider == null)
                    break;
                if(colliders[i].collider.TryGetComponent(out IDamageable enemy))
                {
                    ApplyDamage(colliders[i].collider.transform.position, damage, enemy);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnDrawGizmos()
    {
        float angle = obj.rotation.eulerAngles.z * Mathf.Deg2Rad;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(obj.position, new Vector2(
            obj.position.x + distance * Mathf.Cos(angle),
            obj.position.y + distance * Mathf.Sin(angle)
            ));
    }
}