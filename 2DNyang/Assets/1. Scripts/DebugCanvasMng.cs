using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugCanvasMng : MonoBehaviour
{

    public static DebugCanvasMng Instance;

    [Header("Prefab")]
    public GameObject DebugText;
    public TMP_Text DisplayFpsTitle;


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


    private void Update()
    {
        if (SettingsMng.Instance.GetSettings().DisplayFPS)
        {
            DisplayFpsTitle.gameObject.SetActive(true);
            float fps = Mathf.Floor(1.0f / Time.unscaledDeltaTime);
            DisplayFpsTitle.text = $"Fps : {fps}";
        }
        else
        {
            DisplayFpsTitle.gameObject.SetActive(false);
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
