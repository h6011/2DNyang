using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCanvasMng : MonoBehaviour
{

    public static LobbyCanvasMng Instance;

    [Header("UI")]

    public Button PlayBtn;
    public Button ShopBtn;
    public Button SettingsBtn;
    public Button ExitGameBtn;

    [Header("Prefab")]

    public GameObject StageItem;

    [Header("Prefab Parent")]

    public Transform StageBtns;




    public System.Action quitEvent;
    public bool isApplicationCanQuit;


    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }


        quitEvent += () =>
        {
            VisibleUIExpectOther("NoticeExit");
        };

        Application.wantsToQuit += ApplicationQuit;
    }


    private void addListenerToBtn(Button _Btn, UnityEngine.Events.UnityAction _Action)
    {
        _Btn.onClick.RemoveAllListeners();
        _Btn.onClick.AddListener(_Action);
    }

    private void SetUpStageUI()
    {
        List<GameStatus.StageInfoClass> Infos = GameStatus.StageInfos;
        int count = Infos.Count;

        for (int i = 0; i < count; i++)
        {
            GameStatus.StageInfoClass Info = Infos[i];

            GameObject newItem = Instantiate(StageItem, StageBtns);
            StageItemScript stageItemScript = newItem.GetComponent<StageItemScript>();
            stageItemScript.ChangeText(Info.index.ToString());




        }

        
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }

    private void Start()
    {
        SetUpStageUI();

        VisibleUIExpectOther("Main");

        addListenerToBtn(PlayBtn, () =>
        {
            VisibleUIExpectOther("Stage");
        });

        addListenerToBtn(ExitGameBtn, () =>
        {
            ExitGame();
            //Application.Quit();
        });

        //Application.wantsToQuit

    }

    

   



    public void OnClickQuitProcess()
    {
        isApplicationCanQuit = true;
        Application.Quit();
    }


    public void OnClickQuitCancel()
    {
        isApplicationCanQuit = false;
        VisibleUIExpectOther("Main");
    }


    private bool ApplicationQuit()
    {
        if (!isApplicationCanQuit)
        {
            quitEvent?.Invoke();
        }

        return isApplicationCanQuit;
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
