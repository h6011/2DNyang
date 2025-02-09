using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MngsController : MonoBehaviour
{
    public static MngsController Instance;


    private void Awake()
    {
        //Debug.Log("[MngsController] Awake");

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



}
