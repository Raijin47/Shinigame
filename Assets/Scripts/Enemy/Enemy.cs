using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class EnemyStats
{
    public int Hp;
    public int Damage;
    public int ExperienceReward;
    public float MoveSpeed;

    public EnemyStats(EnemyStats stats)
    {
        Hp = stats.Hp;
        Damage = stats.Damage;
        ExperienceReward = stats.ExperienceReward;
        MoveSpeed = stats.MoveSpeed;
    }

    internal void ApplyProgress(float progress)
    {
        Hp = (int)(Hp * progress);
        Damage = (int)(Damage * progress);
        //this.experienceReward = (int)(experienceReward * progress);
    }
}
public class Enemy : MonoBehaviour, IDamageable, IPoolMember
{
    public EnemyStats Stats;

    protected bool _isDeath = false;

    [SerializeField] protected Rigidbody2D _rigidbody;
    protected Transform _targetDestination;

    [SerializeField] private Vector2 _attackArea;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private LayerMask _player;
    [SerializeField] private PoolObjectData[] _dropData;
    [SerializeField, Range(0f,1f)] private float _chanceDrop;
    [SerializeField] protected float _timeToAttack = .5f;

    [SerializeField] private BoxCollider2D _boxCol;

    private bool _isActive;
    private bool _isBurn;
    private bool _isStunned;
    private bool _isknockback;
    private bool _isRight = true;

    private int _burnDamage;

    private float _stunTime;
    private float _burnTime;
    private float _knockbackForce;
    private float _knockbackTimeWeight;
    private float _burnDamageCycle = 0.5f;

    private Vector2 _knockbackVector;
    private Vector2 _newVelocity;

    private Character _targetCharacter;
    private PoolMember _poolMember;
    private EnemyFade _enemyFade;
    private MessageSystem _message;
    private DropManager _dropManager;

    private Coroutine _updateDirectionCoroutine;
    private Coroutine _updateBurnProcessCoroutine;
    private Coroutine _updateStunPrecessCoroutine;
    private Coroutine _updateAttackPorecessCoroutine;
    private Coroutine _updateMovementProcessCoroutine;
    private Coroutine _updateKnockbackTimeCoroutine;
    
    public virtual void Activate()
    {
        _isActive = true;
        _isDeath = false;
        _boxCol.enabled = true;
        if (_message == null)
            _message = EssentialService.instance.message;

        if (_enemyFade == null)
        {
            _enemyFade = GetComponentInChildren<EnemyFade>();
        }
        _enemyFade.Fire(false);

        if (_updateDirectionCoroutine != null)
        {
            StopCoroutine(_updateDirectionCoroutine);
            _updateDirectionCoroutine = null;
        }
        _updateDirectionCoroutine = StartCoroutine(UpdateDirection());
        if (_updateAttackPorecessCoroutine != null)
        {
            StopCoroutine(_updateAttackPorecessCoroutine);
            _updateAttackPorecessCoroutine = null;
        }
        _updateAttackPorecessCoroutine = StartCoroutine(UpdateAttackPorecess());

        if (_updateMovementProcessCoroutine != null)
        {
            StopCoroutine(_updateMovementProcessCoroutine);
            _updateMovementProcessCoroutine = null;
        }
        _updateMovementProcessCoroutine = StartCoroutine(UpdateMovementProcess());
    }
    public void SetTarget(GameObject target, Character chara, DropManager drop)
    {
        _targetDestination = target.transform;
        _targetCharacter = chara;
        _dropManager = drop;
    }
    public void Burn(float time, int damage)
    {
        if (time == 0) { return; }

        _isBurn = true;
        _burnTime = time;
        _burnDamage += damage;
        _enemyFade.Fire(_isBurn);

        if (_updateBurnProcessCoroutine != null)
        {
            StopCoroutine(_updateBurnProcessCoroutine);
            _updateBurnProcessCoroutine = null;
        }
        StartCoroutine(UpdateBurnProcess());
    }
    public void TakeDamage(int damage, bool showMessage = true)
    {
        Stats.Hp -= damage;
        if (showMessage)
            _message.PostMessage(damage.ToString(), transform.position);

        if (Stats.Hp < 1)
        {
            _targetCharacter.AddKilled();
            Defeated();
        }
    }
  
    public void ReturnToPool()
    {
        _dropManager.ExpDrop(transform.position, Stats.ExperienceReward);
        _dropManager.Drop(transform.position, _dropData, _chanceDrop);
        if (_poolMember != null)
        {
            _poolMember.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Stun(float stunTime)
    {
        if (stunTime == 0) { return; }

        _isStunned = true;
        _stunTime = stunTime;
        _rigidbody.velocity = Vector2.zero;

        if (_updateStunPrecessCoroutine!=null)
        {
            StopCoroutine(_updateStunPrecessCoroutine);
            _updateStunPrecessCoroutine = null;
        }

        StartCoroutine(UpdateStunPrecess());
    }
    public void Knockback(Vector2 vector, float force, float timeWeight)
    {
        if (force == 0) { return; }

        _isknockback = true;
        _knockbackVector = vector;
        _knockbackForce = force;
        _knockbackTimeWeight = timeWeight;

        if (_updateKnockbackTimeCoroutine != null)
        {
            StopCoroutine(_updateKnockbackTimeCoroutine);
            _updateKnockbackTimeCoroutine = null;
        }
        _updateKnockbackTimeCoroutine = StartCoroutine(UpdateKnockbackTime());
    }
    public void SetPoolMember(PoolMember poolMember)
    {
        _poolMember = poolMember;
    }
  
    internal void UpdateStatsForProgress(float progress)
    {
        Stats.ApplyProgress(progress);
    }
    internal void SetStats(EnemyStats stats)
    {
        Stats = new EnemyStats(stats);
    }


    protected virtual void Attack()
    {
        _targetCharacter.TakeDamage(Stats.Damage);
    }
    private IEnumerator UpdateAttackPorecess()
    {
        while (_isActive)
        {
            yield return new WaitForSeconds(_timeToAttack);
            var direction = _targetDestination.position - transform.position;
            var directionLenth = direction.sqrMagnitude;
            var maxDirectionLenth = (direction.normalized * _attackArea).sqrMagnitude;
            if (directionLenth <= maxDirectionLenth)
            {
                Attack();
            }
        }
    }
    private IEnumerator UpdateBurnProcess()
    {
        var cyclesCount = MathF.Floor(_burnTime / _burnDamageCycle);
        for (int i = 0; i < cyclesCount; i++)
        {
            yield return new WaitForSeconds(_burnDamageCycle);
            TakeDamage(_burnDamage);
        }
        _isBurn = false;
        _enemyFade.Fire(_isBurn);
        _burnDamage = 0;
    }
    private IEnumerator UpdateDirection()
    {
        while (_isActive)
        {
            var isRight = transform.position.x < _targetDestination.position.x;
            if (_isRight != isRight)
            {
                _isRight = !_isRight;
                transform.localScale = new Vector2(_isRight ? 1 : -1, 1);
            }
            yield return null;
        }
    }
    private IEnumerator UpdateStunPrecess()
    {
        yield return new WaitForSeconds(_stunTime);
        _isStunned = false;
    }
    private IEnumerator UpdateMovementProcess()
    {
        while (_isActive)
        {
            if (_isStunned == false)
            {
                Move();
            }
            yield return null;
        }
    }
    protected virtual void Move()
    {
        if (_isStunned) { return; }

        _newVelocity = (_targetDestination.position - transform.position).normalized * Stats.MoveSpeed;
        if (_isknockback)
        {
            _newVelocity += _knockbackVector * _knockbackForce;
        }
        _rigidbody.velocity = _newVelocity;

    }

    private IEnumerator UpdateKnockbackTime()
    {
        yield return new WaitForSeconds(_knockbackTimeWeight);
        _isknockback = false;
    }
    private void Defeated()
    {
        _isActive = false;
        _isDeath = true;
        _boxCol.enabled = false;
        _enemyFade.Death();

        StopAllCoroutines();
        _updateDirectionCoroutine = null;
        _updateBurnProcessCoroutine = null;
        _updateStunPrecessCoroutine = null;
        _updateAttackPorecessCoroutine = null;
        _updateMovementProcessCoroutine = null;
        _updateKnockbackTimeCoroutine = null;
    }


    //private void Update()
    //{
    //    UpdateState();
    //}
    //private void FixedUpdate()
    //{
    //    UpdateAction();
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + _offset, _attackArea);
    }
}