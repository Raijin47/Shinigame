using Assets.SimpleLocalization;
using UnityEngine;
using YG;
public class LanguageSettings: MonoBehaviour
{
    [SerializeField] private Animator[] buttons;
    private int _currentID = -1;
    private int _id;
    private readonly string Select = "Highlighted";
    private readonly string Deselect = "Normal";

    private void Awake() => LocalizationManager.Read();
    public void SetLanguage(string lang)
    {
        switch (lang)
        {
            case "ru": _id = 0; break;
            case "en": _id = 1; break;
            case "tr": _id = 2; break;
        }

        if (_currentID == _id) { return; }

        if (_currentID != -1) buttons[_currentID].Play(Deselect);

        _currentID = _id;

        buttons[_currentID].Play(Select);

        LocalizationManager.Language = lang;
        YandexGame.savesData.language = lang;
    }
}