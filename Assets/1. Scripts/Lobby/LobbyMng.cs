using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMng : MonoBehaviour
{


    private void Awake()
    {
        if (GameStatus.isLobbyLoaded == false)
        {
            GameStatus.isLobbyLoaded = true;
        }
    }



}
