using System.Collections;
using UnityEngine;

public class EnemyGrand : Enemy
{
    [SerializeField] private PoolObjectData _projectile;
    [SerializeField] private Vector3 _offsetProjectile = new Vector2(0,0.8f);
    [SerializeField] private EnemyData _minion;

    private PoolManager _poolManager;
    private EnemyManager _manager;

    private Coroutine _updatePhaseCoroutine;

    private bool _isAction;
    public override void Activate()
    {
        base.Activate();

        _isAction = false;

        if (_poolManager == null)
        {
            _poolManager = EssentialService.instance.poolManager;
        }

        if (_manager == null)
        {
            _manager = EssentialService.instance.enemyManager;
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

            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    private void Rush()
    {
        _rigidbody.velocity = Vector2.zero;
        var direction = (_targetDestination.position - transform.position).normalized;
        _rigidbody.AddForce(direction * 8, ForceMode2D.Impulse);
    }
    private IEnumerator RushPhase()
    {
        var timeLimit = new WaitForSeconds(1f);
        for (int i = 0; i < 2; i++)
        {
            Rush();
            yield return timeLimit;
        }
    }
    private IEnumerator SpawnPhase()
    {
        var timer = new WaitForSeconds(0.5f);

        for (int i = 0; i < 10; i++)
        {
            _manager.SpawnEnemy(_minion, transform.position);
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
}