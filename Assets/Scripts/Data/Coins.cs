using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsCountText;
    private float _coinBoost;
    private DataContainer data;

    private void Start()
    {
        data = EssentialService.instance.dataContainer;
    }
    public void SetBoost(float coinBoost)
    {
        _coinBoost = coinBoost;
    }

    public void Add(int count)
    {
        data.coins += (int)(count * _coinBoost);
        coinsCountText.text = "Coins: " + data.coins.ToString();
    }
}