using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMng : MonoBehaviour
{

    public static LobbyMng Instance;

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
    [SerializeField] private float GameTime;
    [SerializeField] private bool NoLongerEnemyBaseKnockback = false;


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

        //QualitySettings.vSyncCount = 0;

        //Debug.Log(AudioListener.volume);
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
        NoLongerEnemyBaseKnockback = false;

        Time.timeScale = 1;

        AudioMng.Instance.StopAudio("BGM");
        AudioMng.Instance.PlayAudio("BGM", 0.3f, true);
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

            GameCanvasMng.Instance.SetVisibleUI("GameResult", true);

            EntityMng.Instance.BaseDestroyedEffect(baseType);

            if (baseType == eBaseType.Enemy)
            {
                PlayerDataMng.Instance.OnClearedStage(GameStatus.CurrentLevel);
            }

            //PauseGame(false);



        }
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



                    EntityMng.Instance.TrySpawnEnemy(eEnemyType.UpgradedSword);

                    EntityMng.Instance.KnockbackAllies(new Vector2(15, 15));

                    ParticleMng.Instance.CreateParticle(eParticleType.Destroy2, Vector2.zero);

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
            AudioMng.Instance.PauseAllAudio();
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
            AudioMng.Instance.UnpauseAllAudio();
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
