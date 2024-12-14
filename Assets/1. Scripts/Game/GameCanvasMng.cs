using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasMng : MonoBehaviour
{

    public static GameCanvasMng Instance;

    private LobbyMng lobbyMng;



    [Header("UI")]
    public TMP_Text MoneyTitle;





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
        lobbyMng = LobbyMng.Instance;
    }


    private void Update()
    {
        MoneyTitle.text = $"Money : {lobbyMng.money} / {lobbyMng.maxMoney}";
    }






}
