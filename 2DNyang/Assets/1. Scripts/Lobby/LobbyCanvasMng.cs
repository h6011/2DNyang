using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyCanvasMng : MonoBehaviour
{
    AudioMng audioMng;


    public static LobbyCanvasMng Instance;

    [Header("UI")]

    public Button PlayBtn;
    public Button ShopBtn;
    public Button SettingsBtn;
    public Button ExitGameBtn;

    [Header("NoticeExit UI")]

    public Button NoticeExitUI_YesBtn;
    public Button NoticeExitUI_NoBtn;

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
            stageItemScript.ChangeLevel(Info.index);




        }

        
    }

    

    

    private void Start()
    {
        audioMng = AudioMng.Instance;

        SetUpStageUI();

        VisibleUIExpectOther("Main");

        addListenerToBtn(PlayBtn, () =>
        {
            VisibleUIExpectOther("Stage");
            audioMng.PlayClickAudio();
        });

        addListenerToBtn(ShopBtn, () =>
        {
            audioMng.PlayClickAudio();
        });

        addListenerToBtn(SettingsBtn, () =>
        {
            SceneManager.LoadScene("Settings");
            audioMng.PlayClickAudio();
        });

        addListenerToBtn(ExitGameBtn, () =>
        {
            ApplicationQuitMng.Instance.ExitGame();
            audioMng.PlayClickAudio();
        });

        addListenerToBtn(NoticeExitUI_YesBtn, () =>
        {
            ApplicationQuitMng.Instance.TryQuitGame();
            audioMng.PlayClickAudio();
        });

        addListenerToBtn(NoticeExitUI_NoBtn, () =>
        {
            ApplicationQuitMng.Instance.TryCancelQuitGame();
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
