using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForPreGameScene : MonoBehaviour
{
    private void Awake()
    {
        if (GameStatus.isLobbyLoaded == false)
        {
            if (SceneManager.GetActiveScene().name != "Lobby")
            {
                SceneManager.LoadScene("Lobby");
                return;
            }
        }
    }
}
