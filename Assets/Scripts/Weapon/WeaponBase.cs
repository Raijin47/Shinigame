using UnityEngine;

public enum DirectionOfAttack
{
    Random,
    Forward,
    LeftRight,
    UpDown,
    None
}
public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] DirectionOfAttack attackDirection;

    [HideInInspector] public WeaponData weaponData;
    public WeaponStats weaponStats;
    [HideInInspector] public Vector2 vectorOfAttack;

    protected Character wielder;
    protected PoolManager poolManager;
    protected PlayerMovement playerMove;
    private MessageSystem message;
    [SerializeField] private AudioSource _audio;
    private float timer;

    //currentWeaponStats
    [SerializeField] private float curTimer;
    [SerializeField] protected float duration;
    public float size;
    public int damage;
    [SerializeField] protected int numberOfAttacks;
    public float projectileSpeed;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMovement>();
    }
    private void Start()
    {
        message = EssentialService.instance.message;
    }
    protected virtual void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0f)
        {
            Attack();
            timer = curTimer;
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
    public virtual void Recalculate()
    {
        damage = (int)(weaponStats.damage * wielder.damageBonus);
        size = weaponStats.attackAreaSize * wielder.attackAreaSizeBonus;
        duration = weaponStats.duration * wielder.durationBonus;
        curTimer = weaponStats.timeToAttack / wielder.attackSpeedBonus;
        numberOfAttacks = weaponStats.numberOfAttacks + wielder.projectileCountBonus;
        projectileSpeed = weaponStats.projectileSpeed * wielder.projectileSpeedBonus;
    }
    public virtual void PostDamage(int damage, Vector2 targetPosition)
    {
        message.PostMessage(damage.ToString(), targetPosition);
    }
    public virtual void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }
    public void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }
    public void ApplyDamage(Collider2D[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == null)
                break;
            if (colliders[i].TryGetComponent(out IDamageable enemy))
            {
                ApplyDamage(colliders[i].transform.position, damage, enemy);
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
        e.Burn(weaponStats.timeBurn, weaponStats.damageBurn);
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

            case DirectionOfAttack.Random:
                Vector2 target = UtilityTools.GenerateRandomPositionSquarePattern(Vector2.one);
                vectorOfAttack.x = target.x;
                vectorOfAttack.y = target.y;
                break;
        }
        vectorOfAttack = vectorOfAttack.normalized;
    }
    protected void AudioPlay() => _audio.Play();
    protected void AudioStop() => _audio.Stop();
    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector2 position)
    {
        GameObject projectileGO = poolManager.GetObject(poolObjectData);

        projectileGO.transform.position = position;

        ProjectileBase projectile = projectileGO.GetComponent<ProjectileBase>();

        projectile.SetStats(this);
        projectile.SetDirection(vectorOfAttack.x, vectorOfAttack.y);


        return projectileGO;
    }
}