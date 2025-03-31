using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyCanvasMng : MonoBehaviour
{
    AudioMng audioMng;
    ApplicationQuitMng applicationQuitMng;
    PlayerDataMng playerDataMng;

    public static LobbyCanvasMng Instance;

    [Header("UI")]

    public Button PlayBtn;
    public Button ShopBtn;
    public Button SettingsBtn;
    public Button ExitGameBtn;

    [Header("NoticeExit UI")]

    public Button NoticeExitUI_YesBtn;
    public Button NoticeExitUI_NoBtn;

    [Header("NoticeTutorial UI")]

    public Button NoticeTutorialUI_YesBtn;
    public Button NoticeTutorialUI_NoBtn;

    [Header("Stage UI")]
    public Button Stage_BackBtn;


    [Header("Prefab")]

    public GameObject StageItem;

    [Header("Prefab Parent")]

    public Transform StageBtns;




    


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
        INIT_MNGS();

        GameStatus.IsMultipleScene = false;

        audioMng.StopAudio("BGM");

        SetUpStageUI();

        VisibleUIExpectOther("Main");

        ConnectBtns();





    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            VisibleUIExpectOther("Main");
        }
    }



    private void SetUpStageUI()
    {
        List<GameStatus.StageInfoClass> Infos = GameStatus.GetAvailableStageInfos();
        int count = Infos.Count;

        PlayerDataArgs args = PlayerDataMng.Instance.GetData();

        List<IsStageClearArg> isStageClearArgs = args.IsStageClearArg;
        int isStageClearArgsCount = isStageClearArgs.Count;


        for (int i = 0; i < count; i++)
        {
            GameStatus.StageInfoClass Info = Infos[i];

            GameObject newItem = Instantiate(StageItem, StageBtns);
            StageItemScript stageItemScript = newItem.GetComponent<StageItemScript>();
            stageItemScript.ChangeText(Info.index.ToString());
            stageItemScript.ChangeLevel(Info.index);

            for (int i2 = 0; i2 < isStageClearArgsCount; i2++)
            {
                IsStageClearArg arg = isStageClearArgs[i2];
                if (arg.StageNum == Info.index)
                {
                    Color AlreadyClearedColor = new Color() { a = 1, r = 0.9297475f, g = 1, b = 0.495283f };
                    stageItemScript.ChangeColor(AlreadyClearedColor);
                    break;
                }
            }



        }

        
    }

    private void OnPlayBtnClicked()
    {
        PlayerDataArgs args = PlayerDataMng.Instance.GetData();
        audioMng.PlayClickAudio();
        if (args.SkippedTutorial)
        {
            VisibleUIExpectOther("Stage");
        }
        else
        {
            if (args.DidTutorial)
            {
                VisibleUIExpectOther("Stage");
            }
            else
            {
                VisibleUIExpectOther("NoticeTutorial");
            }
        }
    }

    private void INIT_MNGS()
    {
        audioMng = AudioMng.Instance;
        applicationQuitMng = ApplicationQuitMng.Instance;
        playerDataMng = PlayerDataMng.Instance;
    }
    

    private IEnumerator whenLoadStageScene(int level)
    {
        yield return null;
        GameStatus.CurrentLevel = level;
        LobbyMng.Instance.StartGame();
        SceneManager.LoadScene("Game");
    }


    private void ConnectBtns()
    {

        GameStatus.addListenerToBtn(NoticeTutorialUI_YesBtn, () =>
        {
            StartCoroutine(whenLoadStageScene(0));
            audioMng.PlayClickAudio();
        });

        GameStatus.addListenerToBtn(NoticeTutorialUI_NoBtn, () =>
        {
            PlayerDataArgs Args = playerDataMng.GetData();
            Args.SkippedTutorial = true;
            playerDataMng.SaveData(Args);
            VisibleUIExpectOther("Main");
            audioMng.PlayClickAudio();
        });

        GameStatus.addListenerToBtn(PlayBtn, OnPlayBtnClicked);

        GameStatus.addListenerToBtn(ShopBtn, () =>
        {
            audioMng.PlayClickAudio();
        });

        GameStatus.addListenerToBtn(SettingsBtn, () =>
        {
            SceneManager.LoadScene("Settings");
            audioMng.PlayClickAudio();
        });

        GameStatus.addListenerToBtn(ExitGameBtn, () =>
        {
            applicationQuitMng.ExitGame();
            audioMng.PlayClickAudio();
        });

        ////////////////////// NoticeExit UI ////////////////////

        GameStatus.addListenerToBtn(NoticeExitUI_YesBtn, () =>
        {
            applicationQuitMng.TryQuitGame();
            audioMng.PlayClickAudio();
        });

        GameStatus.addListenerToBtn(NoticeExitUI_NoBtn, () =>
        {
            applicationQuitMng.TryCancelQuitGame();
            audioMng.PlayClickAudio();
        });

        ////////////////////////////////////////////////////////


        GameStatus.addListenerToBtn(Stage_BackBtn, () =>
        {
            VisibleUIExpectOther("Main");
            audioMng.PlayClickAudio();
        });
    }



    public void VisibleUI(string name)
    {
        Transform findUI = transform.Find(name);
        if (findUI)
        {
            findUI.gameObject.SetActive(true);
        }
    }

    public void SetVisibleUI(string name, bool boolean)
    {
        Transform findUI = transform.Find(name);
        if (findUI)
        {
            findUI.gameObject.SetActive(boolean);
        }
    }

    public void VisibleUIExpectOther(string name)
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.name == name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }








}
