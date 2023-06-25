using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, IPoolMember
{
    private PoolMember poolMember;
    protected WeaponBase weapon;
    protected Vector3 direction;
    protected float speed;
    protected int damage;
    private int numOfHits;

    private float ttl = 6f;
    public virtual void SetDirection(float dir_x, float dir_y)
    {

    }
    protected void Update()
    {
        Move();

        TimerToLive();
    }

    private void TimerToLive()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            DestroyProjectile();
        }
    }

    protected virtual void DestroyProjectile()
    {
        if(poolMember == null)
        {
            Destroy(gameObject);
        }
        else
        {
            poolMember.ReturnToPool();
        }       
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(numOfHits > 0)
        {
            IDamageable enemy = collision.GetComponent<IDamageable>();

            if (collision.TryGetComponent(out IDamageable _))
            {
                weapon.ApplyDamage(collision.transform.position, damage, enemy);
                numOfHits--;
            }
        }
        if (numOfHits <= 0)
        {
            DestroyProjectile();
        }
    }

    protected virtual void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    //public void PostDamage(int damage, Vector2 worldPosition)
    //{
    //    MessageSystem.instance.PostMessage(damage.ToString(), worldPosition);
    //}

    public void SetStats(WeaponBase weaponBase)
    {
        weapon = weaponBase;
        speed = weaponBase.weaponStats.projectileSpeed;
        damage = weaponBase.GetDamage();
        numOfHits = weaponBase.weaponStats.numberOfHits;
    }

    protected void OnEnable()
    {
        ttl = 6f;
    }

    public virtual void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
}
