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



    }

    private void Update()
    {
        inputKeys();
    }

    private void LateUpdate()
    {
        Vector3 savedPos = mainCam.transform.position;
        mainCam.transform.position = new Vector3(cameraPosX, savedPos.y, savedPos.z);

        float boundX = fieldCollider.size.x;
        Vector3 center = fieldCollider.bounds.center;
        Vector3 boundLeft = center + new Vector3(-boundX / 2, 0, 0);

        Vector3 camLeftWorld = mainCam.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0));

        //mainCam.transform.position = boundLeft;

        Vector3 BoundLeftView = Camera.main.WorldToViewportPoint(boundLeft);
        Debug.LogWarning(BoundLeftView.x);

        if (BoundLeftView.x > 0)
        {
            BoundLeftView.x = 0;

            

            Vector3 Oppsite = mainCam.ViewportToWorldPoint(BoundLeftView);
            Vector3 FixPos = Oppsite + new Vector3(boundX / 2, 0, 0);
            mainCam.transform.position = new Vector3(FixPos.x, savedPos.y, savedPos.z);
        }

       



    }





}
