using UnityEngine;

public class EnemyRunner : Enemy
{
    private Vector3 dir;
    protected override void Move()
    {
        if (dir == Vector3.zero)
        {
            dir = (targetDestination.position - transform.position).normalized;
            Flip();
        }
        rb.velocity = dir * stats.moveSpeed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (targetDestination != null)
        {
            dir = (targetDestination.position - transform.position).normalized;
            Flip();
        }
    }

    protected override void Update()
    {
        if (isDeath) { return; }
        ProcessBurn();
    }
}