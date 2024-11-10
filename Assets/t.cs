using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t : MonoBehaviour
{

    Camera mainCam;

    

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        //Debug.Log(mainCam.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)));
        //transform.position = Camera.main.scaledPixelWidth

    }
}
