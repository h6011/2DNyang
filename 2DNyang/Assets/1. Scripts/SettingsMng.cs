using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public enum eSettingsFps
{
    NoLimit,
    Fps30,
    Fps60,
    Fps144,
}

[System.Serializable]
public class SettingsArgs
{
    public eSettingsFps fps = eSettingsFps.NoLimit;
    public float CameraSpeed = 5f;
}

public class SettingsMng : MonoBehaviour
{

    private string FileName = "Test.txt";
    public string FixedPath = string.Empty;

    private void Start()
    {
        FixedPath = Path.Combine(Application.persistentDataPath, FileName);

        SettingsArgs args = GetSettings();
        Debug.LogWarning(FixedPath);

        Debug.Log($"Fps: {args.fps}");
        Debug.Log($"CameraSpeed: {args.CameraSpeed}");





    }

    

    private string LoadFile()
    {
        if (!File.Exists(FixedPath))
        {
            File.Create(FixedPath);
        }

        string Readed = File.ReadAllText(FixedPath);
        return Readed;
    }

    private void SaveFile(string Jsoned)
    {
        File.WriteAllText(FixedPath, Jsoned);
    }

    private void SaveSettings(SettingsArgs args)
    {
        string Jsoned = JsonUtility.ToJson(args);
        SaveFile(Jsoned);
    }

    public SettingsArgs GetSettings()
    {
        string Jsoned = LoadFile();
        SettingsArgs args = JsonUtility.FromJson<SettingsArgs>(Jsoned);
        if(args == null)
        {
            args = new SettingsArgs();
        }
        return args;
    }







}
