using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetection : MonoBehaviour
{
    //detects Annie
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            this.GetComponentInParent<grab>().jump();
        }
    }
}
