using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMng : MonoBehaviour
{

    public static LobbyMng Instance;

    [Header("MNGS")]
    private AudioMng audioMng;


    [Header("Stat")]
    [SerializeField] private float Score;
    [SerializeField] private float Money;
    private float MaxMoney = 2000;

    [Header("Hp")]
    [SerializeField] private float AllyBaseHp;
    [SerializeField] private float EnemyBaseHp;

    [SerializeField] private float AllyBaseMaxHp = 100;
    [SerializeField] private float EnemyBaseMaxHp = 100;


    private float MoneyMulti = 15f;

    [Header("InGame Stat")]

    [SerializeField] private bool IsGamePaused = false;
    [SerializeField] private bool isGameStarted = false;
    [SerializeField] private bool isGameEnded = false;
    [SerializeField] private float GameTime;
    [SerializeField] private bool NoLongerEnemyBaseKnockback = false;


    public float score => Score;
    public float money => Money;
    public float maxMoney => MaxMoney;


    public float allyBaseHp => AllyBaseHp;
    public float enemyBaseHp => EnemyBaseHp;
    public float allyBaseMaxHp => AllyBaseMaxHp;
    public float enemyBaseMaxHp => EnemyBaseMaxHp;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SettingsArgs args = SettingsMng.Instance.GetSettings();
        SettingsMng.Instance.ApplySettings(args);

        GetMngs();
        //QualitySettings.vSyncCount = 0;

        //Debug.Log(AudioListener.volume);
    }

    private void GetMngs()
    {
        audioMng = AudioMng.Instance;
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
        AllyBaseHp = AllyBaseMaxHp;
        EnemyBaseHp = EnemyBaseMaxHp;
    }


    /// <summary>
    /// 게임 시작할떄 스탯 초기화 느낌으로
    /// </summary>
    public void StartGame()
    {
        ResetGameStat();
        isGameStarted = true;
        isGameEnded = false;
        NoLongerEnemyBaseKnockback = false;

        Time.timeScale = 1;

        audioMng.StopAudio("BGM");
        audioMng.PlayAudio("BGM", 0.3f, true);
    }


    IEnumerator BaseGotDestoyedCoroutine()
    {
        
        yield return null;
    }


    private void BaseGotDestroyed(eBaseType baseType)
    {
        if (isGameStarted)
        {
            isGameStarted = false;
            isGameEnded = true;

            EntityMng.Instance.BaseDestroyedEffect(baseType);

            if (baseType == eBaseType.Enemy)
            {
                GameCanvasMng.Instance.OnGameEnd(true);
                PlayerDataMng.Instance.OnClearedStage(GameStatus.CurrentLevel);
            }
            else
            {
                GameCanvasMng.Instance.OnGameEnd(false);
            }

            //PauseGame(false);



        }
    }

    IEnumerator SpawnEnemyWhenLow(List<EnemyListArg> enemyList)
    {
        int count = enemyList.Count;

        for (int i = 0; i < count; i++)
        {
            EnemyListArg arg = enemyList[i];

            int EnemyCount = arg.Count;

            for (int j = 0; j < EnemyCount; j++)
            {
                EntityMng.Instance.TrySpawnEnemy(arg.EnemyType);
                yield return new WaitForSeconds(arg.Delay);
            }

            yield return new WaitForSeconds(arg.BreakTime);
        }



        yield return null;
    }

    public void BaseGetDamage(eBaseType baseType, float Damage = 0)
    {
        if (baseType == eBaseType.Ally)
        {
            AllyBaseHp -= Damage;
            if (AllyBaseHp <= 0)
            {
                BaseGotDestroyed(baseType);
            }
        }
        else if (baseType == eBaseType.Enemy)
        {
            EnemyBaseHp -= Damage;
            if (EnemyBaseHp <= 0)
            {
                BaseGotDestroyed(baseType);
            }
            if (EnemyBaseHp <= (EnemyBaseMaxHp * 0.2f))
            {
                if (!NoLongerEnemyBaseKnockback)
                {
                    NoLongerEnemyBaseKnockback = true;

                    Debug.Log("ENOUGH");

                   

                    StageWaveInfoArg WaveInfo = GameStatus.GetStageWaveInfoByWave(GameStatus.CurrentLevel);
                    List<EnemyListArg> HpLowSpawArg = WaveInfo.HpLowSpawArg;

                    if (HpLowSpawArg.Count > 0)
                    {
                        StartCoroutine(SpawnEnemyWhenLow(HpLowSpawArg));

                        EntityMng.Instance.KnockbackAllies(new Vector2(15, 15));

                        ParticleMng.Instance.CreateParticle(eParticleType.Destroy2, GameMng.Instance.EnemyBase.position);

                    }


                   

                   

                }
            }
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
    
    private void EscapeUIVisible(bool VisibleBool)
    {
        GameCanvasMng.Instance.SetVisibleUI("Escape", VisibleBool);
    }

    public void PauseGame(bool UIVisible = false)
    {
        if (!IsGamePaused)
        {
            IsGamePaused = true;
            PauseGameTime();
            audioMng.PauseAllAudio();
            if (UIVisible)
            {
                EscapeUIVisible(IsGamePaused);
            }
        }
    }

    public void UnpauseGame(bool UIVisible = false)
    {
        if (IsGamePaused)
        {
            IsGamePaused = false;
            StartGameTime();
            audioMng.UnpauseAllAudio();
            if (UIVisible)
            {
                EscapeUIVisible(IsGamePaused);
            }
        }
    }


    /// <summary>
    /// Return IsGamePaused
    /// </summary>
    /// <returns></returns>
    public bool TryEscape(bool UIVisible = false)
    {
        if (IsGamePaused)
        {
            UnpauseGame(UIVisible);
            return IsGamePaused;
        }
        else
        {
            PauseGame(UIVisible);
            return IsGamePaused;
        }
    }

    public bool GetIsGamePaused()
    {
        return IsGamePaused;
    }

    public bool GetisGameEnded()
    {
        return isGameEnded;
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
