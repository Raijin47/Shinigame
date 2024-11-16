using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Plugins.Audio.Core;
using Plugins.Audio.Utils;

public class EnemySpawnGroup
{
    public EnemyData EnemyData;
    public int Count;
    public bool IsBoss;

    public float RepeatTimer;
    public float TimeBetweenSpawn;
    public int RepeatCount;

    public EnemySpawnGroup(EnemyData enemyData, int count, bool isBoss)
    {
        EnemyData = enemyData;
        Count = count;
        IsBoss = isBoss;
    }
    public void SetRepeatSpawn(float timeBetweenSpawns, int repeatCount)
    {
        TimeBetweenSpawn = timeBetweenSpawns;
        RepeatCount = repeatCount;
        RepeatTimer = TimeBetweenSpawn;
    }
}

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float _offset;
    [SerializeField] private int _enemyLimit;

    [SerializeField] private Vector2 _spawnArea;

    [SerializeField] private PoolManager _poolManager;
    [SerializeField] private Slider _bossHealthBar;
    [SerializeField] private Character _chara;
    [SerializeField] private DropManager _dropManager;
    [SerializeField] private GameObject _barrierZone;
    [SerializeField] private TextMeshProUGUI _textBossHealth;
    [SerializeField] private SourceAudio _source;
    [SerializeField] private AudioDataProperty _clip;

    private int _enemyCount;
    private int _totalBossHealth;
    private int _currentBossHealth;
    private int _spawnPerFrame = 2;

    private Vector3 _min;
    private Vector3 _max;
    private Vector3 _center;
    private Vector3 _size;
    private Vector3 _halfSize;
    private Vector3 _borderPoint;

    private StageProgress _stageProgress;
    private GameObject _player;
    private StageTime _stageTimer;

    private List<Enemy> _bossEnemiesList;
    private List<EnemySpawnGroup> _enemySpawnGroupList;
    private List<EnemySpawnGroup> _repeatedSpawnGroupList;

    public void AddGroupToSpawn(EnemyData enemyToSpawn, int count, bool isBoss)
    {
        EnemySpawnGroup newGroupToSpawn = new EnemySpawnGroup(enemyToSpawn, count, isBoss);

        if (_enemySpawnGroupList == null) { _enemySpawnGroupList = new List<EnemySpawnGroup>(); }

        _enemySpawnGroupList.Add(newGroupToSpawn);
    }
    public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        var repeatSpawnGroup = new EnemySpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds, stageEvent.repeatCount);

        if (_repeatedSpawnGroupList == null)
        {
            _repeatedSpawnGroupList = new List<EnemySpawnGroup>();
        }

        _repeatedSpawnGroupList.Add(repeatSpawnGroup);
    }
    public void AudioPlay()
    {
        _source.Play(_clip.Key);
    }
    public void RemoveEnemy()
    {
        _enemyCount--;
    }
    
    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
        if(_enemyCount > _enemyLimit && !isBoss) return;
        _enemyCount++;

        var position = GenerateSpawnPos();

        GameObject newEnemy = _poolManager.GetObject(enemyToSpawn.poolObjectData);
        newEnemy.transform.position = position;

        var newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(_player, _chara, _dropManager);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(_stageProgress.Progress);
        newEnemyComponent.Activate();
        if (isBoss)
        {
            SpawnBossEnemy(newEnemyComponent, enemyToSpawn);
        }

        newEnemy.transform.parent = transform;
    }
    public void SpawnEnemy(EnemyData enemyToSpawn)
    {
        var position = GenerateSpawnPos();

        GameObject newEnemy = _poolManager.GetObject(enemyToSpawn.poolObjectData);
        newEnemy.transform.position = position;

        var newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(_player, _chara, _dropManager);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(_stageProgress.Progress);
        newEnemyComponent.Activate();

        newEnemy.transform.parent = transform;
    }
    private Vector3 GenerateSpawnPos()
    {
        var ray = Camera.main.ScreenPointToRay(Vector2.zero);
        var ray2 = Camera.main.ScreenPointToRay(new Vector3(Screen.width, Screen.height));

        if (Physics.Raycast(ray2, out RaycastHit hit2, 100))
        {
            _max = hit2.point;
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            _min = hit.point;
        }

        _center = (_min + _max) / 2;
        _size = _max - _min;
        _halfSize = _size / 2;

        _borderPoint = RandomPoint();

        return _borderPoint;
    }
    private Vector3 RandomPoint()
    {
        var x = _center.x;
        var y = _center.y;
        var halfOffset = _offset / 2;

        if (Random.value > .5f)
        {
            x -= Random.value > .5f ? -_halfSize.x - halfOffset : _halfSize.x + halfOffset;
            y -= Random.Range(-_halfSize.y, _halfSize.y);
        }
        else
        {
            x -= Random.Range(-_halfSize.x, _halfSize.x);
            y -= Random.value > .5f ? -_halfSize.y - halfOffset : _halfSize.y + halfOffset;
        }

        return new Vector3(x, y);
    }
    private void ProcessRepeatedSpawnGroups()
    {
        if(_repeatedSpawnGroupList == null) { return; }
        for(int i = _repeatedSpawnGroupList.Count - 1; i >= 0; i--)
        {
            _repeatedSpawnGroupList[i].RepeatTimer -= Time.deltaTime;
            if(_repeatedSpawnGroupList[i].RepeatTimer < 0)
            {
                _repeatedSpawnGroupList[i].RepeatTimer = _repeatedSpawnGroupList[i].TimeBetweenSpawn;
                AddGroupToSpawn(_repeatedSpawnGroupList[i].EnemyData, _repeatedSpawnGroupList[i].Count, _repeatedSpawnGroupList[i].IsBoss);
                _repeatedSpawnGroupList[i].RepeatCount -= 1;
                if(_repeatedSpawnGroupList[i].RepeatCount <=0)
                {
                    _repeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }
    private void ProcessSpawn()
    {
        if(_enemySpawnGroupList == null) { return; }

        for(int i = 0; i < _spawnPerFrame; i++)
        {
            if (_enemySpawnGroupList.Count > 0)
            {
                if (_enemySpawnGroupList[0].Count <= 0) { return; }
                SpawnEnemy(_enemySpawnGroupList[0].EnemyData, _enemySpawnGroupList[0].IsBoss);
                _enemySpawnGroupList[0].Count -= 1;

                if (_enemySpawnGroupList[0].Count <= 0)
                {
                    _enemySpawnGroupList.RemoveAt(0);
                }
            }
        }
    }
    private void UpdateBossHealth()
    {
        if(_bossEnemiesList == null) { return; }
        if(_bossEnemiesList.Count == 0) { return; }

        _currentBossHealth = 0;

        for(int i= 0; i < _bossEnemiesList.Count; i++)
        {
            if(_bossEnemiesList[i] == null) { continue; }
            _currentBossHealth += _bossEnemiesList[i].Stats.Hp;
        }

        _bossHealthBar.value = _currentBossHealth;
        _textBossHealth.text = _currentBossHealth.ToString() + " / " + _totalBossHealth.ToString();

        if (_currentBossHealth <= 0)
        {
            _totalBossHealth = 0;
            _bossHealthBar.gameObject.SetActive(false);
            _bossEnemiesList.Clear();

            _barrierZone.SetActive(false);
            _stageTimer.BossBattle(false);
        }
    }

    private void SpawnBossEnemy(Enemy newBoss, EnemyData enemyData)
    {
        if(_bossEnemiesList == null) { _bossEnemiesList = new List<Enemy>(); }
        _bossEnemiesList.Add(newBoss);

        _totalBossHealth += newBoss.Stats.Hp;

        _bossHealthBar.gameObject.SetActive(true);
        _bossHealthBar.maxValue = _totalBossHealth;

        _barrierZone.transform.position = _chara.transform.position;
        _barrierZone.SetActive(true);
        _stageTimer.BossBattle(true, enemyData);
    }

    private void Start()
    {
        _player = GameManager.instance.playerTransform.gameObject;
        _bossHealthBar = FindObjectOfType<BossHPBar>(true).GetComponent<Slider>();
        _stageProgress = FindObjectOfType<StageProgress>();
        _stageTimer = FindObjectOfType<StageTime>();
    }
    private void Update()
    {
        UpdateBossHealth();
        ProcessSpawn();
        ProcessRepeatedSpawnGroups();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_center, _size + Vector3.one * _offset);
    }
}