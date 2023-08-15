using UnityEngine;
using System;

[Serializable]
public class EnemyStats
{
    public int hp;
    public int damage;
    public int experienceReward;
    public float moveSpeed;

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.experienceReward = stats.experienceReward;
        this.moveSpeed = stats.moveSpeed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);
        //this.experienceReward = (int)(experienceReward * progress);
    }
}
public class Enemy : MonoBehaviour, IDamageable, IPoolMember
{
    public EnemyStats stats;

    [SerializeField] private Vector2 attackArea;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask player;
    [SerializeField] private PoolObjectData[] dropData;
    [SerializeField, Range(0f,1f)] private float _chanceDrop;
    protected Rigidbody2D rb;

    private Character targetCharacter;
    protected Transform targetDestination;
    private PoolMember poolMember;
    private Vector2 knockbackVector;
    private EnemyFade enemyFade; 
    private BoxCollider2D _boxCol;
    private MessageSystem message;
    private DropManager dropManager;

    protected float stunned;
    private float burn;
    private float knockbackForce;
    private float knockbackTimeWeight;
    [SerializeField] protected float timeToAttack = .5f;
    private float currentTime;
    private float timeToBurn = 0.5f;
    private float curTTB;
 
    private int burnDamage;

    private bool isBurn = false;
    private bool isRight = true;
    protected bool isDeath = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        enemyFade = GetComponentInChildren<EnemyFade>();
        _boxCol = GetComponent<BoxCollider2D>();
        message = EssentialService.instance.message;
    }
    public void SetTarget(GameObject target, Character chara, DropManager drop)
    {
        targetDestination = target.transform;
        targetCharacter = chara;
        dropManager = drop;
    }
    protected virtual void Update()
    {
        if(isDeath) { return; }
        Flip();
        ProcessBurn();
    }
    protected virtual void FixedUpdate()
    {
        if (isDeath) { return; }
        ProcessStun();
        Move();
        Attack();
    }
    public void Burn(float time, int damage)
    {
        if(time == 0) { return; }

        isBurn = true;
        burn = time;
        burnDamage += damage;
        enemyFade.Fire(isBurn);
    }
    protected void ProcessBurn()
    {
        if(isBurn)
        {
            if (burn > 0f)
            {
                burn -= Time.deltaTime;
                curTTB += Time.deltaTime;
                if(curTTB > timeToBurn)
                {
                    curTTB = 0;
                    TakeDamage(burnDamage);
                    message.PostMessage(burnDamage.ToString(), transform.position);
                }
            }
            else
            {
                isBurn = false;
                enemyFade.Fire(isBurn);
                burnDamage = 0;
            }
        }
    }
    protected virtual void Attack()
    {
        currentTime -= Time.fixedDeltaTime;
        if(currentTime < 0)
        {
            Collider2D col = Physics2D.OverlapBox(transform.position + offset, attackArea, 0f, player);
            if(col != null) targetCharacter.TakeDamage(stats.damage);
            currentTime = timeToAttack;
        }
    }
    protected void Flip()
    {
        if (transform.position.x > targetDestination.position.x)
        {
            if (isRight)
            {
                isRight = false;
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            if (!isRight)
            {
                isRight = true;
                transform.localScale = new Vector2(1, 1);
            }
        }
    }
    protected void ProcessStun()
    {
        if(stunned > 0f)
        {
            stunned -= Time.fixedDeltaTime;
        }
    }
    protected virtual void Move()
    {
        if (stunned > 0f) { return; }
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb.velocity = CalculateMovementVelocity(direction) + CalculateKnockBack();
    }
    protected Vector3 CalculateMovementVelocity(Vector3 direction)
    {
        return direction * stats.moveSpeed * (stunned > 0f || isDeath ? 0f: 1f);
    }
    protected Vector3 CalculateKnockBack()
    {
        if(knockbackTimeWeight > 0f)
        {
            knockbackTimeWeight -= Time.fixedDeltaTime;
        }

        return knockbackVector * knockbackForce * (knockbackTimeWeight > 0f ? 1f : 0f);
    }
    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }
    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }
    public void TakeDamage(int damage)
    {
        stats.hp -= damage;
        if (stats.hp < 1)
        {
            targetCharacter.AddKilled();
            Defeated();
        }
    }
    private void Defeated()
    {
        isDeath = true;
        enemyFade.Death();
        _boxCol.enabled = false;
    }
    public void ReturnToPool()
    {
        dropManager.ExpDrop(transform.position, stats.experienceReward);
        dropManager.Drop(transform.position, dropData, _chanceDrop);
        if (poolMember != null)
        {
            poolMember.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnEnable()
    {
        if(enemyFade != null)
        {
            isDeath = false;
            enemyFade.Fire(false);
            _boxCol.enabled = true;
        }
    }
    public void Stun(float stun)
    {
        if(stun == 0) { return; }

        stunned = stun;
        rb.velocity = Vector2.zero;
    }
    public void Knockback(Vector2 vector, float force, float timeWeight)
    {
        if(force == 0) { return; }

        knockbackVector = vector;
        knockbackForce = force;
        knockbackTimeWeight = timeWeight;
    }
    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, attackArea);
    }
}