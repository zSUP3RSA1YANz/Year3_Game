using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    public float speed = 2f;

    public bool activateFall = false;
    public bool canKill = false;
    public bool isGrounded = false;

    //public GameObject respawnMarker;

    Rigidbody2D rb;

    Vector3 originPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        originPos = transform.position;
    }

    void FixedUpdate()
    {
        if (activateFall)
        {
            moveSpike(Vector2.down);
            canKill = true;
        }
        else
            canKill = false;
    }
     
    //moves spike
    void moveSpike(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    //decides whether spike can kill or not
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && canKill)
        {
            GetComponentInParent<SpikeManager>().playerRespawn();
            GetComponentInParent<SpikeManager>().resetAllSpikes();
        }
        else if(col.gameObject.tag == "Ground")
        {
            GetComponent<PolygonCollider2D>().isTrigger = true;
            GetComponentInParent<BoxCollider2D>().enabled = false;
            isGrounded = true;
            activateFall = false;
            canKill = false;
        }
    }

    //reset spike pos
    public void reset()
    {
        GetComponent<PolygonCollider2D>().isTrigger = false; 
        GetComponentInParent<BoxCollider2D>().enabled = true;
        activateFall = false;
        transform.position = originPos;
        isGrounded = false;
    }
}
