using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerMng : MonoBehaviour
{

    public static AudioListenerMng Instance;


    private void Awake()
    {
        //Debug.Log("[AudioListener] Awake");

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        gameObject.AddComponent<AudioListener>();
    }



}
