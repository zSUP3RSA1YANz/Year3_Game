using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraSwitch : MonoBehaviour
{
    //public GameObject oldC;
    public GameObject newC;

    //keeps camera on as Annie stays in area
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player") && !newC.activeSelf)
        {
            newC.SetActive(true);
        }
    }

    //switches camera when Annie leaves area
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            newC.SetActive(false);
        }
    }
}
