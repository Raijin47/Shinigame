using UnityEngine;

public class WeaponZangetsu : WeaponBase
{
    [SerializeField] PoolObjectData getsugaPrefab;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        Vector2 newPosition = transform.position;
        SpawnProjectile(getsugaPrefab, newPosition);
    }
}