using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{

    public static GameMng Instance;

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
        for (int i = 0; i < 5; i++)
        {
            EntityMng.Instance.SpawnEntity(eEntityType.Ally);
        }
    }






}
