using Assets.SimpleLocalization;
using UnityEngine;
using YG;
public class LanguageSettings: MonoBehaviour
{
    [SerializeField] private Animator[] buttons;
    private int currentID = -1;
    private readonly string Select = "Highlighted";
    private readonly string Deselect = "Normal";

    private void Awake() => LocalizationManager.Read();
    public void SetLanguage(string lang)
    {
        if (currentID != -1) buttons[currentID].Play(Deselect);

        switch (lang)
        {
            case "ru": currentID = 0; break;
            case "en": currentID = 1; break;
            case "tr": currentID = 2; break;
        }

        buttons[currentID].Play(Select);

        LocalizationManager.Language = lang;
        YandexGame.savesData.language = lang;
    }
}