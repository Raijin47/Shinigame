using UnityEngine;

public class EnemyRange : Enemy
{
    [SerializeField] private float _distance;
    [SerializeField] private PoolObjectData _projectile;
    [SerializeField] private Vector3 _offsetProjectile = new Vector2(0, 0.8f);
    [SerializeField] private Transform _projectileSpawner;

    private PoolManager _poolManager;

    public override void Activate()
    {
        base.Activate();

        if (_poolManager == null)
        {
            _poolManager = EssentialService.instance.poolManager;
        }
    }
    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector2 position)
    {
        var projectile = _poolManager.GetObject(poolObjectData).GetComponent<EnemyProjectile>();
        projectile.transform.position = position;

        projectile.SetDirection(ShotDirection(), Stats.Damage);

        return projectile.gameObject;
    }

    protected override void Attack()
    {
        SpawnProjectile(_projectile, transform.position + _offsetProjectile);
    }
    protected override void Move()
    {

    }
    private Vector2 ShotDirection()
    {
        var direction = _targetDestination.position - (transform.position + _offsetProjectile);
        return direction.normalized;
    }
}