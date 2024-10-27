using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvasMng : MonoBehaviour
{

    public static LobbyCanvasMng Instance;


    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void VisibleUI(string name)
    {
        Transform findUI = transform.Find(name);
        if (findUI)
        {
            findUI.gameObject.SetActive(true);
        }
    }

    public void SetVisibleUI(string name, bool boolean)
    {
        Transform findUI = transform.Find(name);
        if (findUI)
        {
            findUI.gameObject.SetActive(boolean);
        }
    }








}
