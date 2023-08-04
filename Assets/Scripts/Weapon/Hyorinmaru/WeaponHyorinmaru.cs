using UnityEngine;

public class WeaponHyorinmaru : WeaponBase
{
    [SerializeField] private PoolObjectData prefab;

    public override void Attack()
    {
        for(int i = 0; i < numberOfAttacks; i++)
        {
            int a = 360 / numberOfAttacks * i;

            vectorOfAttack = Cicle(a);
            SpawnProjectile(prefab, transform.position);
        }
    }
    private Vector2 Cicle(int a)
    {
        Vector2 dir;
        float ang = a;
        dir.x = Mathf.Sin(ang * Mathf.Deg2Rad);
        dir.y = Mathf.Cos(ang * Mathf.Deg2Rad);

        return dir;
    }
}