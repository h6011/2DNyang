using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationQuitMng : MonoBehaviour
{

    private LobbyMng lobbyMng;
    private LobbyCanvasMng lobbyCanvasMng;

    public static ApplicationQuitMng Instance;

    private bool CanExitGame = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        MngsCheck();
        Application.wantsToQuit += WhenApplicationQuit;

    }

    /// <summary>
    /// lobbyCanvasMng 변수 체크하고 없다면 채우는 용도
    /// </summary>
    private void MngsCheck()
    {
        if (lobbyCanvasMng == null)
        {
            lobbyCanvasMng = LobbyCanvasMng.Instance;
        }
        if (lobbyMng == null)
        {
            lobbyMng = LobbyMng.Instance;
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

    /// <summary>
    /// Alt F4
    /// </summary>
    /// <returns></returns>
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

        Scene currentScene = SceneManager.GetActiveScene();

        if (!CanExitGame)
        {
            if (currentScene.name == "Lobby")
            {
                MngsCheck();
                if (lobbyCanvasMng != null)
                {
                    lobbyCanvasMng.VisibleUIExpectOther("NoticeExit");
                }
            }
            else if (currentScene.name == "Game")
            {
                lobbyMng.PauseGame(true);
            }
        }


        if (IsAltF4())
        {
            return true;
        }

        return CanExitGame;
    }



}
