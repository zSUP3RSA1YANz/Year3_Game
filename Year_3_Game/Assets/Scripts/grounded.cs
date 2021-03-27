using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grounded : MonoBehaviour
{
    public GameObject player;
    public GameObject ladder;

    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        if(ladder == null)
        {
            ladder = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            player.GetComponent<CustomPathAI>().isGrounded = true;
            player.GetComponent<CustomPathAI>().isAirborne = false;
            Debug.Log("landed");
        }

        if(ladder!= null)
        activateLadder();
    }

    void activateLadder()
    {
        if(ladder.GetComponent<BoxCollider2D>().isTrigger == false)
        {
            ladder.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
