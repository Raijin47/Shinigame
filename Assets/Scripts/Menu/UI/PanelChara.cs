using UnityEngine;
using Assets.SimpleLocalization;
using TMPro;

public class PanelChara : MonoBehaviour
{
    [SerializeField] private DataContainer data;
    [SerializeField] private LocalizedDynamic nameChara;
    [SerializeField] private TextMeshProUGUI levelChara;
    [SerializeField] private TextMeshProUGUI healthChara;
    [SerializeField] private TextMeshProUGUI movementChara;
    [SerializeField] private TextMeshProUGUI damageChara;
    [SerializeField] private LocalizedDynamic weaponName;
    [SerializeField] private LocalizedDynamic weaponDescription;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private UpdateDescription updateDescription;
    [SerializeField] private SelectionCharaButton[] buttons;

    public void UpdateUI(CharacterData getData)
    {
        data.SetSelectedCharacter(getData);
        nameChara.Localize(getData.Name);
        levelChara.text = getData.Level.ToString();
        healthChara.text = getData.Health.ToString();
        movementChara.text = getData.MovementSpeed.ToString();
        damageChara.text = getData.Damage.ToString();
        weaponName.Localize(getData.WeaponName);
        weaponDescription.Localize(getData.WeaponDescription);

        updateDescription.AnimEnd();
    }
}