using UnityEngine;

public class EnemyRange : Enemy
{
    [SerializeField] private float distance;
    [SerializeField] private PoolObjectData projectile;
    [SerializeField] private Transform projectileSpawner;
    private float currentTimeToAttack;
    private PoolManager poolManager;
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
    protected override void FixedUpdate()
    {
        if (isDeath) { return; }
        ProcessStun();
    }
    protected override void Update()
    {
        base.Update();
        Attack();
    }
    protected override void Attack()
    {
        currentTimeToAttack += Time.deltaTime;
        if(currentTimeToAttack > timeToAttack)
        {
            SpawnProjectile(projectile, projectileSpawner.transform.position);
            currentTimeToAttack = 0;
        }
    }
    private Vector2 ShotDir()
    {
        Vector2 dir;

        dir = targetDestination.position - projectileSpawner.position;
        dir.Normalize();

        return dir;
    }
    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector2 position)
    {
        if(poolManager == null) poolManager = EssentialService.instance.poolManager;

        GameObject projectileGO = poolManager.GetObject(poolObjectData);

        projectileGO.transform.position = position;

        EnemyProjectile projectile = projectileGO.GetComponent<EnemyProjectile>();

        projectile.SetDirection(ShotDir(), stats.damage);


        return projectileGO;
    }
}