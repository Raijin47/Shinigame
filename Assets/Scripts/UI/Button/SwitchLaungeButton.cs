using Assets.SimpleLocalization;
using UnityEngine;
using YG;
public class SwitchLaungeButton : MonoBehaviour
{
    public void SetLanguage(string lang)
    {
        LocalizationManager.Language = lang;
        YandexGame.savesData.language = lang;
        YandexGame.SaveProgress();
    }
}