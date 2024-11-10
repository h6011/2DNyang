using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasMng : MonoBehaviour
{

    public static GameCanvasMng Instance;


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



}
