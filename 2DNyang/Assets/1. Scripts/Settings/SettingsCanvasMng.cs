using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsCanvasMng : MonoBehaviour
{
    public static SettingsCanvasMng Instance;

    [Header("INS")]
    private SettingsMng settingsMng;


    [Header("Btn")]
    public Button BackBtn;

    [Header("Text")]
    public TMP_Text CameraSpeedSliderDisplayText;
    public TMP_Text MasterVolumeSliderDisplayText;


    [Header("Slider")]
    public Slider CameraSpeedSlider;
    public Slider MasterVolumeSlider;

    [Header("Dropdown")]
    public TMP_Dropdown FpsDropdown;

    [Header("Toggle")]
    public Toggle DisplayFpsToggle;
    public Toggle VSyncToggle;

    [Header("UI Transform")]
    public Transform SettingsOptions;

    [Header("Settings Options")]
    public Transform SettingsOptions_Fps;
    public Transform SettingsOptions_CameraSpeed;

    private void Awake()
    {
        SELF_INS();
    }


    private void Start()
    {
        INSs();
        InteractableUIInital();

        SettingsArgs args = settingsMng.GetSettings();
        DisplaySettings(args);

    }

    private void InteractableUIInital()
    {
        FpsDropDownInital();

        GameStatus.addListenerToBtn(BackBtn, () =>
        {
            SceneManager.LoadScene("Lobby");
        });
        FpsDropdown.onValueChanged.RemoveAllListeners();
        FpsDropdown.onValueChanged.AddListener((int arg1) =>
        {
            OnSettingsChangedByUser();
        });

        CameraSpeedSlider.onValueChanged.RemoveAllListeners();
        CameraSpeedSlider.onValueChanged.AddListener((float arg1) =>
        {
            OnSettingsChangedByUser();
        });

        MasterVolumeSlider.onValueChanged.RemoveAllListeners();
        MasterVolumeSlider.onValueChanged.AddListener((float arg1) =>
        {
            OnSettingsChangedByUser();
        });

        DisplayFpsToggle.onValueChanged.RemoveAllListeners();
        DisplayFpsToggle.onValueChanged.AddListener((bool arg1) =>
        {
            OnSettingsChangedByUser();
        });

        VSyncToggle.onValueChanged.RemoveAllListeners();
        VSyncToggle.onValueChanged.AddListener((bool arg1) =>
        {
            OnSettingsChangedByUser();
        });


    }



    private SettingsArgs GetSettingsByUIInputed()
    {
        return new SettingsArgs()
        {
            fps = (eSettingsFps)FpsDropdown.value,
            CameraSpeed = CameraSpeedSlider.value,
            MasterVolume = MasterVolumeSlider.value,
            DisplayFPS = DisplayFpsToggle.isOn,
            VSync = VSyncToggle.isOn,
        };
    }

    private void OnSettingsChangedByUser()
    {
        SettingsArgs args = GetSettingsByUIInputed();

        settingsMng.SaveSettings(args);

        DisplaySettings(args);

        SettingsMng.Instance.ApplySettings(args);

    }

    private void DisplaySettings(SettingsArgs args)
    {
        CameraSpeedSlider.minValue = 0.1f;
        CameraSpeedSlider.maxValue = 30f;
        CameraSpeedSlider.value = args.CameraSpeed;
        CameraSpeedSliderDisplayText.text = args.CameraSpeed.ToString("F1");

        MasterVolumeSlider.minValue = 0f;
        MasterVolumeSlider.maxValue = 1f;
        MasterVolumeSlider.value = args.MasterVolume;
        MasterVolumeSliderDisplayText.text = args.MasterVolume.ToString("F1");



        FpsDropdown.value = (int)args.fps;

        DisplayFpsToggle.isOn = args.DisplayFPS;
        VSyncToggle.isOn = args.VSync;



    }

    private void FpsDropDownInital()
    {
        FpsDropdown.ClearOptions();

        int FpsEnumCount = System.Enum.GetValues(typeof(eSettingsFps)).Length;

        for (int i = 0; i < FpsEnumCount; i++)
        {
            string EnumName = System.Enum.GetName(typeof(eSettingsFps), i);

            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = EnumName;

            FpsDropdown.options.Add(optionData);

        }
    }

    



    private void INSs()
    {
        settingsMng = SettingsMng.Instance;
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


}
