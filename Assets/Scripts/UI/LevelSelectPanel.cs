using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPanel : MonoBehaviour
{
    [SerializeField] List<Button> stageSelectButton;
    [SerializeField] DataContainer dataContainer;

    void UpdateButtons()
    {
        stageSelectButton[0].interactable = true;
        for(int i = 1; i < stageSelectButton.Count; i++)
        {
            if(dataContainer.stageCompletion[i - 1])
            {
                stageSelectButton[i].interactable = true;
            }
            else
            {
                stageSelectButton[i].interactable = false;
            }
        }
    }

    private void OnEnable()
    {
        UpdateButtons();
    }
}