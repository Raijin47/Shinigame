//using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolMember
{
    private PoolMember poolMember;
    private WeaponBase weapon;
    private Vector3 direction;
    private float speed;
    private int damage;
    private int numOfHits;
    //[SerializeField] float defaultAttackArea = 0.3f;
    //private float attackArea;
    //List<IDamageable> enemyHit;

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
        //attackArea = defaultAttackArea * boostSize;
    }
    private void Update()
    {
        Move();

        //if (Time.frameCount % 6 == 0)// ןנמגונךא םא ךאזהûי 2י פנויל
        //{
        //    HitDetection();
        //}

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

    private void OnTriggerEnter2D(Collider2D collision)
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



    //private void HitDetection()
    //{
    //    Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, attackArea);
    //    foreach (Collider2D c in hit)
    //    {
    //        if (numOfHits > 0)
    //        {
    //            IDamageable enemy = c.GetComponent<IDamageable>();
    //            if (enemy != null)
    //            {
    //                if (CheckRepeatHit(enemy) == false)
    //                {
    //                    weapon.ApplyDamage(c.transform.position, damage, enemy);
    //                    enemyHit.Add(enemy);
    //                    numOfHits--;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            break;
    //        }

    //    }
    //    if (numOfHits <= 0)
    //    {
    //        DestroyProjectile();
    //    }
    //}

    //private bool CheckRepeatHit(IDamageable enemy)
    //{
    //    if (enemyHit == null) { enemyHit = new List<IDamageable>(); }

    //    return enemyHit.Contains(enemy);
    //}

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void PostDamage(int damage, Vector2 worldPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackArea);
    //}

    public void SetStats(WeaponBase weaponBase)
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
