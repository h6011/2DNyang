using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileMng : MonoBehaviour
{
    public static FileMng Instance;

    public class LoadFIleResult
    {
        public bool WasEmpty;
        public string Text;
    }


    private void Awake()
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



    public LoadFIleResult LoadFile(string Path)
    {
        bool WasEmpty = false;
        if (!File.Exists(Path))
        {
            WasEmpty = true;
            FileStream fileStream = File.Create(Path);
            fileStream.Close();
        }

        string Readed = File.ReadAllText(Path);

        return new LoadFIleResult()
        {
            WasEmpty = WasEmpty,
            Text = Readed,
        };
    }

    public void SaveFile(string Path, string Jsoned)
    {
        File.WriteAllText(Path, Jsoned);
    }







}
