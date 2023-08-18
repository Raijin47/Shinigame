using UnityEngine;

public class PanelUpgrades : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeUIElement[] upgradeElements;

    public void UpdateElement()
    {
        for(int i = 0; i < upgradeElements.Length; i++)
        {
            upgradeElements[i].UpdateElement();
        }
    }
}