using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    /*
    public GameObject camera1;
    public GameObject camera2;
    public GameObject camera3;
    public GameObject MainCamera;*/
    public Camera[] cams;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            MainCam();
        }
        else if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            Cam1();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Cam2();
        }
    }

    public void MainCam()
    {
        cams[0].enabled = true;
        cams[1].enabled = false;
        cams[2].enabled = false;
    }
    public void Cam1()
    {
        cams[0].enabled = false;
        cams[1].enabled = true;
        cams[2].enabled = false;
    }
    public void Cam2()
    {
        cams[0].enabled = false;
        cams[1].enabled = false;
        cams[2].enabled = true;
    }
}