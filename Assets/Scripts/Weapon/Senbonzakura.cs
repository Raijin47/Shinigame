using UnityEngine;

public class Senbonzakura : WeaponBase
{
    [SerializeField] private Vector2 attackZone;
    [SerializeField] private SenbonzakuraProjectile[] projectile;
    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, attackZone, 0f);
        projectile[0].GetTarget(colliders[Random.Range(0,colliders.Length)]);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attackZone);
    }
}
