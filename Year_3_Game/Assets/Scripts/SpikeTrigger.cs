using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    GameObject spike;
    SpikeFall spikeScript;

    // Start is called before the first frame update
    void Start()
    {
        spike = transform.GetChild(0).gameObject;
        spikeScript = spike.GetComponent<SpikeFall>();
    }

    //makes spike fall
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !spikeScript.isGrounded)
        {
            Debug.Log("PlayerIn");
            spikeScript.activateFall = true;
        }
    }
}
