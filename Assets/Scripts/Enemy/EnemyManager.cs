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
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private Character chara;
    [SerializeField] private DropManager dropManager;

    [SerializeField] private float offset;

    private StageProgress stageProgress;
    private GameObject player;

    private Vector3 min;
    private Vector3 max;
    private Vector3 center;
    private Vector3 size;
    private Vector3 halfSize;
    private Vector3 borderPoint;

    List<Enemy> bossEnemiesList;
    List<EnemySpawnGroup> enemySpawnGroupList;
    List<EnemySpawnGroup> repeatedSpawnGroupList;

    private int totalBossHealth;
    private int currentBossHealth;
    private int spawnPerFrame = 2;

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
    private Vector3 GenerateSpawnPos()
    {
        var ray = Camera.main.ScreenPointToRay(Vector2.zero);
        var ray2 = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height));

        if (Physics.Raycast(ray2, out RaycastHit hit2, 100))
        {
            max = hit2.point;
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            min = hit.point;
        }

        center = (min + max) / 2;
        size = max - min;
        halfSize = size / 2;

        borderPoint = RandomPoint();

        return borderPoint;
    }
    private Vector3 RandomPoint()
    {
        var x = center.x;
        var y = center.y;
        var halfOffset = offset / 2;

        if (Random.value > .5f)
        {
            x -= Random.value > .5f ? -halfSize.x - halfOffset : halfSize.x + halfOffset;
            y -= Random.Range(-halfSize.y, halfSize.y);
        }
        else
        {
            x -= Random.Range(-halfSize.x, halfSize.x);
            y -= Random.value > .5f ? -halfSize.y - halfOffset : halfSize.y + halfOffset;
        }

        return new Vector3(x, y);
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
        Vector3 position = GenerateSpawnPos();

        GameObject newEnemy = poolManager.GetObject(enemyToSpawn.poolObjectData);
        newEnemy.transform.position = position;

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player, chara, dropManager);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);
        if(isBoss)
        {
            SpawnBossEnemy(newEnemyComponent);
        }

        newEnemy.transform.parent = transform;
    }
    private void SpawnBossEnemy(Enemy newBoss)
    {
        if(bossEnemiesList == null) { bossEnemiesList = new List<Enemy>(); }
        bossEnemiesList.Add(newBoss);
        totalBossHealth += newBoss.stats.hp;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(min, .3f);

        //Gizmos.color = Color.blue;
        //Gizmos.DrawSphere(max, .3f);

        //Gizmos.color = new Color(0, 1, 0, .3f);
        //Gizmos.DrawCube(center, size);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size + Vector3.one * offset);
        //Gizmos.DrawSphere(borderPoint, .3f);
    }
}