using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnTrigger : MonoBehaviour
{
    public GameObject respawnMarker;
    private GameObject player;

    public bool jumpArea = false;

    public GameObject jumpDetectorManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Respawns Annie
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Out of Bounds");
            player.GetComponent<CustomPathAI>().respawnPosMark(respawnMarker);

            if(jumpArea)
            {
                jumpDetectorManager.GetComponent<jumpDetectorManager>().resetJumpDetectors();
            }
        }
    }
}
