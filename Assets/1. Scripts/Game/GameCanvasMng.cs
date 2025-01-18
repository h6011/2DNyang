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
    private AudioMng audioMng;



    [Header("UI")]
    public TMP_Text MoneyTitle;
    public Transform SpawnFrame;
    public Transform EscapeFrame;

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
        audioMng = AudioMng.Instance;

        InitSpawnItemFrames();
        Init();
    }


    private void Update()
    {
        MoneyTitleDisplay();
    }

    private void Init()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name == "Main")
            {
                SetVisibleUI(child, true);
            }
            else
            {
                SetVisibleUI(child, false);
            }
        }
    }

    private void InitSpawnItemFrames()
    {
        string[] AllyNames = System.Enum.GetNames(typeof(eAllyType));

        for (int i = 0; i < AllyNames.Length; i++)
        {
            string AllyName = AllyNames[i];
            eAllyType allyType = (eAllyType)Enum.Parse(typeof(eAllyType), AllyName);

            GameObject NewSpawnItemFrame = Instantiate(SpawnItemFrame, SpawnFrame);
            Button Btn = NewSpawnItemFrame.GetComponent<Button>();

            Transform HolderFrame = NewSpawnItemFrame.transform.Find("Holder");
            Transform TitleTransform = NewSpawnItemFrame.transform.Find("Title");
            Transform EntityImgFrame = HolderFrame.Find("EntityImg");

            Image EntityImg = EntityImgFrame.GetComponent<Image>();
            EntityImg.sprite = GetAllyImg(allyType);

            TMP_Text Title = TitleTransform.GetComponent<TMP_Text>();
            Title.text = $"{GameSettings.GetAllyProperties(allyType).SpawnCost}";

            GameStatus.addListenerToBtn(Btn, () =>
            {
                EntityMng.Instance.TrySpawnAlly(allyType);
                audioMng.PlayClickAudio();
            });

        }
    }

    private void MoneyTitleDisplay()
    {
        MoneyTitle.text = $"{Mathf.FloorToInt(lobbyMng.money)} / {lobbyMng.maxMoney}";
    }



    public void ToggleUI(string Name)
    {
        Transform Find = transform.Find(Name);
        if (Find)
        {
            Find.gameObject.SetActive(!Find.gameObject.activeSelf);
        }
    }

    public void ToggleUI(Transform Trs)
    {
        Trs.gameObject.SetActive(!Trs.gameObject.activeSelf);
    }

    public void SetVisibleUI(string Name, bool Visible)
    {
        Transform Find = transform.Find(Name);
        if (Find)
        {
            Find.gameObject.SetActive(Visible);
        }
    }

    public void SetVisibleUI(Transform Trs, bool Visible)
    {
        Trs.gameObject.SetActive(Visible);
    }






}
