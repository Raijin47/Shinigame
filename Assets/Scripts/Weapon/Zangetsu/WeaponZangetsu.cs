using UnityEngine;

public class WeaponZangetsu : WeaponBase
{
    [SerializeField] PoolObjectData getsugaPrefab;

    public override void Attack()
    {
        AudioPlay();
        UpdateVectorOfAttack();
        Vector2 newPosition = transform.position;
        SpawnProjectile(getsugaPrefab, newPosition);
    }
}