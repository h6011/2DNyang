using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{

    public static GameMng Instance;

    private List<Coroutine> EnemySpawnLoopCoroutines = new List<Coroutine>();

    public Transform AllyBase;
    public Transform EnemyBase;


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
    
    IEnumerator EnemySpawnLoopCoroutine(EnemyListArg arg)
    {
        bool STOP_LOOP = false;

        while (!STOP_LOOP)
        {

            int allyListArgCount = arg.Count;
            for (int jNum = 0; jNum < allyListArgCount; jNum++)
            {
                EntityMng.Instance.TrySpawnEnemy(arg.EnemyType);
                yield return new WaitForSeconds(arg.Delay);
            }

            yield return new WaitForSeconds(arg.BreakTime);

            yield return null;
        }

    }

    IEnumerator WaveCoroutine()
    {
        int currentLevel = GameStatus.CurrentLevel;
        Debug.Log($"Current Level : {currentLevel}");

        StageWaveInfoArg WaveInfo = GameStatus.GetStageWaveInfoByWave(currentLevel);

        List<EnemyListArg> MainSpawnArg = WaveInfo.MainSpawnArg;
        int argsCount = MainSpawnArg.Count;

        for (int iNum = 0; iNum < MainSpawnArg.Count; iNum++)
        {
            Coroutine coroutine = StartCoroutine(EnemySpawnLoopCoroutine(MainSpawnArg[iNum]));
            EnemySpawnLoopCoroutines.Add(coroutine);
        }


        yield return null;
    }

    private void Start()
    {
        StartCoroutine(WaveCoroutine());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStatus.IsMultipleScene == false && LobbyMng.Instance.GetisGameEnded() == false)
            {
                LobbyMng.Instance.TryEscape(true);
            }
        }
    }

    
    /// <summary>
    /// Damaging Function Core
    /// </summary>
    /// <param name="TargetTrans">Target (Entity, Base)</param>
    /// <param name="Damage">Damage Amount</param>
    /// <returns></returns>
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
                    bool IsSuccess = entityCtrl.GetAttacked(Damage);
                    return IsSuccess;
                }
            }
        }
        return false;
    }


    


}
