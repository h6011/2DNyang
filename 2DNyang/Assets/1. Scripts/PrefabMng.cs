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
        INIT_SELF_MNG();
    }

    /// <summary>
    /// Instance = this;
    /// </summary>
    private void INIT_SELF_MNG()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Get Prefab By Name, Using Already Specified Data
    /// </summary>
    /// <param name="Name"></param>
    /// <returns></returns>
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
