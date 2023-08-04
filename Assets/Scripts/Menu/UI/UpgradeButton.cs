using Assets.SimpleLocalization;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] LocalizedDynamic upgradeNameText;
    [SerializeField] LocalizedDynamic upgradeDescription;

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        upgradeNameText.Localize(upgradeData.Name);
        upgradeDescription.Localize(upgradeData.Description);
    }

    internal void Clean()
    {
        icon.sprite = null;
    }
}