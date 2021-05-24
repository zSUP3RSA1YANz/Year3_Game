using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlapperStartTargeting : MonoBehaviour
{
    //call to start following Annie
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            GetComponentInParent<EnemyFlapperMangager>().startTargetingPlayer();
        }
    }
}
