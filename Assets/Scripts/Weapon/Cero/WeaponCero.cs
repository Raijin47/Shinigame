using System.Collections;
using UnityEngine;

public class WeaponCero : WeaponBase
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private Transform obj;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] float distance;
    private float dir;
    public override void Attack()
    {
        particle.Play();
        StartCoroutine(AttackProcess());
        if (Random.value < 0.5f) dir = 1;
        else dir = -1;
    }

    protected override void Update()
    {
        base.Update();
        obj.Rotate(0, 0, dir * weaponStats.projectileSpeed * Time.deltaTime);
    }

    IEnumerator AttackProcess()
    {
        while(particle.isPlaying)
        {
            float angle = obj.rotation.eulerAngles.z * Mathf.Deg2Rad;
            RaycastHit2D[] hit = Physics2D.LinecastAll(obj.position, new Vector2(
            obj.position.x + distance * Mathf.Cos(angle),
            obj.position.y + distance * Mathf.Sin(angle)
            ));

            int damage = GetDamage();
            for (int a = 0; a < hit.Length; a++)
            {
                IDamageable e = hit[a].collider.GetComponent<IDamageable>();
                if (e != null)
                {
                    ApplyDamage(hit[a].collider.transform.position, damage, e);
                }
            }

            Collider2D[] colliders = Physics2D.OverlapBoxAll(obj.position, attackSize, obj.rotation.z);
            ApplyDamage(colliders);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        float angle = obj.rotation.eulerAngles.z * Mathf.Deg2Rad;
        Gizmos.DrawLine(obj.position, new Vector2(
            obj.position.x + distance * Mathf.Cos(angle),
            obj.position.y + distance * Mathf.Sin(angle)
            ));
    }
}