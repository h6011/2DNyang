using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{

    public static GameMng Instance;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        int currentLevel = GameStatus.CurrentLevel;
        Debug.Log($"Current Level : {currentLevel}");


        List<AllyListArg> args = GameStatus.GetStageWaveInfoByWave(currentLevel);
        int argsCount = args.Count;
        for (int iNum = 0; iNum < args.Count; iNum++)
        {
            AllyListArg allyListArg = args[iNum];
            int allyListArgCount = allyListArg.count;
            for (int jNum = 0; jNum < allyListArgCount; jNum++)
            {
                EntityMng.Instance.TrySpawnAlly(allyListArg.allyType);
            }

        }

        

        //for (int i = 0; i < 5; i++)
        //{
        //    EntityMng.Instance.SpawnEntity(eEntityType.Ally);
        //}
    }






}
