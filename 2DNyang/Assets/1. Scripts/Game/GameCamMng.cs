using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamMng : MonoBehaviour
{

    public static GameCamMng Instance;

    private Camera mainCam;



    [Header("Stat")]
    [SerializeField] private float cameraMoveSpeed;

    

    [Header("Vars")]
    [SerializeField] BoxCollider2D fieldCollider;


    [SerializeField] float height;
    [SerializeField] float width;

    [SerializeField] Vector2 mapSize;
    [SerializeField] Vector2 center;

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

    private void Start()
    {
        mainCam = Camera.main;

        float Aspect = (float)Screen.width / (float)Screen.height;

        height = mainCam.orthographicSize;
        width = height * Aspect;

        mapSize = fieldCollider.bounds.size;
        center = fieldCollider.bounds.center;

        cameraMoveSpeed = SettingsMng.Instance.GetSettings().CameraSpeed;
    }

    private float inputKeys()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        if (LobbyMng.Instance.GetIsGamePaused())
        {
            Horizontal = 0;
        }
        return Horizontal;
    }

    private void MoveCamera(float Horizontal)
    {
        float CameraPosX = mainCam.transform.position.x + Horizontal * cameraMoveSpeed * Time.unscaledDeltaTime;

        Vector3 mainCamPos = mainCam.transform.position;
        mainCam.transform.position = new Vector3(CameraPosX, mainCamPos.y, mainCamPos.z);

        float lx = mapSize.x/2 - width;
        float clampX = Mathf.Clamp(mainCam.transform.position.x, -lx + center.x, lx + center.x);


        mainCam.transform.position = new Vector3(clampX, mainCamPos.y, mainCamPos.z);

    }

    private void Update()
    {
        float Horizontal = inputKeys();
        MoveCamera(Horizontal);
    }


    

    //private void LateUpdate()
    //{
    //    //Vector3 savedPos = mainCam.transform.position;
    //    //mainCam.transform.position = new Vector3(cameraPosX, savedPos.y, savedPos.z);

    //    //float boundX = fieldCollider.size.x;
    //    //Vector3 center = fieldCollider.bounds.center;
    //    //Vector3 boundLeft = center + new Vector3(-boundX / 2, 0, 0);

    //    //Vector3 camLeftWorld = mainCam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0));

    //    ////mainCam.transform.position = boundLeft;

    //    //Vector3 BoundLeftView = Camera.main.WorldToViewportPoint(boundLeft);
    //    ////Debug.LogWarning(BoundLeftView.x);

    //    //if (BoundLeftView.x > 0)
    //    //{
    //    //    BoundLeftView.x = 0;

            

    //    //    Vector3 Oppsite = mainCam.ViewportToWorldPoint(BoundLeftView);
    //    //    Vector3 FixPos = Oppsite + new Vector3(boundX / 2, 0, 0);
    //    //    mainCam.transform.position = new Vector3(FixPos.x, savedPos.y, savedPos.z);
    //    //}

       



    //}





}
