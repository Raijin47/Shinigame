using UnityEngine;
using System;
using System.Data;

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

    protected Rigidbody2D _rigidbody;
    protected Transform _targetDestination;

    [SerializeField] private Vector2 _attackArea;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private LayerMask _player;
    [SerializeField] private PoolObjectData[] _dropData;
    [SerializeField, Range(0f,1f)] private float _chanceDrop;
    [SerializeField] protected float _timeToAttack = .5f;

    private bool _isBurn = false;
    private bool _isRight = true;

    private int _burnDamage;

    private float _stunned;
    private float _burn;
    private float _knockbackForce;
    private float _knockbackTimeWeight;
    private float _currentTime;
    private float _timeToBurn = 0.5f;
    private float _curTTB;

    private Vector2 _knockbackVector;

    private Character _targetCharacter;
    private PoolMember _poolMember;
    private EnemyFade _enemyFade; 
    private BoxCollider2D _boxCol;
    private MessageSystem _message;
    private DropManager _dropManager;

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
        _burn = time;
        _burnDamage += damage;
        _enemyFade.Fire(_isBurn);
    }
    public void TakeDamage(int damage)
    {
        Stats.Hp -= damage;
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

    public void Stun(float stun)
    {
        if (stun == 0) { return; }

        _stunned = stun;
        _rigidbody.velocity = Vector2.zero;
    }
    public void Knockback(Vector2 vector, float force, float timeWeight)
    {
        if (force == 0) { return; }

        _knockbackVector = vector;
        _knockbackForce = force;
        _knockbackTimeWeight = timeWeight;
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
    protected virtual void Activate()
    {
        if (_enemyFade != null)
        {
            _isDeath = false;
            _enemyFade.Fire(false);
            _boxCol.enabled = true;
        }
    }
    protected virtual void UpdateState()
    {
        if (_isDeath) return;
        Flip();
        ProcessBurn();
        ProcessStun();     
        Attack();
    }
    protected virtual void UpdateAction()
    {
        if (_isDeath) return;
        Move();
    }

    protected void ProcessBurn()
    {
        if(_isBurn)
        {
            if (_burn > 0f)
            {
                _burn -= Time.deltaTime;
                _curTTB += Time.deltaTime;
                if(_curTTB > _timeToBurn)
                {
                    _curTTB = 0;
                    TakeDamage(_burnDamage);
                    _message.PostMessage(_burnDamage.ToString(), transform.position);
                }
            }
            else
            {
                _isBurn = false;
                _enemyFade.Fire(_isBurn);
                _burnDamage = 0;
            }
        }
    }
    protected virtual void Attack()
    {
        _currentTime -= Time.deltaTime;
        if(_currentTime < 0)
        {
            Collider2D col = Physics2D.OverlapBox(transform.position + _offset, _attackArea, 0f, _player);
            if(col != null) _targetCharacter.TakeDamage(Stats.Damage);
            _currentTime = _timeToAttack;
        }
    }
    protected void Flip()
    {
        if (transform.position.x > _targetDestination.position.x)
        {
            if (_isRight)
            {
                _isRight = false;
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            if (!_isRight)
            {
                _isRight = true;
                transform.localScale = new Vector2(1, 1);
            }
        }
    }
    protected void ProcessStun()
    {
        if(_stunned > 0f)
        {
            _stunned -= Time.deltaTime;
        }
    }
    protected virtual void Move()
    {
        if (_stunned > 0f) { return; }
        Vector3 direction = (_targetDestination.position - transform.position).normalized;
        _rigidbody.velocity = CalculateMovementVelocity(direction) + CalculateKnockBack();
    }
    protected Vector3 CalculateMovementVelocity(Vector3 direction)
    {
        return direction * Stats.MoveSpeed * (_stunned > 0f || _isDeath ? 0f: 1f);
    }
    protected Vector3 CalculateKnockBack()
    {
        if(_knockbackTimeWeight > 0f)
        {
            _knockbackTimeWeight -= Time.fixedDeltaTime;
        }

        return _knockbackVector * _knockbackForce * (_knockbackTimeWeight > 0f ? 1f : 0f);
    }
  
    private void Defeated()
    {
        _isDeath = true;
        _enemyFade.Death();
        _boxCol.enabled = false;
    }

   
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _enemyFade = GetComponentInChildren<EnemyFade>();
        _boxCol = GetComponent<BoxCollider2D>();
        _message = EssentialService.instance.message;
    }

    private void Update()
    {
        UpdateState();
    }
    private void FixedUpdate()
    {
        UpdateAction();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + _offset, _attackArea);
    }
}