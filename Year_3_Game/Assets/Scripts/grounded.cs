using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grounded : MonoBehaviour
{
    public GameObject player;

    public bool isGrounded = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        //Annie is on the floor
        if(col.tag == "Player")
        {
            player.GetComponent<CustomPathAI>().isGrounded = true;
            player.GetComponent<CustomPathAI>().isAirborne = false;
            Debug.Log("landed");
        }
        //spikes cannot harm Annie
        else if(col.tag == "Spike")
        {
            col.GetComponent<SpikeFall>().activateFall = false;
        }
    }
}
