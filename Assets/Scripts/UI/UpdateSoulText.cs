using UnityEngine;
using TMPro;
public class UpdateSoulText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soulText;
    [SerializeField] private DataContainer data;
    void Update()
    {
        soulText.text = data.souls.ToString();
    }
}