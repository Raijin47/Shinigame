using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, IPoolMember
{
    private PoolMember poolMember;
    protected WeaponBase weapon;
    protected Vector3 direction;
    protected float speed;
    protected int damage;
    private int numOfHits;
    [SerializeField] protected float timeToLive;
    protected float ttl;
    protected float size;
    [SerializeField] protected Vector2 projectSize;
    protected Vector2 projectileSize;
    public virtual void SetDirection(float dir_x, float dir_y)
    {
        ttl = timeToLive;
        transform.localScale = projectileSize;
    }
    protected virtual void Update()
    {
        Move();

        TimerToLive();
    }

    protected virtual void TimerToLive()
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

    public void SetStats(WeaponBase weaponBase)
    {
        weapon = weaponBase;
        speed = weaponBase.weaponStats.projectileSpeed;
        damage = weaponBase.GetDamage();
        numOfHits = weaponBase.weaponStats.numberOfHits;
        size = weaponBase.weaponStats.attackAreaSize;
        float boostSize = EssentialService.instance.character.attackAreaSizeBonus;
        projectileSize = projectSize * size * boostSize;
    }

    public virtual void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
}