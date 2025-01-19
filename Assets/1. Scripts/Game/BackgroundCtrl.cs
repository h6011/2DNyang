using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCtrl : MonoBehaviour
{

    GameCamMng gameCamMng;

    Camera MainCam;

    [SerializeField] Transform Ground;
    [SerializeField] Transform Tree;
    [SerializeField] Transform Sky;
    [SerializeField] Transform Bush;

    private Vector3 SaveTreePos;
    private Vector3 SaveSkyPos;
    private Vector3 SaveBushPos;


    private void Start()
    {
        gameCamMng = GameCamMng.Instance;
        MainCam = Camera.main;

        SaveTreePos = Tree.position;
        SaveSkyPos = Sky.position;
        SaveBushPos = Bush.position;
    }


    private void Update()
    {
        float x = MainCam.transform.position.x;

        //Tree.position = SaveTreePos + new Vector3(x / 40, 0);
        Sky.position = SaveSkyPos + new Vector3(x / 5, 0);
        Bush.position = SaveBushPos + new Vector3(x / 20, 0);


    }






}
