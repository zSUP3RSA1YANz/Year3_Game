using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public GameManager GM;
    public GameObject posMark;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GM.teleportPlayer(posMark);
        }
    }
}
