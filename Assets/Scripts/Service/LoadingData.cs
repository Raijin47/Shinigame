using UnityEngine;
using YG;

public class LoadingData : MonoBehaviour
{
    [SerializeField] private DataContainer data;
    [SerializeField] private CharacterData[] chara;
    [SerializeField] private LanguageSettings languageSettings;
    [SerializeField] private Animator stub;

    private readonly string startGame = "PanelStubOut";
    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;
    private void Awake()
    {
        Time.timeScale = 1f;
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
        if(YandexGame.savesData.isFirstSession)
        {
            YandexGame.savesData.language = YandexGame.EnvironmentData.language;
        }
        languageSettings.SetLanguage(YandexGame.savesData.language);
        stub.Play(startGame);
    }
}