using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum eEntityType
{
    Ally,
    Enemy,
}

public enum eAllyType
{
    Sword,
    Shield,
    Bow,
}




public class AllyListArg
{
    public eAllyType allyType;
    public int count;
}


public class StageWaveInfoArg
{
    public int wave;
    public List<AllyListArg> arg;

}



public class GameStatus : MonoBehaviour
{

    [System.Serializable]
    public class StageInfoClass
    {
        public int index;
        public string name;
        public string description;
    }

    public static List<StageInfoClass> StageInfos = new List<StageInfoClass>()
    {
        new StageInfoClass() { index = 1, name = "Stage1" },
        new StageInfoClass() { index = 2, name = "Stage2" }
    };


    public static bool isLobbyLoaded = false;
    public static int CurrentLevel = 0;

    

    public static List<StageWaveInfoArg> StageWaveInfo = new List<StageWaveInfoArg>()
    {
        new StageWaveInfoArg()
        {
            wave = 1,
            arg = new List<AllyListArg>()
            {
                new AllyListArg()
                {
                    allyType = eAllyType.Sword,
                    count = 2,
                },
                new AllyListArg()
                {
                    allyType = eAllyType.Sword,
                    count = 3,
                },
            }
        }
    };

    public static List<AllyListArg> GetStageWaveInfoByWave(int _wave)
    {
        int count = StageWaveInfo.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            StageWaveInfoArg arg = StageWaveInfo[iNum];
            if (arg.wave == _wave)
            {
                return arg.arg;
            }
        }
        return null;
    }


}
