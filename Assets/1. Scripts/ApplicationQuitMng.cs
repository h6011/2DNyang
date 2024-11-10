using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuitMng : MonoBehaviour
{


    private bool CanExitGame = false;

    private LobbyCanvasMng lobbyCanvasMng;

    public static ApplicationQuitMng Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LobbyCanvasMngCheck();
        Application.wantsToQuit += WhenApplicationQuit;
    }

    /// <summary>
    /// lobbyCanvasMng ���� üũ�ϰ� ���ٸ� ä��� �뵵
    /// </summary>
    private void LobbyCanvasMngCheck()
    {
        if (lobbyCanvasMng == null)
        {
            lobbyCanvasMng = LobbyCanvasMng.Instance;
        }
    }

    

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }



    public void TryQuitGame()
    {
        CanExitGame = true;
        ExitGame();
    }


    public void TryCancelQuitGame()
    {
        CanExitGame = false;
        lobbyCanvasMng.VisibleUIExpectOther("Main");
    }


    private bool WhenApplicationQuit()
    {
        if (!CanExitGame)
        {
            if (lobbyCanvasMng)
            {
                lobbyCanvasMng.VisibleUIExpectOther("NoticeExit");
            }
        }

        return CanExitGame;
    }



}
