using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    /// lobbyCanvasMng 변수 체크하고 없다면 채우는 용도
    /// </summary>
    private void LobbyCanvasMngCheck()
    {
        if (lobbyCanvasMng == null)
        {
            lobbyCanvasMng = LobbyCanvasMng.Instance;
        }
    }

    

    /// <summary>
    /// 게임 종료 함수
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

    private bool IsAltF4()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            return true;
        }
        return false;
    }


    private bool WhenApplicationQuit()
    {
        if (!CanExitGame)
        {
            if (SceneManager.GetActiveScene().name == "Lobby")
            {
                if (lobbyCanvasMng != null)
                {
                    lobbyCanvasMng.VisibleUIExpectOther("NoticeExit");
                }
            }
            else if (SceneManager.GetActiveScene().name == "Game")
            {
                LobbyMng.Instance.PauseGame(true);
            }
        }

        if (IsAltF4())
        {
            return true;
        }

        return CanExitGame;
    }



}
