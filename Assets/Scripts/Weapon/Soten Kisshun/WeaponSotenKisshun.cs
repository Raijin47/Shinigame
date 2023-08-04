using UnityEngine;

public class WeaponSotenKisshun : WeaponBase
{
    [SerializeField] private PoolObjectData prefab;

    public override void Attack()
    {
        UpdateVectorOfAttack();

        Vector2 newPosition = transform.position;
        SpawnProjectile(prefab, newPosition);
    }
}