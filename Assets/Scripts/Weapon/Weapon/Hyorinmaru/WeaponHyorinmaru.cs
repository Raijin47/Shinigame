using UnityEngine;

public class WeaponHyorinmaru : WeaponBase
{
    [SerializeField] private PoolObjectData prefab;
    [SerializeField] private PoolObjectData prefabSmall;

    public override void Attack()
    {
        UpdateVectorOfAttack();
        SpawnProjectile(prefab, transform.position);
        for(int i = 0; i < weaponStats.numberOfAttacks + wielder.projectileCountBonus; i++)
        {
            vectorOfAttack = UtilityTools.GenerateRandomPositionSquarePattern(Vector2.one).normalized;
            SpawnProjectile(prefabSmall, transform.position);
        }
    }
}