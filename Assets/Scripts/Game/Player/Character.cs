using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private PlayerMovement playerMovement;

    // default stats
    [HideInInspector] public float damageBonus;
    [HideInInspector] public float attackSpeedBonus;
    [HideInInspector] public float attackAreaSizeBonus;
    [HideInInspector] public float projectileSpeedBonus;
    [HideInInspector] public int projectileCountBonus;
    private int armor;
    private int recoveryHp;
    private int maxHp;
    // default stats
    //item stats
    [HideInInspector] public float damageItem = 1;
    [HideInInspector] public float attackSpeedItem = 1;
    [HideInInspector] public float attackAreaSizeItem = 1;
    [HideInInspector] public float projectileSpeedItem = 1;
    [HideInInspector] public float movementSpeedItem = 1;
    [HideInInspector] public float experienceItem = 1;
    [HideInInspector] public float soulsItem = 1;
    [HideInInspector] public float healthItem = 1;
    [HideInInspector] public int recoveryItem;
    [HideInInspector] public int projectileCountItem;
    [HideInInspector] public int armorItem;
    //itme stats

    private float hpRegenerationRate = 1f;
    private float hpRegenerationTimer;
    private int currentHp;

    private bool isDeath = false;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        LoadSelectedCharacter(EssentialService.instance.dataContainer.selectedCharacter);
        CalculateStats();
        hpBar.SetState(currentHp, maxHp);
    }

    private void LoadSelectedCharacter(CharacterData selectedCharacter)
    {
        InitAnimation(selectedCharacter.spritePrefab);
        GetComponent<WeaponManager>().AddWeapon(selectedCharacter.startingWeapon);
    }

    private void InitAnimation(GameObject spritePrefab)
    {
        GameObject animObject = Instantiate(spritePrefab, transform);
        GetComponent<PlayerAnimate>().SetAnimate(animObject);
    }

    public void CalculateStats()
    {
        DataContainer data = EssentialService.instance.dataContainer;
        int charaLevel = data.selectedCharacter.Level;
        if (charaLevel > 30) charaLevel = 30;

        float damageUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.Damage);
        float damageCharaBase = data.selectedCharacter.Damage;
        damageBonus = (damageCharaBase + damageUpgradeLevel * 0.06f + charaLevel * 0.015f) * damageItem;

        float attackSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackSpeed);
        attackSpeedBonus = (1 + attackSpeedUpgradeLevel * 0.1f) * attackSpeedItem;

        float attackAreaSizeUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackAreaSize);
        attackAreaSizeBonus = (1 + attackAreaSizeUpgradeLevel * 0.05f) * attackAreaSizeItem;

        float projectileSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ProjectileSpeed);
        projectileSpeedBonus = (1 + 0.05f * projectileSpeedUpgradeLevel) * projectileSpeedItem;

        float movementSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.MovementSpeed);
        float movementSpeedBase = data.selectedCharacter.MovementSpeed;
        float speed = (movementSpeedBase * (1 + 0.05f * movementSpeedUpgradeLevel)) * movementSpeedItem;
        playerMovement.SetSpeed(speed);

        float ExperienceBoostUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ExperienceBoost);
        float boostExp = (1 + ExperienceBoostUpgradeLevel * 0.2f) * experienceItem;
        level.SetBoost(boostExp);

        float soulsUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.GoldBoost);
        float soulsBoost = (1 + soulsUpgradeLevel * 0.2f) * soulsItem;
        coins.SetBoost(soulsBoost);

        int healthCharaBase = data.selectedCharacter.Health;
        int hpUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.HP);
        maxHp = (int)((1 + hpUpgradeLevel * 0.1f + charaLevel * 0.015f) * healthCharaBase * healthItem);
        currentHp = maxHp;

        int recoveryHpUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.RecoveryHP);
        recoveryHp = recoveryHpUpgradeLevel + recoveryItem;

        int projectileCountUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ProjectileCount);
        projectileCountBonus = projectileCountUpgradeLevel + projectileCountItem;

        int armorUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.Armor);
        armor = armorUpgradeLevel + armorItem;
    }


    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;
        if (hpRegenerationTimer > 3f)
        {
            Heal(recoveryHp);
            hpRegenerationTimer -= 1f;
        }
    }
    public void TakeDamage(int damage)
    {
        if(isDeath) { return; }
        ApplyArmor(ref damage);

        currentHp -= damage;

        if(currentHp <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDeath = true;
        }
        hpBar.SetState(currentHp, maxHp);
    }

    public void Heal(int amount)
    {
        if(currentHp <= 0) { return; }

        currentHp += amount;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hpBar.SetState(currentHp, maxHp);
    }

    private void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if(damage < 0) { damage = 1; }
    }
}