using System.Collections;
using UnityEngine;

public class WeaponKunai : WeaponBase
{
    [SerializeField] float _radius;
    [SerializeField] PoolObjectData projectile;
    [SerializeField] LayerMask _layer;
    private Vector3 offset = new Vector2(0,0.5f);

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }
    IEnumerator AttackProcess()
    {
        var timer = new WaitForSeconds(0.1f);
        for (int i = 0; i < numberOfAttacks; i++)
        {
            AudioPlay();
            vectorOfAttack = SetDirection().normalized;
            SpawnProjectile(projectile, transform.position);
            yield return timer;
        }
    }

    private Vector2 SetDirection()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _radius,_layer);
        Vector2 dir;
        if (collider != null)
        {
            dir = collider.transform.position + offset - transform.position;
        }

        else dir = UtilityTools.GenerateRandomPositionSquarePattern(Vector2.one);

        return dir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}