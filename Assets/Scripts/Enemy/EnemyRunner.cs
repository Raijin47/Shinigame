using UnityEngine;

public class EnemyRunner : Enemy
{
    private Vector3 _direction;
    protected override void Move()
    {
        if (_direction == Vector3.zero)
        {
            _direction = (_targetDestination.position - transform.position).normalized;
        }
        _rigidbody.velocity = _direction * Stats.MoveSpeed;
    }

    public override void Activate()
    {
        base.Activate();
        if (_targetDestination != null)
        {
            _direction = (_targetDestination.position - transform.position).normalized;
        }
    }
}