using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Animator[] panels;
    [SerializeField] private Animator[] subPanels;
    [SerializeField] private int currentPanelIndex = 0;

    string panelFadeIn = "Panel In";
    string panelFadeOut = "Panel Out";
    void Start()
    {
        Time.timeScale = 1;
        panels[currentPanelIndex].Play(panelFadeIn);
    }
    public void PanelAnim(int newPanel)
    {
        panels[currentPanelIndex].Play(panelFadeOut);
        currentPanelIndex = newPanel;
        panels[currentPanelIndex].Play(panelFadeIn);
    }

    public void SubPanelAnim(int newPanel)
    {
        subPanels[newPanel].Play(panelFadeIn);
    }

    public void CloseSubPanel(int id)
    {
        subPanels[id].Play(panelFadeOut);
    }
}