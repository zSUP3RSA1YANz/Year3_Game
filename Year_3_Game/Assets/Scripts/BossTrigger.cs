using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject spikeMan;

    public GameObject oldCamera;
    public GameObject newCamera;

    //Activates Enemy if Annie stays in zone
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (!spikeMan.GetComponent<EnemyShooting>().inBossZone)
            {
                newCamera.SetActive(true);
                spikeMan.GetComponent<EnemyShooting>().inBossZone = true;
            }
            else
                return;
        }
    }

    //Deactivates Enemy if Annie stays in zone
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            spikeMan.GetComponent<EnemyShooting>().inBossZone = false;
            newCamera.SetActive(false);
        }
    }
}
