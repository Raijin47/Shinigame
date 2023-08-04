using TMPro;
using UnityEngine;

public class UpdateButterfliesText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI butterfliesText;
    [SerializeField] private DataContainer data;
    void Update()
    {
        butterfliesText.text = data.butterflies.ToString();
    }
}