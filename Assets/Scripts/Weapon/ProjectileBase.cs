using UnityEngine;

public abstract class ProjectileBase: MonoBehaviour, IPoolMember
{
    private PoolMember poolMember;
    protected WeaponBase weapon;
    private Vector3 direction;
    protected float speed;
    protected int damage;
    private int numOfHits;

    private float ttl = 6f;
    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y);
        float angle = 0;

        switch(dir_x, dir_y)
        {
            case (1, 0):
                angle = 0;
                break;

            case (> 0, > 0):
                angle = 45;
                break;

            case (0, 1):
                angle = 90;
                break;

            case (< 0, > 0):
                angle = 135;
                break;

            case (-1, 0):
                angle = 180;
                break;

            case (< 0, < 0):
                angle = 225;
                break;

            case (0, -1):
                angle = 270;
                break;

            case ( > 0, < 0):
                angle = 315;
                break;
        }

        float boostSize = EssentialService.instance.character.attackAreaSizeBonus;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localScale = new Vector2(1 * boostSize, 1 * boostSize);
    }
    private void Update()
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

    private void DestroyProjectile()
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

    public void PostDamage(int damage, Vector2 worldPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition);
    }

    public virtual void SetStats(WeaponBase weaponBase)
    {
        weapon = weaponBase;
        speed = weaponBase.weaponStats.projectileSpeed;
        damage = weaponBase.GetDamage();
        numOfHits = weaponBase.weaponStats.numberOfHits;
    }

    private void OnEnable()
    {
        ttl = 6f;
    }

    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }
}
