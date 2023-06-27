using UnityEngine;
using Assets.SimpleLocalization;

public class UpgradeDescriptionPanel : MonoBehaviour
{
    [SerializeField] LocalizedDynamic upgradeNameText;
    [SerializeField] LocalizedDynamic upgradeDescription;

    public void Set(UpgradeData upgradeData)
    {
        upgradeNameText.Localize(upgradeData.Name);
        upgradeDescription.Localize(upgradeData.Description);
    }
}