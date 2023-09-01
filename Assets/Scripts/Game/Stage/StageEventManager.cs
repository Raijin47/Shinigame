using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] private StageData stageData;

    private EnemyManager enemyManager;
    private StageTime stageTime;

    private int eventIndexer;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }
    private void Start()
    {
        enemyManager = EssentialService.instance.enemyManager;
    }
    private void Update()
    {
        if(eventIndexer >= stageData.stageEvents.Count) { return; }

        if(stageTime.time > stageData.stageEvents[eventIndexer].time)
        {
            switch (stageData.stageEvents[eventIndexer].eventType)
            {
                case StageEventType.SpawnEnemy:
                    SpawnEnemy(false);
                    break;

                case StageEventType.SpawnObject:
                    SpawnObject();
                    break;

                case StageEventType.WinStage:
                    WinStage();
                    break;

                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemyBoss();
                    break;
            }
            eventIndexer ++;
        }
    }

    private void SpawnEnemyBoss()
    {
        SpawnEnemy(true);
    }

    private void WinStage()
    {
        EssentialService.instance.playerWinManager.Win(stageData.stageID);
    }

    private void SpawnObject()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
        {
            Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
            positionToSpawn += UtilityTools.GenerateRandomPositionSquarePattern(new Vector2(5f, 5f));
            SpawnManager.instance.SpawnObject(positionToSpawn, stageData.stageEvents[eventIndexer].objectToSpawn);
        }
    }
    private void SpawnEnemy(bool bossEnemy)
    {
        StageEvent currentEvent = stageData.stageEvents[eventIndexer];
        enemyManager.AddGroupToSpawn(currentEvent.enemyToSpawn, currentEvent.count, bossEnemy);

        if(currentEvent.isRepeatedEvent)
        {
            enemyManager.AddRepeatedSpawn(currentEvent, bossEnemy);
        }
    }
}