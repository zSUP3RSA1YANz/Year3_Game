using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnGround : MonoBehaviour
{
    public GameObject respawnMarker;

    //respawns Annie
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<CustomPathAI>().respawnPosMark(respawnMarker);
        }
    }
}
