using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnGroup
{
    public EnemyData enemyData;
    public int count;
    public bool isBoss;

    public float repeatTimer;
    public float timeBetweenSpawn;
    public int repeatCount;

    public EnemySpawnGroup(EnemyData enemyData, int count, bool isBoss)
    {
        this.enemyData = enemyData;
        this.count = count;
        this.isBoss = isBoss;
    }

    public void SetRepeatSpawn(float timeBetweenSpawns, int repeatCount)
    {
        this.timeBetweenSpawn = timeBetweenSpawns;
        this.repeatCount = repeatCount;
        repeatTimer = timeBetweenSpawn;
    }
}

public class EnemyManager : MonoBehaviour
{
    StageProgress stageProgress;
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    GameObject player;

    List<Enemy> bossEnemiesList;
    int totalBossHealth;
    int currentBossHealth;
    [SerializeField] private Slider bossHealthBar;

    List<EnemySpawnGroup> enemySpawnGroupList;
    List<EnemySpawnGroup> repeatedSpawnGroupList;

    int spawnPerFrame = 2;
    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        bossHealthBar = FindObjectOfType<BossHPBar>(true).GetComponent<Slider>();
        stageProgress = FindObjectOfType<StageProgress>();
    }
    private void Update()
    {
        UpdateBossHealth();
        ProcessSpawn();
        ProcessRepeatedSpawnGroups();
    }

    private void ProcessRepeatedSpawnGroups()
    {
        if(repeatedSpawnGroupList == null) { return; }
        for(int i = repeatedSpawnGroupList.Count - 1; i >= 0; i--)
        {
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if(repeatedSpawnGroupList[i].repeatTimer < 0)
            {
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;
                AddGroupToSpawn(repeatedSpawnGroupList[i].enemyData, repeatedSpawnGroupList[i].count, repeatedSpawnGroupList[i].isBoss);
                repeatedSpawnGroupList[i].repeatCount -= 1;
                if(repeatedSpawnGroupList[i].repeatCount <=0)
                {
                    repeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }

    private void ProcessSpawn()
    {
        if(enemySpawnGroupList == null) { return; }

        for(int i = 0; i < spawnPerFrame; i++)
        {
            if (enemySpawnGroupList.Count > 0)
            {
                if (enemySpawnGroupList[0].count <= 0) { return; }
                SpawnEnemy(enemySpawnGroupList[0].enemyData, enemySpawnGroupList[0].isBoss);
                enemySpawnGroupList[0].count -= 1;

                if (enemySpawnGroupList[0].count <= 0)
                {
                    enemySpawnGroupList.RemoveAt(0);
                }
            }
        }
    }

    private void UpdateBossHealth()
    {
        if(bossEnemiesList == null) { return; }
        if(bossEnemiesList.Count == 0) { return; }

        currentBossHealth = 0;

        for(int i= 0; i < bossEnemiesList.Count; i++)
        {
            if(bossEnemiesList[i] == null) { continue; }
            currentBossHealth += bossEnemiesList[i].stats.hp;
        }

        bossHealthBar.value = currentBossHealth;

        if(currentBossHealth <= 0)
        {
            bossHealthBar.gameObject.SetActive(false);
            bossEnemiesList.Clear();
        }
    }

    public void AddGroupToSpawn(EnemyData enemyToSpawn, int count, bool isBoss)
    {
        EnemySpawnGroup newGroupToSpawn = new EnemySpawnGroup(enemyToSpawn, count, isBoss);

        if(enemySpawnGroupList == null) { enemySpawnGroupList = new List<EnemySpawnGroup>(); }

        enemySpawnGroupList.Add(newGroupToSpawn);
    }

    public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        EnemySpawnGroup repeatSpawnGroup = new EnemySpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds, stageEvent.repeatCount);

        if(repeatedSpawnGroupList == null)
        {
            repeatedSpawnGroupList = new List<EnemySpawnGroup>();
        }

        repeatedSpawnGroupList.Add(repeatSpawnGroup);
    }

    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
        Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea);

        position += player.transform.position;

        // �����
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);
        if(isBoss)
        {
            SpawnBossEnemy(newEnemyComponent);
        }

        newEnemy.transform.parent = transform;
        // �����

        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = newEnemy.transform;
        spriteObject.transform.localPosition = Vector2.zero;
    }

    private void SpawnBossEnemy(Enemy newBoss)
    {
        if(bossEnemiesList == null) { bossEnemiesList = new List<Enemy>(); }
        bossEnemiesList.Add(newBoss);
        totalBossHealth += newBoss.stats.hp;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }
}