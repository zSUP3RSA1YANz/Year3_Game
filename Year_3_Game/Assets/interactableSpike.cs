using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableSpike : MonoBehaviour
{
    public GameObject player;
    private CustomPathAI path;

    private GameManager GM;
    private SpikeFall fallScript;

    public bool canCarry = false;
    public bool carry = false;

    private SpriteRenderer spriteRenderer;

    public Vector3 rotation;
    public GameObject cageClickDetector;

    private Rigidbody2D rb;
    private Rigidbody2D playerRb;

    private Vector3 localScale;

    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        path = player.GetComponent<CustomPathAI>();
        GM = FindObjectOfType<GameManager>();
        fallScript = GetComponent<SpikeFall>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerRb = player.GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //makes spike render ahead of Annie
        if(fallScript.isGrounded && gameObject.layer == 0)
        {
            gameObject.layer = 11;
        }

        //stops carry from overloading
        if(canCarry == false && carry)
        {
            carry = false;
        }
    }

    void FixedUpdate()
    {
        //switches spike direction based on Annies
        if (carry)
        {
            transform.position = player.transform.position;

            if (playerRb.velocity.x >= 0.01f)
            {
                transform.localScale = Vector3.Scale(localScale, new Vector3(1f, -1f, 1f));
            }
            else if (playerRb.velocity.x <= -0.01f)
            {
                transform.localScale = Vector3.Scale(localScale, new Vector3(1f, 1f, 1f));
            }
        }
    }

    //detects mouse click
    void OnMouseDown()
    {
        PlayerPrefs.SetInt("isSelected", 1);
        GM.playInteractableEffect();
        canCarry = true;
        path.setTargetPosition(transform.position.x, transform.position.y, transform.position.z);
    }

    //picks up spike when in area
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && canCarry)
        {
            fallScript.enabled = false;
            carry = true;
            transform.Rotate(rotation);
            spriteRenderer.sortingOrder = 3;
            cageClickDetector.SetActive(true);
        }
    }

    //activates animation when hits cage
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Cage"))
        {
            anim.SetBool("cageFallAnim", true);
            gameObject.SetActive(false);
        }
    }
}
