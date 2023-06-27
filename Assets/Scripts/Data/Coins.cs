using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsCountText;
    private float _soulBoost;
    private DataContainer data;

    private void Start()
    {
        data = EssentialService.instance.dataContainer;
    }
    public void SetBoost(float coinBoost)
    {
        _soulBoost = coinBoost;
    }

    public void Add(int count)
    {
        data.souls += (int)(count * _soulBoost);
        coinsCountText.text = "Coins: " + data.souls.ToString();
    }
}