using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class EnemyListArg
{
    public eEnemyType enemyType;
    public int count;
}


public class StageWaveInfoArg
{
    public int wave;
    public List<EnemyListArg> arg;

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
            arg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    enemyType = eEnemyType.Sword,
                    count = 3,
                },
                new EnemyListArg()
                {
                    enemyType = eEnemyType.Sword,
                    count = 0,
                },
            }
        }
    };

    public static List<EnemyListArg> GetStageWaveInfoByWave(int _wave)
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
    
    public static void addListenerToBtn(UnityEngine.UI.Button _Btn, UnityEngine.Events.UnityAction _Action)
    {
        _Btn.onClick.RemoveAllListeners();
        _Btn.onClick.AddListener(_Action);
    }

}
