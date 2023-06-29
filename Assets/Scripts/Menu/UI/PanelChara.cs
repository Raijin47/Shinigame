using UnityEngine;
using Assets.SimpleLocalization;
using TMPro;

public class PanelChara : MonoBehaviour
{
    [SerializeField] private DataContainer data;
    [SerializeField] private CharacterData[] charaData;
    [SerializeField] private LocalizedDynamic nameChara;
    [SerializeField] private TextMeshProUGUI levelChara;
    [SerializeField] private GameObject panelStage;
    private int selectedUpgradeID;
    private void Start()
    {
        UpdateUI(charaData[0]);
    }

    private void OnEnable()
    {
        selectedUpgradeID = -1;
    }

    private void UpdateUI(CharacterData getData)
    {
        data.SetSelectedCharacter(getData);
        nameChara.Localize(getData.Name);
        levelChara.text = getData.Level.ToString();
    }
    public void Selected(int pressedButtonID)
    {
        if (selectedUpgradeID != pressedButtonID)
        {
            selectedUpgradeID = pressedButtonID;
            UpdateUI(charaData[pressedButtonID]);
        }
        else
        {
            panelStage.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}