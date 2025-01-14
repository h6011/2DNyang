using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


[System.Serializable]
public class PrefabMngArg
{
    public string Name;
    public GameObject PrefabLocation;
}

public class PrefabMng : MonoBehaviour
{
    public static PrefabMng Instance;

    public List<PrefabMngArg> PrefabSettings;


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

    public GameObject GetPrefabByName(string Name)
    {
        int count = PrefabSettings.Count;
        for (int i = 0; i < count; i++)
        {
            PrefabMngArg arg = PrefabSettings[i];
            if (arg.Name == Name)
            {
                return arg.PrefabLocation;
            }
        }
        return null;
    }








}
