using UnityEngine;

public class WeaponSotenKisshun : WeaponBase
{
    [SerializeField] private PoolObjectData prefab;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        AudioPlay();
        Vector2 newPosition = transform.position;
        SpawnProjectile(prefab, newPosition);
    }
}