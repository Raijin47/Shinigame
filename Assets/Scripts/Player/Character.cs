using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private StatusBar hpBar;
    [SerializeField] private WeaponManager wpnManager;
    [SerializeField] private TextMeshProUGUI textEnemy;
    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private PlayerMovement playerMovement;
    
    // default stats
    [HideInInspector] public float damageBonus;
    [HideInInspector] public float attackSpeedBonus;
    [HideInInspector] public float attackAreaSizeBonus;
    [HideInInspector] public float projectileSpeedBonus;
    [HideInInspector] public float durationBonus;
    [HideInInspector] public int projectileCountBonus;
    private float armor;
    private int recoveryHp;
    private int maxHp;

    //ItemStats
    [HideInInspector] public float damageItem = 1;
    [HideInInspector] public float attackSpeedItem = 1;
    [HideInInspector] public float attackAreaSizeItem = 1;
    [HideInInspector] public float projectileSpeedItem = 1;
    [HideInInspector] public float movementSpeedItem = 1;
    [HideInInspector] public float experienceItem = 1;
    [HideInInspector] public float soulsItem = 1;
    [HideInInspector] public float healthItem = 1;
    [HideInInspector] public float durationItem = 1;
    [HideInInspector] public int recoveryItem;
    [HideInInspector] public int projectileCountItem;
    [HideInInspector] public float armorItem;

    private float adsDamage = 1;
    private float adsHp = 1;
    private float adsCoin = 1;

    private float hpRegenerationRate = 1f;
    private float hpRegenerationTimer;
    private int currentHp;
    [HideInInspector] public int enemyKilled = 0;
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
        currentHp = maxHp;
        hpBar.SetState(currentHp, maxHp);
    }
    private void LoadSelectedCharacter(CharacterData selectedCharacter)
    {
        InitAnimation(selectedCharacter.spritePrefab);
        GetComponent<Level>().AddStartWeapon(selectedCharacter.startingWeapon);
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
        
        int damageUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.Damage);
        float damageCharaBase = data.selectedCharacter.Damage;
        damageBonus = (damageCharaBase + damageUpgradeLevel * 0.06f + charaLevel * 0.03f) * damageItem * adsDamage;

        int attackSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackSpeed);
        attackSpeedBonus = (1 + attackSpeedUpgradeLevel * 0.02f) * attackSpeedItem;

        int attackAreaSizeUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackAreaSize);
        attackAreaSizeBonus = (1 + attackAreaSizeUpgradeLevel * 0.05f) * attackAreaSizeItem;

        int projectileSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ProjectileSpeed);
        projectileSpeedBonus = (1 + 0.05f * projectileSpeedUpgradeLevel) * projectileSpeedItem;

        int movementSpeedUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.MovementSpeed);
        float movementSpeedBase = data.selectedCharacter.MovementSpeed;
        float speed = (movementSpeedBase * (1 + 0.03f * movementSpeedUpgradeLevel)) * movementSpeedItem;
        playerMovement.SetSpeed(speed);

        int ExperienceBoostUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ExperienceBoost);
        float boostExp = (1 + ExperienceBoostUpgradeLevel * 0.05f) * experienceItem;
        level.SetBoost(boostExp);

        float soulsUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.GoldBoost);
        float soulsBoost = (1 + soulsUpgradeLevel * 0.05f) * soulsItem * adsCoin;
        coins.SetBoost(soulsBoost);

        int healthCharaBase = data.selectedCharacter.Health;
        int hpUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.HP);
        maxHp = (int)((1 + hpUpgradeLevel * 0.1f + charaLevel * 0.03f) * healthCharaBase * healthItem * adsHp);

        int recoveryHpUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.RecoveryHP);
        recoveryHp = recoveryHpUpgradeLevel + recoveryItem;

        int projectileCountUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.ProjectileCount);
        projectileCountBonus = projectileCountUpgradeLevel + projectileCountItem;

        int armorUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.Armor);
        armor = 1f - (armorUpgradeLevel * 0.04f + armorItem);

        int durationUpgradeLevel = data.GetUpgradeLevel(PlayerPersisrentUpgrades.Duration);
        durationBonus = (1 + durationUpgradeLevel * 0.03f) * durationItem;

        wpnManager.RecalculateWeapons();
    }
    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;
        if (hpRegenerationTimer > 1f)
        {
            Heal(recoveryHp);
            hpRegenerationTimer = 0;
        }
    }
    public void TakeDamage(int damage)
    {
        if(isDeath) { return; }
        int dmg = (int)(damage * armor);
        currentHp -= dmg;

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
    public void AddPersistance(UpgradeData data)
    {
        coins.Add(data.persistanceStats.SoulAmount);
        Heal(data.persistanceStats.HealAmount);
    }
    public void AddKilled()
    {
        enemyKilled++;
        textEnemy.text = enemyKilled.ToString();
    }
    public void AdsBoost()
    {
        adsDamage = 1.3f;
        adsHp = 1.3f;
        adsCoin = 1.3f;

        CalculateStats();
        currentHp = maxHp;
        hpBar.SetState(currentHp, maxHp);
    }
}