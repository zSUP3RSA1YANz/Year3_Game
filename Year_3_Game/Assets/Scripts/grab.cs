using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D rbPlayer;
    public GameObject player;

    public GameObject jumpDetectorBackground;
    public GameObject respawnMarker;

    public float force;
    public float dragSpeed;

    private bool contact = false;

    Vector3 originSize;
    Vector3 tempSize;

    Vector2 startDirection;

    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        originSize = this.transform.localScale;
        tempSize = this.transform.localScale;
        startDirection = this.transform.up;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (contact)
        {
            dragDown();
            Debug.Log("Moving");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            facePlayer();
            jump();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            rbPlayer = col.gameObject.GetComponent<Rigidbody2D>();
            rbPlayer.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            contact = true;
            Debug.Log("grabbed");
        }
        else if(contact && col.gameObject.tag == "Ground")
        {
            player.GetComponent<CustomPathAI>().respawnPos(respawnMarker.transform.position.x, respawnMarker.transform.position.y);
            jumpDetectorBackground.GetComponent<jumpDetector>().reset();
            contact = false;
            this.transform.up = startDirection;
        }
    }

    public void facePlayer()
    {
        //Vector3 playerPos = player.transform.position;

        //Vector2 direction = new Vector2(
        //    playerPos.x - transform.position.x,
        //    playerPos.y - transform.position.y
        //    );

        //this.transform.up = direction;
    }

    public void jump()
    {
        rb.AddForce(Vector2.up * force);
    }

    void dragDown()
    {
        //rb.bodyType = RigidbodyType2D.Dynamic;
        rb.MovePosition(rb.position + (Vector2.down * dragSpeed * Time.deltaTime));
        rbPlayer.MovePosition(rbPlayer.position + (Vector2.down * dragSpeed * Time.deltaTime));
    }

    void OnMouseDrag()
    {
        silence();
        Debug.Log("silenced");
    }

    void OnMouseUp()
    {
        free();
        GM.stopInteractableEffectLooped();
        Debug.Log("free");
    }

    public void silence()
    {
        if(tempSize.y > 1)
        {
            tempSize.y -= 0.5f;
            GM.playInteractableEffectLooped();
            this.transform.localScale = tempSize;
        }
        //this.transform.lossyScale.Set(this.transform.localScale.x, this.transform.localScale.y / 2, this.transform.localScale.z); 
    }

    void free()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.transform.localScale = originSize;
        tempSize = this.transform.localScale;
    }
}
