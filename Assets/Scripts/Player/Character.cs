using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private PlayerMovement playerMovement;

    // default stats
    public float damageBonus;
    public float attackSpeedBonus;
    public float attackAreaSizeBonus;
    public float projectileSpeedBonus;
    public int projectileCountBonus;
    [SerializeField]private int armor;
    [SerializeField]private int recoveryHp;
    [SerializeField]private int maxHp;
    // default stats
    //item stats
    public float damageItem = 1;
    public float attackSpeedItem = 1;
    public float attackAreaSizeItem = 1;
    public float projectileSpeedItem = 1;
    public float movementSpeedItem = 1;
    public float experienceItem = 1;
    public float soulsItem = 1;
    public float healthItem = 1;
    public int recoveryItem;
    public int projectileCountItem;
    public int armorItem;
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
        ApplyPersistantUpgrades();
        hpBar.SetState(currentHp, maxHp);
    }

    private void LoadSelectedCharacter(CharacterData selectedCharacter)
    {
        InitAnimation(selectedCharacter.spritePrefab);
        GetComponent<WeaponManager>().AddWeapon(selectedCharacter.startingWeapon);
        maxHp = selectedCharacter.Health;
    }

    private void InitAnimation(GameObject spritePrefab)
    {
        GameObject animObject = Instantiate(spritePrefab, transform);
        GetComponent<PlayerAnimate>().SetAnimate(animObject);
    }

    private void ApplyPersistantUpgrades()
    {
        DataContainer data = EssentialService.instance.dataContainer;
        //дополнить с учётом предметов
        int hpUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.HP);
        maxHp += maxHp / 10 * hpUpgradeLevel;
        currentHp = maxHp;
    
        float damageUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.Damage);
        float damageCharaBase = data.selectedCharacter.Damage;
        float damageCharaLevel = data.selectedCharacter.Level;
        if (damageCharaLevel > 30) damageCharaLevel = 30;
        damageBonus = damageCharaBase + damageUpgradeLevel * 0.06f + damageCharaLevel * 0.015f;

        int armorUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.Armor);
        armor = armorUpgradeLevel;

        int recoveryHpUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.RecoveryHP);
        recoveryHp = recoveryHpUpgradeLevel;

        float attackSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackSpeed);
        attackSpeedBonus = 1 + attackSpeedUpgradeLevel * 0.1f;

        float attackAreaSizeUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackAreaSize);
        attackAreaSizeBonus = 1 + attackAreaSizeUpgradeLevel * 0.05f;

        float movementSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.MovementSpeed);
        float movementSpeedBase = data.selectedCharacter.MovementSpeed;
        float speed = movementSpeedBase * (1 + 0.05f * movementSpeedUpgradeLevel);
        playerMovement.SetSpeed(speed);

        float goldBoostUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.GoldBoost);
        float goldBoost = 1 + goldBoostUpgradeLevel * 0.2f;
        coins.SetBoost(goldBoost);

        float ExperienceBoostUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ExperienceBoost);
        float boostExp = 1 + ExperienceBoostUpgradeLevel / 10;
        level.SetBoost(boostExp);

        int projectileCountUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ProjectileCount);
        projectileCountBonus = projectileCountUpgradeLevel;
        //float projectaleSpeed = 0;
        //int projectileCount = 1;
        //int reroll = 1;


    }

    public void CalculateStats()
    {
        DataContainer data = EssentialService.instance.dataContainer;

        //damage
        float damageUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.Damage);
        float damageCharaBase = data.selectedCharacter.Damage;
        float damageCharaLevel = data.selectedCharacter.Level;
        if (damageCharaLevel > 30) damageCharaLevel = 30;
        damageBonus = (damageCharaBase + damageUpgradeLevel * 0.06f + damageCharaLevel * 0.015f) * damageItem;
        //attackSpeed
        float attackSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackSpeed);
        attackSpeedBonus = 1 + attackSpeedUpgradeLevel * 0.1f;
        //attackAreaSize
        float attackAreaSizeUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackAreaSize);
        attackAreaSizeBonus = 1 + attackAreaSizeUpgradeLevel * 0.05f;
        //projectileSpeed

        //movementSpeed
        float movementSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.MovementSpeed);
        float movementSpeedBase = data.selectedCharacter.MovementSpeed;
        float speed = movementSpeedBase * (1 + 0.05f * movementSpeedUpgradeLevel);
        playerMovement.SetSpeed(speed);
        //experienceBoost
        float ExperienceBoostUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ExperienceBoost);
        float boostExp = 1 + ExperienceBoostUpgradeLevel / 10;
        level.SetBoost(boostExp);
        //soulsBoost
        float goldBoostUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.GoldBoost);
        float goldBoost = 1 + goldBoostUpgradeLevel * 0.2f;
        coins.SetBoost(goldBoost);
        //healh
        int hpUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.HP);
        maxHp += maxHp / 10 * hpUpgradeLevel;
        currentHp = maxHp;
        //recoveryHP
        int recoveryHpUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.RecoveryHP);
        recoveryHp = recoveryHpUpgradeLevel;
        //projectileCount
        int projectileCountUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ProjectileCount);
        projectileCountBonus = projectileCountUpgradeLevel;
        //armor
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