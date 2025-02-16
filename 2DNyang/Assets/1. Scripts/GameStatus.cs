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
    public List<EnemyListArg> MainSpawnArg;
    public List<EnemyListArg> HpLowSpawArg;

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
        new StageInfoClass() { index = 2, name = "Stage2" },
        new StageInfoClass() { index = 3, name = "Stage3" },
    };


    public static bool isLobbyLoaded = false;
    public static bool IsMultipleScene = false;
    public static int CurrentLevel = 0;


    public static List<StageInfoClass> GetAvailableStageInfos()
    {
        List<StageInfoClass> New = new List<StageInfoClass>(StageInfos);
        //New.RemoveAt(0);
        return New;
    }


    public static List<StageWaveInfoArg> StageWaveInfo = new List<StageWaveInfoArg>()
    {
        new StageWaveInfoArg()
        {
            wave = 0,
            MainSpawnArg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.Sword,
                    Count = 0,
                    Delay = 0f,
                    BreakTime = 100f,
                },
            },
        },

        new StageWaveInfoArg()
        {
            wave = 1,
            MainSpawnArg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.Sword,
                    Count = 1,
                    Delay = 7f,
                    BreakTime = 6f,
                },
            },
             HpLowSpawArg = new List<EnemyListArg>(){ },
        },
        new StageWaveInfoArg()
        {
            wave = 2,
            MainSpawnArg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.Sword,
                    Count = 1,
                    Delay = 7f,
                    BreakTime = 6f,
                },
            },
             HpLowSpawArg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.UpgradedSword,
                    Count = 2,
                    Delay = 1f,
                    BreakTime = 0f,
                },
            },
        },
        new StageWaveInfoArg()
        {
            wave = 3,
            MainSpawnArg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.Sword,
                    Count = 1,
                    Delay = 7f,
                    BreakTime = 6f,
                },
            },
             HpLowSpawArg = new List<EnemyListArg>()
            {
                new EnemyListArg()
                {
                    EnemyType = eEnemyType.UpgradedSword,
                    Count = 2,
                    Delay = 1f,
                    BreakTime = 0f,
                },

                new EnemyListArg()
                {
                    EnemyType = eEnemyType.Bow,
                    Count = 3,
                    Delay = 1f,
                    BreakTime = 0f,
                },
            },
        },
    };

    public static StageWaveInfoArg GetStageWaveInfoByWave(int _wave)
    {
        int count = StageWaveInfo.Count;
        for (int iNum = 0; iNum < count; iNum++)
        {
            StageWaveInfoArg arg = StageWaveInfo[iNum];
            if (arg.wave == _wave)
            {
                return arg;
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
