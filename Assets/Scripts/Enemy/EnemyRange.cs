using UnityEngine;

public class EnemyRange : Enemy
{
    [SerializeField] private float _distance;
    [SerializeField] private PoolObjectData _projectile;
    [SerializeField] private Transform _projectileSpawner;

    private float _currentTimeToAttack;
    private PoolManager _poolManager;

   
    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector2 position)
    {
        if (_poolManager == null) _poolManager = EssentialService.instance.poolManager;

        GameObject projectileGO = _poolManager.GetObject(poolObjectData);

        projectileGO.transform.position = position;

        EnemyProjectile projectile = projectileGO.GetComponent<EnemyProjectile>();

        projectile.SetDirection(ShotDir(), Stats.Damage);


        return projectileGO;
    }

    protected override void Attack()
    {
       SpawnProjectile(_projectile, _projectileSpawner.transform.position);
    }
    //protected override void Move()
    //{
    //    if (stunned > 0f) { return; }
    //    if (Vector2.Distance(targetDestination.position, transform.position) < distance)
    //    {
    //        rb.velocity = Vector2.zero;
    //        return;
    //    }

    //    Vector3 direction = (targetDestination.position - transform.position).normalized;
    //    rb.velocity = CalculateMovementVelocity(direction) + CalculateKnockBack();
    //}
    private Vector2 ShotDir()
    {
        Vector2 dir;

        dir = _targetDestination.position - _projectileSpawner.position;
        dir.Normalize();

        return dir;
    }

}