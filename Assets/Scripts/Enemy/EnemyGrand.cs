using System.Collections;
using UnityEngine;

public class EnemyGrand : Enemy
{
    [SerializeField] private PoolObjectData _projectile;
    [SerializeField] private Vector3 _offsetProjectile = new Vector2(0,0.8f);
    [SerializeField] private EnemyData _minion;
    [SerializeField] private Animator _animator;

    private PoolManager _poolManager;

    private Coroutine _updatePhaseCoroutine;

    private bool _isAction;

    private readonly string IdleA = "Idle";
    private readonly string RushA = "Rush";
    private readonly string MoveA = "Move";
    public override void Activate()
    {
        base.Activate();

        _isAction = false;

        if(_animator == null)
        {
            _animator = GetComponentInChildren<Animator>();
        }

        if (_poolManager == null)
        {
            _poolManager = EssentialService.instance.poolManager;
        }

        if (_updatePhaseCoroutine != null)
        {
            StopCoroutine(_updatePhaseCoroutine);
            _updatePhaseCoroutine = null;
        }
        _updatePhaseCoroutine = StartCoroutine(UpdatePhaseProcess());
    }

    protected override void Move()
    {
        if (_isAction) { return; }

        base.Move();
    }

    private IEnumerator UpdatePhaseProcess()
    {
        while (_isActive)
        {
            _isAction = true;
            _rigidbody.velocity = Vector2.zero;

            var action = Random.Range(0, 2) switch
            {
                0 => RushPhase(),
                1 => SpawnPhase(),
                _ => null,
            };
            yield return StartCoroutine(action);
            _isAction = false;
            _animator.SetTrigger(MoveA);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    private void Rush()
    {
        base.Flip();
        _rigidbody.velocity = Vector2.zero;
        var direction = (_targetDestination.position - transform.position).normalized;
        _rigidbody.AddForce(direction * 8, ForceMode2D.Impulse);
    }
    private IEnumerator RushPhase()
    {
        _animator.SetTrigger(RushA);
        var timeLimit = new WaitForSeconds(1f);
        for (int i = 0; i < 2; i++)
        {
            Rush();
            yield return timeLimit;
        }
    }
    private IEnumerator SpawnPhase()
    {
        _animator.SetTrigger(IdleA);
        var timer = new WaitForSeconds(0.5f);

        for (int i = 0; i < 10; i++)
        {
            _enemyManager.SpawnEnemy(_minion, transform.position);
            yield return timer;
        }
    }
    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector2 position)
    {
        var projectile = _poolManager.GetObject(poolObjectData).GetComponent<EnemyProjectile>();
        projectile.transform.position = position;

        projectile.SetDirection(ShotDirection(), Stats.Damage);

        return projectile.gameObject;
    }
    private Vector2 ShotDirection()
    {
        var direction = _targetDestination.position - (transform.position + _offsetProjectile);
        return direction.normalized;
    }
    protected override void Defeated()
    {
        base.Defeated();
        _updatePhaseCoroutine = null;
    }
    protected override void Attack()
    {
        base.Attack();
        if (_isAction) { return; }
        SpawnProjectile(_projectile, transform.position + _offsetProjectile);
    }
    protected override void Flip()
    {
        if(_isAction) { return; }
        base.Flip();
    }
    public override void Knockback(Vector2 vector, float force, float timeWeight)
    {

    }
}