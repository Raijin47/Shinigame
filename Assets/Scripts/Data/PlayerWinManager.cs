using Assets.SimpleLocalization;
using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMessagePanel;
    [SerializeField] DataContainer dataContainer;
    [SerializeField] private FinalStatictic _finalStat;
    [SerializeField] private LocalizedDynamic _text;
    private PauseManager pauseManager;

    private void Start()
    {
        pauseManager = GetComponent<PauseManager>();
    }
    public void Win(int stageID)
    {
        _finalStat.gameObject.SetActive(true);
        _finalStat.Result();
        _text.Localize("YouWin");

        pauseManager.PauseGame(true);
        dataContainer.StageComplete(stageID);
        dataContainer.butterflies++;
        SaveService.SaveGame();
    }
}