using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IPoolMember
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    private PoolMember poolMember;
    [SerializeField] private float ttl;
    private int damage;
    public void SetDirection(Vector2 dir, int getDamage)
    {
        damage = getDamage;
        ttl = 10;
        direction = dir;
    }
    private void Update()
    {
        Move();
        TimerToLive();
    }
    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void TimerToLive()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if (poolMember == null)
        {
            Destroy(gameObject);
        }
        else
        {
            poolMember.ReturnToPool();
        }
    }
    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Character chara))
        {
            chara.TakeDamage(damage);
        }
    }
    public void ReboundDirection(Vector2 inNormal)
    {
        direction = Vector2.Reflect(direction, inNormal).normalized;
    }
}