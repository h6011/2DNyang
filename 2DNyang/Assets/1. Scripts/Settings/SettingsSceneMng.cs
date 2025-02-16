using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsSceneMng : MonoBehaviour
{



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameStatus.IsMultipleScene)
            {
                GameStatus.IsMultipleScene = false;
                SceneManager.UnloadSceneAsync("Settings");
            }
            else
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }



}
