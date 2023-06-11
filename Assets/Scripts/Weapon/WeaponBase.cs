using UnityEngine;

public enum DirectionOfAttack
{
    Forward,
    LeftRight,
    UpDown,
    None
}
public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] DirectionOfAttack attackDirection;

    public WeaponData weaponData;
    public WeaponStats weaponStats;
    public Vector2 vectorOfAttack;

    private Character wielder;
    private PoolManager poolManager;
    protected PlayerMovement playerMove;

    private float timer;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMovement>();
    }
    public void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0f)
        {
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;

        weaponStats = new WeaponStats(wd.stats);
    }

    public void SetPoolManager(PoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    public abstract void Attack();

    public int GetDamage()
    {
        int damage = (int)(weaponData.stats.damage * wielder.damageBonus);
        return damage;
    }
    public virtual void PostDamage(int damage, Vector2 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    public void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }

    public void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();
        for (int i = 0; i < colliders.Length; i++)
        {
            IDamageable e = colliders[i].GetComponent<IDamageable>();
            if (e != null)
            {
                ApplyDamage(colliders[i].transform.position, damage, e);
            }
        }
    }

    public void ApplyDamage(Vector2 position, int damage, IDamageable e)
    {
        PostDamage(damage, position);
        e.TakeDamage(damage);
        ApplyAdditionalEffects(e, position);
    }

    private void ApplyAdditionalEffects(IDamageable e, Vector3 enemyPosition)
    {
        e.Stun(weaponStats.stun);
        e.Knockback((enemyPosition - transform.position).normalized, weaponStats.knockback, weaponStats.knockbackTimeWeight);
    }

    public void UpdateVectorOfAttack()
    {
        if(attackDirection == DirectionOfAttack.None)
        {
            vectorOfAttack = Vector2.zero;
            return;
        }

        switch (attackDirection)
        {
            case DirectionOfAttack.Forward:
                vectorOfAttack.x = playerMove.lastHorizontalCoupledVector;
                vectorOfAttack.y = playerMove.lastVerticalCoupledVector;
                break;

            case DirectionOfAttack.LeftRight:
                vectorOfAttack.x = playerMove.lastHorizontalDeCoupledVector;
                vectorOfAttack.y = 0f;
                break;

            case DirectionOfAttack.UpDown:
                vectorOfAttack.x = 0f;
                vectorOfAttack.y = playerMove.lastVerticalDeCoupledVector;
                break;
        }
        vectorOfAttack = vectorOfAttack.normalized;
    }

    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector2 position)
    {
        GameObject projectileGO = poolManager.GetObject(poolObjectData);

        projectileGO.transform.position = position;

        Projectile projectile = projectileGO.GetComponent<Projectile>();

        projectile.SetDirection(vectorOfAttack.x, vectorOfAttack.y);
        projectile.SetStats(this);

        return projectileGO;
    }
}