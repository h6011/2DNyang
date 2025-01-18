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


        List<EnemyListArg> args = GameStatus.GetStageWaveInfoByWave(currentLevel);
        int argsCount = args.Count;
        for (int iNum = 0; iNum < args.Count; iNum++)
        {
            EnemyListArg allyListArg = args[iNum];
            int allyListArgCount = allyListArg.count;
            for (int jNum = 0; jNum < allyListArgCount; jNum++)
            {
                EntityMng.Instance.TrySpawnEnemy(allyListArg.enemyType);
            }

        }



        for (int i = 0; i < 3; i++)
        {
            EntityMng.Instance.TrySpawnAlly(eAllyType.Sword);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool IsGamePaused = LobbyMng.Instance.TryEscape();
            GameCanvasMng.Instance.SetVisibleUI("Escape", IsGamePaused);
            if (IsGamePaused)
            {
                AudioMng.Instance.PauseAllAudio();
            }
            else
            {
                AudioMng.Instance.UnpauseAllAudio();
            }
        }
    }

    

    public bool TryDamage(Transform TargetTrans, float Damage)
    {
        if (TargetTrans)
        {
            if (TargetTrans.CompareTag("Base"))
            {
                LobbyMng.Instance.BaseGetDamage(GameStatus.WhatBaseType(TargetTrans), Damage);
                EntityMng.Instance.BaseDamagedEffect(GameStatus.WhatBaseType(TargetTrans));
                return true;
            }
            else
            {
                EntityCtrl entityCtrl = TargetTrans.GetComponent<EntityCtrl>();
                if (entityCtrl && entityCtrl.GetHp() > 0)
                {
                    entityCtrl.GetAttacked(Damage);
                    return true;
                }
            }
        }
        return false;
    }


    


}
