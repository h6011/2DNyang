using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageItemScript : MonoBehaviour
{

    [SerializeField] private Button Btn;
    [SerializeField] private TMP_Text Title;


    public void ChangeText(string text)
    {
        Title.text = text;
    }

    private void Start()
    {
        Btn.onClick.RemoveAllListeners();
        Btn.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        });



    }




}
