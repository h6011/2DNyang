using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioArg
{
    public string AudioName;
    public AudioClip Clip;
    public AudioMixerGroup audioMixerGroup;
}

public class AudioMng : MonoBehaviour
{
    public static AudioMng Instance;

    public AudioMixer mainAudioMixer;

    public List<AudioArg> Audios;
    AudioMixer audioMixer;

    public AudioClip GetAudioClip(string Name)
    {
        int count = Audios.Count;
        for (int i = 0; i < count; i++)
        {
            AudioArg arg = Audios[i];
            if (arg.AudioName == Name)
            {
                return arg.Clip;
            }
        }
        return null;
    }

    public AudioArg GetAudioInfo(string Name)
    {
        int count = Audios.Count;
        for (int i = 0; i < count; i++)
        {
            AudioArg arg = Audios[i];
            if (arg.AudioName == Name)
            {
                return arg;
            }
        }
        return null;
    }

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

    private void Start()
    {

    }

    private string GetPath(string Name)
    {
        string Path = $"NewAudio-{Name}";
        return Path;
    }

    public void PlayAudio(string Name, float Volume, bool Looped)
    {
        AudioArg Arg = GetAudioInfo(Name);
        if (Arg != null)
        {
            string PathName = GetPath(Name);
            GameObject NewGameObject = new GameObject(PathName);
            NewGameObject.transform.SetParent(transform);

            AudioSource NewAudioSource = NewGameObject.AddComponent<AudioSource>();
            NewAudioSource.clip = Arg.Clip;
            NewAudioSource.volume = Volume;
            NewAudioSource.loop = Looped;
            NewAudioSource.outputAudioMixerGroup = Arg.audioMixerGroup;
            NewAudioSource.Play();

            if (!Looped)
            {
                float AudioLength = NewAudioSource.clip.length;
                Destroy(NewGameObject, AudioLength);
            }
        }
    }

    public void PlayClickAudio()
    {
        PlayAudio("Click", 0.2f, false);
    }

    public void StopAudio(string Name)
    {
        string PathName = GetPath(Name);
        Transform Find = transform.Find(PathName);
        if (Find)
        {
            Destroy(Find.gameObject);
        }
    }

    public void PauseAudio(string Name)
    {
        string PathName = GetPath(Name);
        Transform Find = transform.Find(PathName);
        if (Find)
        {
            AudioSource audioSource = Find.GetComponent<AudioSource>();
            audioSource.Pause();
        }
    }

    public void UnpauseAudio(string Name)
    {
        string PathName = GetPath(Name);
        Transform Find = transform.Find(PathName);
        if (Find)
        {
            AudioSource audioSource = Find.GetComponent<AudioSource>();
            audioSource.UnPause();
        }
    }



    public void PauseAllAudio()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            AudioSource audioSource = child.GetComponent<AudioSource>();
            audioSource.Pause();
        }
    }

    public void UnpauseAllAudio()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = transform.GetChild(i);
            AudioSource audioSource = child.GetComponent<AudioSource>();
            audioSource.UnPause();
        }
    }


}
