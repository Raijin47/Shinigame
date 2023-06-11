using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private DataContainer dataContainer;
    [SerializeField] private StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;

    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;
    public float damageBonus;

    public int maxHp;
    public int currentHp;
    public int armor = 0;

    private bool isDeath = false;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }
    private void Start()
    {
        LoadSelectedCharacter(dataContainer.selectedCharacter);
        ApplyPersistantUpgrades();
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

    private void ApplyPersistantUpgrades()
    {
        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.HP);

        maxHp += maxHp / 10 * hpUpgradeLevel;
    
        int damageUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersisrentUpgrades.Damage);
        damageBonus = 1f + 0.1f * damageUpgradeLevel;
    }

    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;
        if (hpRegenerationTimer > 1f)
        {
            Heal(1);
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