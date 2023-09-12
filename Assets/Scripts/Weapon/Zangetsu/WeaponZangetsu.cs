using UnityEngine;

public class WeaponZangetsu : WeaponBase
{
    [SerializeField] PoolObjectData getsugaPrefab;
    [SerializeField] LayerMask _layer;
    [SerializeField] float _radius;
    private Vector3 offset = new Vector2(0, 0.5f);

    public override void Attack()
    {
        AudioPlay();
        vectorOfAttack = SetDirection().normalized;
        SpawnProjectile(getsugaPrefab, transform.position);
    }

    private Vector2 SetDirection()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _radius, _layer);
        Vector2 dir;
        if (collider != null)
        {
            dir = collider.transform.position + offset - transform.position;
        }

        else dir = UtilityTools.GenerateRandomPositionSquarePattern(Vector2.one);

        return dir;
    }
}