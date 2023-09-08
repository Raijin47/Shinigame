using UnityEngine;
using YG;

public class LoadingData : MonoBehaviour
{
    [SerializeField] private DataContainer data;
    [SerializeField] private CharacterData[] chara;
    [SerializeField] private LanguageSettings languageSettings;

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;
    private void Awake()
    {
        SaveService.GetData(data);
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }
    public void GetData()
    {
        for(int i = 0; i < chara.Length; i++)
        {
            chara[i].Level = YandexGame.savesData.charaLvl[i];
        }
        for(int i = 0; i < data.upgrades.Length; i++)
        {
            data.upgrades[i].level = YandexGame.savesData.upgrades[i];
        }
        for(int i = 0; i < data.stageCompletion.Length; i++)
        {
            data.stageCompletion[i] = YandexGame.savesData.stage[i];
        }

        data.souls = YandexGame.savesData.souls;
        data.butterflies = YandexGame.savesData.butterflies;
        languageSettings.SetLanguage(YandexGame.savesData.language);
    }
}