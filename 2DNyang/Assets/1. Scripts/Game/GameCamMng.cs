using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamMng : MonoBehaviour
{
    public enum GameCamMngState
    {
        Default,
        Disabled,
        Custom,
    }


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

    public Vector3 saveCameraPos;

    private GameCamMngState currentState;

    public GameCamMngState CurrentState
    {
        get { return currentState; }
        set
        {
            if (currentState == GameCamMngState.Custom && value == GameCamMngState.Default)
            {
                // Custom > Default
                mainCam.transform.position = saveCameraPos;
            }
            currentState = value;
        }
    }

    public Vector2 CustomPosition = Vector2.zero;


    private void Awake()
    {
        SELF_INS();
    }

    private void Start()
    {
        mainCam = Camera.main;

        saveCameraPos = mainCam.transform.position;

        float Aspect = (float)Screen.width / (float)Screen.height;

        height = mainCam.orthographicSize;
        width = height * Aspect;

        mapSize = fieldCollider.bounds.size;
        center = fieldCollider.bounds.center;

        cameraMoveSpeed = SettingsMng.Instance.GetSettings().CameraSpeed;
    }

    private void Update()
    {
        if (currentState == GameCamMngState.Default)
        {
            float Horizontal = inputKeys();
            MoveCamera(Horizontal);
        }
        else if (currentState == GameCamMngState.Disabled)
        {

        }
        else if (currentState == GameCamMngState.Custom)
        {
            CustomMoveCamera();
        }
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

    private void CustomMoveCamera()
    {
        float deltaTime = Time.deltaTime;
        float FollowTime = 0.5f;//saveCameraPos
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(CustomPosition.x, CustomPosition.y, saveCameraPos.z), deltaTime / FollowTime);
    }

    





}
