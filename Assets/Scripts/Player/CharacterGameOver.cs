using Assets.SimpleLocalization;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private FinalStatictic _finalStat;
    [SerializeField] private GameObject weaponParent;
    [SerializeField] private LocalizedDynamic _text;
    public void GameOver()
    {
        //GetComponent<PlayerMovement>().enabled = false;
        _finalStat.gameObject.SetActive(true);
        _finalStat.Result();
        _text.Localize("GameOver");

        //weaponParent.SetActive(false);
        pauseManager.PauseGame(true);
        SaveService.SaveGame();
    }
}