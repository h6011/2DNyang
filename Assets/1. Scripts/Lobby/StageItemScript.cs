using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageItemScript : MonoBehaviour
{

    [SerializeField] private Button Btn;
    [SerializeField] private TMP_Text Title;
    [SerializeField] private int level;


    public void ChangeText(string text)
    {
        Title.text = text;
    }

    public void ChangeLevel(int _level)
    {
        level = _level;
    }

    private IEnumerator whenLoadStageScene()
    {
        yield return null;
        GameStatus.CurrentLevel = level;
        LobbyMng.Instance.StartGame();
        SceneManager.LoadScene("Game");

        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
        //asyncOperation.allowSceneActivation = true;

        //while (!asyncOperation.isDone)
        //{
        //    Debug.Log($"asyncOperation.progress : {asyncOperation.progress}");


        //    yield return null;
        //}

        //GameStatus.CurrentLevel = level;




    }


    private void Start()
    {
        Btn.onClick.RemoveAllListeners();
        Btn.onClick.AddListener(() =>
        {
            StartCoroutine(whenLoadStageScene());
            AudioMng.Instance.PlayClickAudio();
        });



    }




}
