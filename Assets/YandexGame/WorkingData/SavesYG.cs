using System;

namespace YG
{
    [Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        //  сохранения
        public int[] upgrades = new int[30];
        public int[] charaLvl = new int[30];
        public bool[] stage = new bool[12];
        public int souls;
        public int butterflies;

        public SavesYG()
        {
            stage[0] = true;
            charaLvl[0] = 1;
            souls = 100000;
            butterflies = 5;
        }
    }
}