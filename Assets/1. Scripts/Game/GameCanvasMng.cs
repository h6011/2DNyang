using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;





public class GameCanvasMng : MonoBehaviour
{
    [System.Serializable]
    public class AllyImgDataClass
    {
        public eAllyType AllyType;
        public Sprite Sprite;
    }

    public static GameCanvasMng Instance;

    private LobbyMng lobbyMng;



    [Header("UI")]
    public TMP_Text MoneyTitle;
    public Transform SpawnFrame;

    [Header("Prefab")]
    [SerializeField] private GameObject SpawnItemFrame;

    [Header("Data")]
    public List<AllyImgDataClass> AllyImgData = new List<AllyImgDataClass>();


    private Sprite GetAllyImg(eAllyType AllyType)
    {
        int count = AllyImgData.Count;
        for (int i = 0; i < count; i++)
        {
            AllyImgDataClass data = AllyImgData[i];
            if (data.AllyType == AllyType)
            {
                return data.Sprite;
            }
        }
        return null;
    }

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

        

        string[] AllyNames = System.Enum.GetNames(typeof(eAllyType));

        for (int i = 0; i < AllyNames.Length; i++)
        {
            string AllyName = AllyNames[i];
            eAllyType allyType = (eAllyType)Enum.Parse(typeof(eAllyType), AllyName);

            GameObject NewSpawnItemFrame = Instantiate(SpawnItemFrame, SpawnFrame);
            Button Btn = NewSpawnItemFrame.GetComponent<Button>();

            Transform HolderFrame = NewSpawnItemFrame.transform.Find("Holder");
            Transform EntityImgFrame = HolderFrame.Find("EntityImg");

            Image EntityImg = EntityImgFrame.GetComponent<Image>();
            EntityImg.sprite = GetAllyImg(allyType);

            GameStatus.addListenerToBtn(Btn, () =>
            {
                EntityMng.Instance.TrySpawnAlly(allyType);
            });

        }

        //Transform TestSpawnItemFrame = SpawnFrame.Find("SpawnItemFrame");
        //Button TestSpawnItemFrameButton = TestSpawnItemFrame.GetComponent<Button>();

        //GameStatus.addListenerToBtn(TestSpawnItemFrameButton, () =>
        //{
        //    EntityMng.Instance.TrySpawnAlly(eAllyType.Sword);
        //});
    }


    private void Update()
    {
        MoneyTitle.text = $"Money : {lobbyMng.money} / {lobbyMng.maxMoney}";

        //int SpawnFrameCount = SpawnFrame.childCount;

        //Debug.Log($"SpawnFrameCount : {SpawnFrameCount}");
        //SpawnFrame.offsetMin = new Vector2(0f, 0f);
        //SpawnFrame.offsetMax = new Vector2((100 * SpawnFrameCount), 100);
    }






}
