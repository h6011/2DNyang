using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{

    [System.Serializable]
    public class StageInfoClass
    {
        public int index;
        public string name;
        public string description;
    }

    public static List<StageInfoClass> StageInfos = new List<StageInfoClass>()
    {
        new StageInfoClass() { index = 1, name = "Stage1" },
        new StageInfoClass() { index = 2, name = "Stage2" }
    };


    public static bool isLobbyLoaded = false;




}
