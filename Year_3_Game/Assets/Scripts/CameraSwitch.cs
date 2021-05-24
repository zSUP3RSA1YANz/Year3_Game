using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject oldCamera;
    public GameObject newCamera;

    //causes camera transiton by deactivating old one and activating new 
    public void switchCamera()
    {
        oldCamera.SetActive(false);
        newCamera.SetActive(true);
    }
}
