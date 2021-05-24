using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D rbPlayer;
    private GameObject player;

    public GameObject respawnMarker;

    public float force;
    public float dragSpeed;

    private bool contact = false;

    private GameObject playerDetector;

    Vector3 originSize;
    Vector3 tempSize;

    Vector2 startDirection;

    private GameManager GM;

    public GameObject jumpDetectorManager;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerDetector = transform.GetChild(0).gameObject;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        originSize = this.transform.localScale;
        tempSize = this.transform.localScale;
        startDirection = this.transform.up;
    }

    void FixedUpdate()
    {
        //pulls Annie down 
        if (contact)
        {
            dragDown();
            Debug.Log("Moving");
        }
    }

    //detects Annie
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            jump();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //grabs Annie
        if (col.gameObject.tag == "Player")
        {
            rbPlayer = col.gameObject.GetComponent<Rigidbody2D>();
            rbPlayer.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            contact = true;
            Debug.Log("grabbed");
        }
        //Respawns annie
        else if(contact && col.gameObject.tag == "Ground")
        {
            jumpDetectorManager.GetComponent<jumpDetectorManager>().resetJumpDetectors();
            player.GetComponent<CustomPathAI>().respawnPos(respawnMarker.transform.position.x, respawnMarker.transform.position.y);
            contact = false;
            this.transform.up = startDirection;
            playerDetector.SetActive(true);
        }
    }

    //adds upwards force
    public void jump()
    {
        rb.AddForce(Vector2.up * force);
    }

    //moves down
    void dragDown()
    {
        rb.MovePosition(rb.position + (Vector2.down * dragSpeed * Time.deltaTime));
        rbPlayer.MovePosition(rbPlayer.position + (Vector2.down * dragSpeed * Time.deltaTime));
    }

    //silenced with mouse
    void OnMouseDown()
    {
        silence();
        Debug.Log("silenced");
    }

    //resume detecting
    void OnMouseUp()
    {
        free();
        GM.stopInteractableEffectLooped();
        Debug.Log("free");
    }

    //shrinks when silenced
    public void silence()
    {
        if(tempSize.y > 1)
        {
            playerDetector.SetActive(false);
            tempSize.y -= 0.5f;
            GM.playInteractableEffectLooped();
            this.transform.localScale = tempSize;
        }
    }

    //resumes detecting Annie
    void free()
    {
        playerDetector.SetActive(true);
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.transform.localScale = originSize;
        tempSize = this.transform.localScale;
    }
}
