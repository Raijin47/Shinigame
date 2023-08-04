using UnityEngine;
using System.Collections;
public class WeaponLightning: WeaponBase
{
    [SerializeField] private Vector2 radius;
    [SerializeField] private PoolObjectData lightningPrefab;
    [SerializeField] private LayerMask layer;
    private Vector2 target;

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }
    IEnumerator AttackProcess()
    {
        var timer = new WaitForSeconds(0.3f);
        for (int i = 0; i < numberOfAttacks; i++)
        {
            SetTarget();
            SpawnProjectile(lightningPrefab, target);
            yield return timer;
        }
    }
    private Vector2 SetTarget()
    {
        Collider2D[] collider = Physics2D.OverlapBoxAll(EssentialService.instance.character.transform.position, radius, 0f, layer);
        if (collider.Length != 0) target = collider[Random.Range(0, collider.Length)].transform.position;
        else target = new Vector2(Random.Range(-10, 10), Random.Range(-5, 5));

        return target;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, radius);
    }
}