using UnityEngine;
using TMPro;
public class UpgradeDescriptionPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] TextMeshProUGUI upgradeDescription;

    public void Set(UpgradeData upgradeData)
    {
        upgradeNameText.text = upgradeData.Name;
        upgradeDescription.text = upgradeData.Description;
    }
}