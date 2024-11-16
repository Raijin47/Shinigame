using UnityEngine;

public class StageTime : MonoBehaviour
{
    [HideInInspector] public float time;
    private TimerUI _timer;
    private bool _isBossBattle;
    [HideInInspector] public float _startTime;

    private void Start()
    {
        _timer = EssentialService.instance.timerUI;
        _isBossBattle = false;
        _startTime = Time.time;
        EssentialService.instance.finalStatictic.GetStageTime(this);
    }
    private void Update()
    {
        if(_isBossBattle) { return; }
        time += Time.deltaTime;
        _timer.UpdateTime(time);
    }

    public void BossBattle(bool isBattle, EnemyData enemy)
    {
        _isBossBattle = isBattle;
        _timer.SetName(enemy.name);
    }
    public void BossBattle(bool isBattle) => _isBossBattle = isBattle;
}