using YG;

public static class SaveService
{
    public static DataContainer Data;
    public static void GetData(DataContainer getData)
    {
        if (Data == null) Data = getData;
    }
    public static void SaveUpgrade(PlayerPersisrentUpgrades upgrade)
    {
        int a = (int)upgrade;

        YandexGame.savesData.upgrades[a] = Data.upgrades[a].level;
        YandexGame.savesData.souls = Data.souls;

        YandexGame.SaveProgress();
    }
    public static void SaveGame()
    {
        YandexGame.savesData.souls = Data.souls;
        YandexGame.savesData.butterflies = Data.butterflies;

        YandexGame.SaveProgress();
    }
    public static void SaveChara(CharacterData chara)
    {
        YandexGame.savesData.charaLvl[chara.ID] = chara.Level;
        YandexGame.savesData.butterflies = Data.butterflies;

        YandexGame.SaveProgress();
    }
}