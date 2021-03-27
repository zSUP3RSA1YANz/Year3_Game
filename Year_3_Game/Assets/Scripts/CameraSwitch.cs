using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject oldCamera;
    public GameObject newCamera;

    public void switchCamera()
    {
        oldCamera.SetActive(false);
        newCamera.SetActive(true);
    }
}
