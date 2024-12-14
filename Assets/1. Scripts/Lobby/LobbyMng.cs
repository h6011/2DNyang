using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMng : MonoBehaviour
{

    public static LobbyMng Instance;

    private float Score;
    private float Money;
    private float MaxMoney;

    
    

    private float MoneyMulti = 5f;

    [Header("InGame Stat")]

    [SerializeField] private bool isGameStarted;
    [SerializeField] private float GameTime;



    public float score => Score;
    public float money => Money;
    public float maxMoney => MaxMoney;



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
        DontDestroyOnLoad(gameObject);
    }

    public void ResetGameStat()
    {
        GameTime = 0;
    }

    public void StartGameStat()
    {
        isGameStarted = true;
    }

    public void EndGameStat()
    {
        isGameStarted = false;
    }

    /// <summary>
    /// StartGameStat() 보단 이걸 추천
    /// </summary>
    public void StartGame()
    {
        ResetGameStat();
        StartGameStat();
    }





    private void Update()
    {
        if (isGameStarted)
        {
            float DeltaTime = Time.deltaTime;
            GameTime += DeltaTime;
            Money += DeltaTime * MoneyMulti;
        }
    }








}
