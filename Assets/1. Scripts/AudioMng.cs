using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AudioArg
{
    public string AudioName;
    public AudioClip Clip;
}

public class AudioMng : MonoBehaviour
{
    public static AudioMng Instance;

    public List<AudioArg> Audios;

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

    private string GetPath(string Name)
    {
        string Path = $"NewAudio-{Name}";
        return Path;
    }

    public void PlayAudio(string Name, float Volume, bool Looped)
    {
        AudioClip AudioClip = GetAudioClip(Name);
        if (AudioClip)
        {
            string PathName = GetPath(Name);
            GameObject NewGameObject = new GameObject(PathName);
            NewGameObject.transform.SetParent(transform);

            AudioSource NewAudioSource = NewGameObject.AddComponent<AudioSource>();
            NewAudioSource.clip = AudioClip;
            NewAudioSource.volume = Volume;
            NewAudioSource.loop = Looped;
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
            Destroy(Find);
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
