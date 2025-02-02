using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;



[System.Serializable]
public class IsStageClearArg
{
    public int StageNum = -1;
}

[System.Serializable]
public class PlayerDataArgs
{
    public bool DidTutorial = false;
    public float PlayTime = 0f;
    public List<IsStageClearArg> IsStageClearArg = new List<IsStageClearArg>();
}



public class PlayerDataMng : MonoBehaviour
{
    public static PlayerDataMng Instance;

    private string FileName = "PlayerData.txt";
    public string FixedPath = string.Empty;

    private PlayerDataArgs CurrentData;


    private void Awake()
    {
        SELF_INS();

        FixedPath = Path.Combine(Application.persistentDataPath, FileName);
    }


    private void Start()
    {
        CheckCurrentData();
    }

    private void Update()
    {
        CurrentData.PlayTime += Time.unscaledDeltaTime;
    }

    private void SELF_INS()
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

    private void CheckCurrentData()
    {
        if (CurrentData == null)
        {
            CurrentData = loadDatasOfFile();
        }
    }
    

    //public void PrintStageClearArg()
    //{
    //    Dictionary<int, bool> dict = CurrentData.StageClearArg;
       
        

    //    List<int> keys = dict.Keys.ToList();
    //    int keyCount = keys.Count;

    //    Debug.Log($"Key Count : {keyCount}");

    //    for (int i = 0; i < keyCount; i++)
    //    {
    //        var key = keys[i];
    //        var value = dict[key];
    //        Debug.Log($"key : {key}, value : {value}");
    //    }

    //    //foreach (var item in dict)
    //    //{
    //    //    item.Key
    //    //}


    //}

    public void OnClearedStage(int StageNum)
    {
        bool IsAlready = false;

        int count = CurrentData.IsStageClearArg.Count;

        for (int i = 0; i < count; i++)
        {
            IsStageClearArg arg = CurrentData.IsStageClearArg[i];
            if (arg.StageNum == StageNum)
            {
                IsAlready = true;
                break;
            }
        }


        if (!IsAlready)
        {
            IsStageClearArg NewArg = new IsStageClearArg();
            NewArg.StageNum = StageNum;
            CurrentData.IsStageClearArg.Add(NewArg);
        }
    }



    public void SaveData(PlayerDataArgs args)
    {
        CurrentData = args;
    }

    public PlayerDataArgs GetData()
    {
        CheckCurrentData();
        return CurrentData;
    }


    public void saveDataOfFile(PlayerDataArgs args)
    {
        string Jsoned = JsonUtility.ToJson(args);
        FileMng.Instance.SaveFile(FixedPath, Jsoned);
    }

    public PlayerDataArgs loadDatasOfFile()
    {
        FileMng.LoadFIleResult Result = FileMng.Instance.LoadFile(FixedPath);
        PlayerDataArgs args = JsonUtility.FromJson<PlayerDataArgs>(Result.Text);
        if (args == null)
        {
            args = new PlayerDataArgs();
        }
        return args;
    }


    private void OnApplicationQuit()
    {
        saveDataOfFile(CurrentData);
    }


}
