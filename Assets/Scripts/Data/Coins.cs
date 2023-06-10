using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    public int coinAcquired;
    [SerializeField] TextMeshProUGUI coinsCountText;
    public void Add(int count)
    {
        coinAcquired += count;
        coinsCountText.text = "Coins: " + coinAcquired.ToString();
    }
}