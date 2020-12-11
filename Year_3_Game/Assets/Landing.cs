using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{
    Rigidbody2D rb;

    void OncollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            rb = col.gameObject.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }
    }
}
