using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCanvasMng : MonoBehaviour
{

    public static DebugCanvasMng Instance;

    [Header("Prefab")]
    public GameObject DebugText;


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


    public void ShowText(string text)
    {
        Transform TextsTransform = transform.Find("Texts");
        GameObject New = Instantiate(DebugText, TextsTransform);
        TMP_Text Text = New.GetComponent<TMP_Text>();
        Text.text = text;
        Destroy(New, 10);
    }

}
