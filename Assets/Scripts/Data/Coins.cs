using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsCountText;
    private float _soulBoost;
    private DataContainer data;
    private int _coins;

    private void Start()
    {
        _coins = 0;
        data = EssentialService.instance.dataContainer;
    }
    public void SetBoost(float coinBoost)
    {
        _soulBoost = coinBoost;
    }

    public void Add(int count)
    {
        _coins += (int)(count * _soulBoost);
        data.souls += (int)(count * _soulBoost);
        coinsCountText.text = _coins.ToString();
    }
}