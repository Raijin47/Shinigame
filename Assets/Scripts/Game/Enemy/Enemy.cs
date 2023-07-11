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
        this.experienceReward = (int)(experienceReward * progress);
    }
}
public class Enemy : MonoBehaviour, IDamageable, IPoolMember
{
    public EnemyStats stats;
    [SerializeField] private EnemyData enemyData;

    private Rigidbody2D rb;
    private GameObject targetGameObject;
    private Character targetCharacter;
    private Transform targetDestination;
    private PoolMember poolMember;
    private Vector2 knockbackVector;
    private EnemyFade enemyFade; 
    private CapsuleCollider2D _capCol;
    private BoxCollider2D _boxCol;
    private MessageSystem message;
    private float stunned;
    private float burn;
    private float knockbackForce;
    private float knockbackTimeWeight;
    private float timeToAttack = .5f;
    private float currentTime;
    private float timeToBurn = 1;
    private float curTTB;
    private int burnDamage;

    private bool isAttack = false;
    private bool isBurn = false;
    private bool isRight = true;
    private bool isDeath = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        enemyFade = GetComponentInChildren<EnemyFade>();
        _capCol = GetComponent<CapsuleCollider2D>();
        _boxCol = GetComponent<BoxCollider2D>();
        message = EssentialService.instance.message;
    }
    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }

    private void Update()
    {
        if(isDeath) { return; }
        ProcessAttack();
        Flip();
        ProcessBurn();
    }
    public void Burn(float time, int damage)
    {
        isBurn = true;
        burn = time;
        burnDamage += damage;
        enemyFade.Fire(isBurn);   
    }
    private void ProcessBurn()
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

    private void FixedUpdate()
    {
        ProcessStun();
        Move();
    }
    private void Flip()
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

    private void ProcessAttack()
    {
        currentTime -= Time.deltaTime;
        if (isAttack && currentTime < 0)
        {
            Attack();
            currentTime = timeToAttack;
        }
    }
    private void ProcessStun()
    {
        if(stunned > 0f)
        {
            stunned -= Time.fixedDeltaTime;
        }
    }

    private void Move()
    {
        if (stunned > 0f) { return; }
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb.velocity = CalculateMovementVelocity(direction) + CalculateKnockBack();
    }

    private Vector3 CalculateMovementVelocity(Vector3 direction)
    {
        return direction * stats.moveSpeed * (stunned > 0f || isDeath ? 0f: 1f);
    }

    private Vector3 CalculateKnockBack()
    {
        if(knockbackTimeWeight > 0f)
        {
            knockbackTimeWeight -= Time.fixedDeltaTime;
        }

        return knockbackVector * knockbackForce * (knockbackTimeWeight > 0f ? 1f : 0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            isAttack = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            isAttack = false;
        }
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }
    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }
    void Attack()
    {
        if(targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }
        targetCharacter.TakeDamage(stats.damage);
    }

    public void TakeDamage(int damage)
    {
        stats.hp -= damage;
        if (stats.hp < 1)
        {
            Defeated();
        }
    }

    private void Defeated()
    {
        isDeath = true;
        enemyFade.Death(isDeath);
        _capCol.enabled = false;
        _boxCol.enabled = false;
    }
    public void ReturnToPool()
    {
        targetGameObject.GetComponent<Level>().AddExperience(stats.experienceReward);
        GetComponent<DropOnDestroy>().CheckDrop();
        if (poolMember != null)
        {
            poolMember.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if(enemyFade != null)
        {
            isDeath = false;
            enemyFade.Death(isDeath);
            enemyFade.Fire(false);
            _capCol.enabled = true;
            _boxCol.enabled = true;
        }
    }

    public void Stun(float stun)
    {
        stunned = stun;
    }

    public void Knockback(Vector2 vector, float force, float timeWeight)
    {
        knockbackVector = vector;
        knockbackForce = force;
        knockbackTimeWeight = timeWeight;
    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
}