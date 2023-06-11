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

    private float stunned;
    private float knockbackForce;
    private float knockbackTimeWeight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }
    void FixedUpdate()
    {
        ProcessStun();
        Move();
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
        return direction * stats.moveSpeed * (stunned > 0f ? 0f: 1f);
    }

    private Vector3 CalculateKnockBack()
    {
        if(knockbackTimeWeight > 0f)
        {
            knockbackTimeWeight -= Time.fixedDeltaTime;
        }

        return knockbackVector * knockbackForce * (knockbackTimeWeight > 0f ? 1f : 0f);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            Attack();
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
        targetGameObject.GetComponent<Level>().AddExperience(stats.experienceReward);
        GetComponent<DropOnDestroy>().CheckDrop();
        if(poolMember != null)
        {
            poolMember.ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
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