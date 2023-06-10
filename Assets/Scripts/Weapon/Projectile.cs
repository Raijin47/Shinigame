using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolMember
{
    PoolMember poolMember;
    WeaponBase weapon;
    Vector3 direction;
    float speed;
    int damage;
    int numOfHits;
    public float attackArea = 0.3f;
    List<IDamageable> enemyHit;

    float ttl = 6f;

    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y);
        if (dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }
    void Update()
    {
        Move();

        if (Time.frameCount % 6 == 0)// ןנמגונךא םא ךאזהûי 2י פנויל
        {
            HitDetection();
        }

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

    private void HitDetection()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, attackArea);
        foreach (Collider2D c in hit)
        {
            if (numOfHits > 0)
            {
                IDamageable enemy = c.GetComponent<IDamageable>();
                if (enemy != null)
                {
                    if (CheckRepeatHit(enemy) == false)
                    {
                        weapon.ApplyDamage(c.transform.position, damage, enemy);
                        enemyHit.Add(enemy);
                        numOfHits--;
                    }
                }
            }
            else
            {
                break;
            }

        }
        if (numOfHits <= 0)
        {
            DestroyProjectile();
        }
    }

    private bool CheckRepeatHit(IDamageable enemy)
    {
        if (enemyHit == null) { enemyHit = new List<IDamageable>(); }

        return enemyHit.Contains(enemy);
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void PostDamage(int damage, Vector2 worldPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackArea);
    }

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
