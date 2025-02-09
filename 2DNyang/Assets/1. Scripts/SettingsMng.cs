using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public enum eSettingsFps
{
    NoLimit,
    Fps10,
    Fps24,
    Fps30,
    Fps48,
    Fps60,
    Fps144,
}

[System.Serializable]
public class SettingsArgs
{
    public eSettingsFps fps = eSettingsFps.NoLimit;
    public float CameraSpeed = 5f;
    public float MasterVolume = 1f;
    public bool DisplayFPS = false;
    public bool VSync = true;
}

public class SettingsMng : MonoBehaviour
{
    public static SettingsMng Instance;


    private string FileName = "Settings.txt";
    public string FixedPath = string.Empty;

    private SettingsArgs CurrentSettings;


    private void Awake()
    {
        SELF_INS();
    }

    private void Start()
    {
        FixedPath = Path.Combine(Application.persistentDataPath, FileName);

        CurrentSettings = loadSettingsOfFile();


        //Debug.LogWarning(FixedPath);

        //PrintCurrentSettings();

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


    private void PrintCurrentSettings()
    {
        Debug.Log($"Fps: {CurrentSettings.fps}");
        Debug.Log($"CameraSpeed: {CurrentSettings.CameraSpeed}");
    }
    
    public void ApplySettings(SettingsArgs args)
    {

        OnFpsChanged(args.fps);
        OnVSyncChanged(args);
        OnMasterVolumeChanged(args);



    }

    private void OnMasterVolumeChanged(SettingsArgs args)
    {
        float minVolume = -20f;
        float maxVolume = 20f;

        float fixedVolume = Mathf.Lerp(minVolume, maxVolume, args.MasterVolume);

        AudioMng.Instance.mainAudioMixer.SetFloat("Master", fixedVolume);
    }

    private void OnVSyncChanged(SettingsArgs args)
    {
        if (args.VSync)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    private void OnFpsChanged(eSettingsFps fps)
    {
        if (fps == eSettingsFps.NoLimit)
        {
            Application.targetFrameRate = -1;
        }
        else if (fps == eSettingsFps.Fps10)
        {
            Application.targetFrameRate = 10;
        }
        else if (fps == eSettingsFps.Fps24)
        {
            Application.targetFrameRate = 24;
        }
        else if (fps == eSettingsFps.Fps30)
        {
            Application.targetFrameRate = 30;
        }
        else if (fps == eSettingsFps.Fps48)
        {
            Application.targetFrameRate = 48;
        }
        else if (fps == eSettingsFps.Fps60)
        {
            Application.targetFrameRate = 60;
        }
        else if (fps == eSettingsFps.Fps144)
        {
            Application.targetFrameRate = 144;
        }
    }


    public void SaveSettings(SettingsArgs args)
    {
        CurrentSettings = args;
    }

    /// <summary>
    /// 현재 저장한값을 불러옴
    /// </summary>
    /// <returns></returns>
    public SettingsArgs GetSettings()
    {
        return CurrentSettings;
    }


    public void saveSettingsOfFile(SettingsArgs args)
    {
        string Jsoned = JsonUtility.ToJson(args);
        FileMng.Instance.SaveFile(FixedPath, Jsoned);
    }

    public SettingsArgs loadSettingsOfFile()
    {
        FileMng.LoadFIleResult Result = FileMng.Instance.LoadFile(FixedPath);
        SettingsArgs args = JsonUtility.FromJson<SettingsArgs>(Result.Text);
        if (args == null)
        {
            args = new SettingsArgs();
        }
        return args;
    }

    private void OnApplicationQuit()
    {
        saveSettingsOfFile(CurrentSettings);
    }




}
