using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsCountText;
    public int coinAcquired;

    public void Add(int count)
    {
        coinAcquired += count;
        coinsCountText.text = "Coins: " + coinAcquired.ToString();
    }
}