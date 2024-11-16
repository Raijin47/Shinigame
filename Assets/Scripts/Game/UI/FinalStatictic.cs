using UnityEngine;
using TMPro;
public class FinalStatictic : MonoBehaviour
{
    [SerializeField] private GameObject _adsButton;

    [SerializeField] private TextMeshProUGUI _valueEnemy;
    [SerializeField] private TextMeshProUGUI _valueTime;
    [SerializeField] private TextMeshProUGUI _valueGold;

    [SerializeField] private Coins _coins;
    [SerializeField] private Character _character;
    private StageTime _stageTime;

    public void Result()
    {
        if (_coins._coins != 0) _adsButton.SetActive(true);

        float endTime = Time.time;
        float resultTime = endTime - _stageTime._startTime;

        int minutes = (int)(resultTime / 60f);
        int seconds = (int)(resultTime % 60f);

        _valueEnemy.text = _character.enemyKilled.ToString();
        _valueTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        _valueGold.text = _coins._coins.ToString();
    }

    public void GetStageTime(StageTime stageTime)
    {
        _stageTime = stageTime;
    }

    public void RewardADs()
    {
        _coins.DoubleCoins();
        _valueGold.text = _coins._coins.ToString();
        SaveService.SaveGame();
    }
}