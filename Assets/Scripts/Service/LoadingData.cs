using UnityEngine;
using YG;
using Assets.SimpleLocalization;

public class LoadingData : MonoBehaviour
{
    [SerializeField] private DataContainer data;
    [SerializeField] private CharacterData[] chara;

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;
    private void Awake()
    {
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
        LocalizationManager.Read();
        LocalizationManager.Language = YandexGame.savesData.language;
    }

    public void SaveData()
    {
        for(int i = 0; i < chara.Length; i++)
        {
            YandexGame.savesData.charaLvl[i] = chara[i].Level;
        }
        for(int i = 0; i < data.upgrades.Length; i++)
        {
            YandexGame.savesData.upgrades[i] = data.upgrades[i].level;
        }
        for (int i = 0; i < data.stageCompletion.Length; i++)
        {
            YandexGame.savesData.stage[i] = data.stageCompletion[i];
        }
        YandexGame.savesData.souls = data.souls;
        YandexGame.savesData.butterflies = data.butterflies;
        YandexGame.SaveProgress();
    }
}