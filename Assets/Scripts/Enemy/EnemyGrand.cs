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
    private Coroutine _rushPhaseCoroutine;
    private Coroutine _spawnPhaseCoroutine;

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
    private void UpdatePhase()
    {
        _isAction = !_isAction;

        if (_isAction)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            if (_updatePhaseCoroutine != null)
            {
                StopCoroutine(_updatePhaseCoroutine);
                _updatePhaseCoroutine = null;
            }
            _updatePhaseCoroutine = StartCoroutine(UpdatePhaseProcess());
            return;
        }
        
        int phase = Random.Range(0, 2);
        switch (phase)
        {
            case 0:
                if(_rushPhaseCoroutine != null)
                {
                    StopCoroutine(_rushPhaseCoroutine);
                    _rushPhaseCoroutine = null;
                }
                _rushPhaseCoroutine = StartCoroutine(RushPhase());
                break;
            case 1:
                if(_spawnPhaseCoroutine != null)
                {
                    StopCoroutine(_spawnPhaseCoroutine);
                    _spawnPhaseCoroutine = null;
                }
                _spawnPhaseCoroutine = StartCoroutine(SpawnPhase());
                break;
        }
    }
    private IEnumerator UpdatePhaseProcess()
    {
        var Timer = new WaitForSeconds(Random.Range(5f,10f));
        yield return Timer;
        UpdatePhase();
    }
    private void Rush()
    {
        _rigidbody.velocity = Vector2.zero;
        Vector2 NewPos = (_targetDestination.position - transform.position).normalized;
        _rigidbody.AddForce(NewPos * 8, ForceMode2D.Impulse);
    }
    private IEnumerator RushPhase()
    {
        var timeLimit = new WaitForSeconds(2f);

        for (int i = 0; i < 2; i++)
        {
            Rush();
            yield return timeLimit;
        }

        UpdatePhase();
    }
    private IEnumerator SpawnPhase()
    {
        var timer = new WaitForSeconds(0.5f);

        for (int i = 0; i < 10; i++)
        {
            _manager.SpawnEnemy(_minion, transform.position);
            yield return timer;
        }

        UpdatePhase();
    }
    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector2 position)
    {
        GameObject projectileGO = _poolManager.GetObject(poolObjectData);

        projectileGO.transform.position = position;

        EnemyProjectile projectile = projectileGO.GetComponent<EnemyProjectile>();

        projectile.SetDirection(ShotDirection(), Stats.Damage);


        return projectileGO;
    }
    private Vector2 ShotDirection()
    {
        Vector2 dir;

        dir = _targetDestination.position - (transform.position + _offsetProjectile);
        dir.Normalize();

        return dir;
    }
    protected override void Defeated()
    {
        base.Defeated();
        _updatePhaseCoroutine = null;
        _rushPhaseCoroutine = null;
        _spawnPhaseCoroutine = null;
    }
    protected override void Attack()
    {
        base.Attack();
        if (_isAction) { return; }
        SpawnProjectile(_projectile, transform.position + _offsetProjectile);
    }
}