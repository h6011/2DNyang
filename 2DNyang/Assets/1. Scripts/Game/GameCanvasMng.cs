using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;






public class GameCanvasMng : MonoBehaviour
{
    [System.Serializable]
    public class AllyImgDataClass
    {
        public eAllyType AllyType;
        public Sprite Sprite;
    }

    [Header("SELF MNG")]
    public static GameCanvasMng Instance;

    [Header("Mngs")]
    private LobbyMng lobbyMng;
    private AudioMng audioMng;
    private PrefabMng prefabMng;



    [Header("UI")]
    public TMP_Text MoneyTitle;
    public Transform SpawnFrame;
    public Transform EscapeFrame;

    [Header("Escape Buttons")]
    public Button BackBtn;
    public Button ExitBtn;

    [Header("GameResult Buttons")]
    public Button BackToLobbyBtn;
    public Button SettingsBtn;
    public Button RestartBtn;


    [Header("Prefab")]
    private GameObject SpawnItemFrame;

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
        INIT_SELF_MNG();
    }

    private void Start()
    {
        lobbyMng = LobbyMng.Instance;
        audioMng = AudioMng.Instance;
        prefabMng = PrefabMng.Instance;

        SpawnItemFrame = prefabMng.GetPrefabByName("SpawnItemFrame");

        InitSpawnItemFrames();
        Init();


    }


    private void Update()
    {
        MoneyTitleDisplay();
    }

    private void INIT_SELF_MNG()
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

    private void Init()
    {
        InitStartFrames();



        GameStatus.addListenerToBtn(BackBtn, () =>
        {
            lobbyMng.UnpauseGame(true);
            audioMng.PlayClickAudio();
        });

        GameStatus.addListenerToBtn(ExitBtn, () =>
        {
            lobbyMng.UnpauseGame(true);
            audioMng.PlayClickAudio();
            SceneManager.LoadScene("Lobby");
        });




        GameStatus.addListenerToBtn(BackToLobbyBtn, () =>
        {
            SceneManager.LoadScene("Lobby");
            audioMng.PlayClickAudio();
            lobbyMng.UnpauseGame(false);
        });

        GameStatus.addListenerToBtn(SettingsBtn, () =>
        {
            SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
            audioMng.PlayClickAudio();
            //lobbyMng.UnpauseGame(false);
        });


        GameStatus.addListenerToBtn(RestartBtn, () =>
        {
            SceneManager.LoadScene("Game");
            audioMng.PlayClickAudio();
        });

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

    private void InitStartFrames()
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
