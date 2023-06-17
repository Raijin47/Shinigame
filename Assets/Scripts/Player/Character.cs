using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private float hpRegenerationRate = 1f;
    private float hpRegenerationTimer;
    public float damageBonus;
    public float attackSpeedBonus;
    private int recoveryHp;
    private int maxHp;
    private int currentHp;
    [HideInInspector]public int armor;

    private bool isDeath = false;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
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
        int hpUpgradeLevel = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.HP);
        maxHp += maxHp / 10 * hpUpgradeLevel;
        currentHp = maxHp;
    
        float damageUpgradeLevel = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.Damage);
        float damageCharaBase = EssentialService.instance.dataContainer.selectedCharacter.Damage;
        float damageCharaLevel = EssentialService.instance.dataContainer.selectedCharacter.Level;
        damageBonus = damageCharaBase + damageUpgradeLevel * 0.06f + damageCharaLevel * 0.015f;

        int armorUpgradeLevel = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.Armor);
        armor = armorUpgradeLevel;

        int recoveryHpUpgradeLevel = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.RecoveryHP);
        recoveryHp = recoveryHpUpgradeLevel;

        float attackSpeed = EssentialService.instance.dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.AttackSpeed);
        attackSpeedBonus = attackSpeed / 20;
    }

    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;
        if (hpRegenerationTimer > 1f)
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
        if(damage < 0) { damage = 0; }
    }
}