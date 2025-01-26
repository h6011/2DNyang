using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;






public class EnemyListArg
{
    public eEnemyType EnemyType;
    public int Count;
    public float Delay;
    public float BreakTime;
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
        new StageInfoClass() { index = 0, name = "Tutorial" },
        new StageInfoClass() { index = 1, name = "Stage1" },
        new StageInfoClass() { index = 2, name = "Stage2" }
    };


    public static bool isLobbyLoaded = false;
    public static int CurrentLevel = 0;



    public static List<StageWaveInfoArg> StageWaveInfo = new List<StageWaveInfoArg>()
    {
        new StageWaveInfoArg()
        {
            wave = 0,
            arg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.Sword,
                    Count = 0,
                    Delay = 0f,
                    BreakTime = 100f,
                },
            }
        },

        new StageWaveInfoArg()
        {
            wave = 1,
            arg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.Sword,
                    Count = 1,
                    Delay = 0f,
                    BreakTime = 6f,
                },
            }
        },
        new StageWaveInfoArg()
        {
            wave = 2,
            arg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.Sword,
                    Count = 1,
                    Delay = 0f,
                    BreakTime = 5f,
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

    public static eBaseType WhatBaseType(Transform Trans)
    {
        if (Trans.name == "AllyBase")
        {
            return eBaseType.Ally;
        }
        else if (Trans.name == "EnemyBase")
        {
            return eBaseType.Enemy;
        }
        return eBaseType.Enemy;
    }

    public static float GetTruncatedFloat(float targetFloat)
    {
        float truncatedFloat = Mathf.Floor(targetFloat * 10f) / 10f;
        return truncatedFloat;
    }








}
