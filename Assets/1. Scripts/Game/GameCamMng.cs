using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamMng : MonoBehaviour
{


    private Camera mainCam;

    private float cameraPosX = 0;

    [Header("Stat")]
    [SerializeField] private float cameraMoveSpeed = 3f;

    [Header("Vars")]
    [SerializeField] BoxCollider2D fieldCollider;

    

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void inputKeys()
    {
        float Horizontal = Input.GetAxis("Horizontal");

        cameraPosX += Horizontal * cameraMoveSpeed * Time.deltaTime;

        float boundX = fieldCollider.size.x;
        Vector3 center = fieldCollider.bounds.center;
        Vector3 boundLeft = center + new Vector3(-boundX, 0, 0);

        Vector3 world = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0, 0));
        Vector3 view = mainCam.WorldToViewportPoint(boundLeft);

        Debug.Log($"view.x : {view.x}");

        if (view.x > -1)
        {
            Debug.Log(1);
        }


    }

    private void Update()
    {
        inputKeys();
    }

    private void LateUpdate()
    {
        Vector3 savedPos = mainCam.transform.position;
        mainCam.transform.position = new Vector3(cameraPosX, savedPos.y, savedPos.z);


        

    }





}
