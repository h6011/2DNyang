using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMng : MonoBehaviour
{

    public static LobbyMng Instance;

    [SerializeField] private float Score;
    [SerializeField] private float Money;
    private float MaxMoney = 2000;

    [SerializeField] private float AllyBaseHp;
    [SerializeField] private float EnemyBaseHp;

    private bool IsGamePaused = false;

    private float MoneyMulti = 15f;

    [Header("InGame Stat")]

    [SerializeField] private bool isGameStarted;
    [SerializeField] private float GameTime;


    public float score => Score;
    public float money => Money;
    public float maxMoney => MaxMoney;


    public float allyBaseHp => AllyBaseHp;
    public float enemyBaseHp => EnemyBaseHp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void WhenAllySpawn(eAllyType allyType)
    {
        Money -= GameSettings.GetAllyProperties(allyType).SpawnCost;
    }

    public void WhenEnemyDied(eEnemyType enemyType, Transform Trs)
    {
        if (Trs != null)
        {
            Money += GameSettings.GetEnemyProperties(enemyType).SpawnCost;
        }
    }

    public void ResetGameStat()
    {
        GameTime = 0;
        Money = 0;
        AllyBaseHp = 1000;
        EnemyBaseHp = 1000;
    }


    /// <summary>
    /// 게임 시작할떄 스탯 초기화 느낌으로
    /// </summary>
    public void StartGame()
    {
        ResetGameStat();
        isGameStarted = true;

        Time.timeScale = 1;

        AudioMng.Instance.StopAudio("BGM");
        AudioMng.Instance.PlayAudio("BGM", 0.3f, true);
    }


    public void BaseGetDamage(eBaseType baseType, float Damage = 0)
    {
        if (baseType == eBaseType.Ally)
        {
            AllyBaseHp -= Damage;
        }
        else if (baseType == eBaseType.Enemy)
        {
            EnemyBaseHp -= Damage;
        }
    }

    public void StartGameTime()
    {
        Time.timeScale = 1;
    }

    public void PauseGameTime()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Return IsGamePaused
    /// </summary>
    /// <returns></returns>
    public bool TryEscape()
    {
        if (IsGamePaused)
        {
            IsGamePaused = false;
            StartGameTime();
            return IsGamePaused;
        }
        else
        {
            IsGamePaused = true;
            PauseGameTime();
            return IsGamePaused;
        }
    }

    public bool GetIsGamePaused()
    {
        return IsGamePaused;
    }





    private void Update()
    {
        if (isGameStarted)
        {
            float DeltaTime = Time.deltaTime;
            GameTime += DeltaTime;
            Money += DeltaTime * MoneyMulti;

            Money = Mathf.Clamp(Money, 0, MaxMoney);
        }
    }








}
