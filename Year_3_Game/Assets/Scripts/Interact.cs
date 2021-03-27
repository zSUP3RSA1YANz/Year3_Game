using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Interact : MonoBehaviour
{
    private bool isSelected = false;
    private bool isClimbing = false;
    private bool correctingPosBase = false;
    private bool correctingPosTop = false;

    public float interactableWidth = 0;
    public float interactableHeight = 0;

    public float interactDistance = 10;

    public float jumpForce = 3;

    public float climbSpeed = 3;
    public float speed = 3;

    Rigidbody2D rb;

    public GameObject playerDetector;
    public GameObject player;
    private CustomPathAI path;

    public GameObject ladderB;
    public GameObject ladderT;


    private Vector2 ladderBase;
    private Vector2 ladderTop;
    private Vector2 direction;

    public GameManager GM;

    void Start()
    {
        path = player.GetComponent<CustomPathAI>();

        rb = player.GetComponent<Rigidbody2D>();

        ladderBase = ladderB.transform.position;
        ladderTop = ladderT.transform.position;
    }

    void FixedUpdate()
    {
        if (correctingPosBase)
        {
            updateDirBase();
            correctPosBase();
        }
        else if (correctingPosTop)
        {
            updateDirTop();
            correctPosTop();
        }
    }

    void passOnInfoBase()
    {
        PlayerPrefs.SetFloat("newTargX", ladderBase.x);
        PlayerPrefs.SetFloat("newTargY", ladderBase.y);
        PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
    }

    void passOnInfoTop()
    {
        PlayerPrefs.SetFloat("newTargX", ladderTop.x);
        PlayerPrefs.SetFloat("newTargY", ladderTop.y);
        PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
    }

    void correctPosBase()
    {
        if (Mathf.Abs(rb.position.x - ladderBase.x) > interactDistance)
        {
            rb.MovePosition((Vector2)rb.position + (direction * speed * Time.deltaTime));
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            climbUp();
        }
    }

    void correctPosTop()
    {
        if (Mathf.Abs(rb.position.x - ladderTop.x) > interactDistance)
        {
            rb.MovePosition((Vector2)rb.position + (direction * speed * Time.deltaTime));
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            climbDown();
        }
    }

    void updateDirBase()
    {
        direction = (ladderBase - rb.position).normalized;
    }

    void updateDirTop()
    {
        direction = (ladderTop - rb.position).normalized;
    }

    private void OnMouseDown()
    {
        if (leftMouseClicked())
        {
            PlayerPrefs.SetInt("isSelected", 1);
            GM.playInteractableEffect();
            if (player.transform.position.y < ObjectPos().y)
            {
                passOnInfoBase();
            }
            else if (player.transform.position.y > ObjectPos().y)
            {
                passOnInfoTop();
            }
                path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));

            playerDetector.SetActive(false);
        }
    }

    bool leftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
            return false;
    }

    bool mouseOutsideInteractable()
    {
        if ((CalculatedMousePos().x > ObjectPos().x + (CalculateInteractWidth() / 2)) || (CalculatedMousePos().x < ObjectPos().x - (CalculateInteractWidth() / 2)) ||
            (CalculatedMousePos().y > ObjectPos().y + (CalculateInteractHeight() / 2)) || (CalculatedMousePos().y < ObjectPos().y - (CalculateInteractHeight() / 2)))
        {
            return true;
        }

        return false;
    }

    Vector3 CalculatedMousePos()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

    float CalculateInteractWidth()
    {
        float width = this.transform.localScale.x;

        return width;
    }

    float CalculateInteractHeight()
    {
        float height = this.transform.localScale.y;

        return height;
    }

    Vector3 ObjectPos()
    {
        return this.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (PlayerPrefs.GetInt("isSelected") == 1 && col.tag == "Player")
        {
            if(player.transform.position.y < ObjectPos().y)
            {
                correctingPosBase = true;
                PlayerPrefs.SetInt("pathChangable", 0);
                player.GetComponent<CustomPathAI>().enabled = false;
            }
        }
    }

    void climbUp()
    {
        rb.MovePosition(rb.position + (Vector2.up * climbSpeed * Time.deltaTime));
        player.GetComponent<CustomPathAI>().isGrounded = false;
    }

    void climbDown()
    {
        if(player.GetComponent<CustomPathAI>().isGrounded == false)
        {
            rb.MovePosition(rb.position + (Vector2.down * climbSpeed * Time.deltaTime));
        }
        else if(player.GetComponent<CustomPathAI>().isGrounded)
        {
            LadderExitBase();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player" && PlayerPrefs.GetInt("isSelected") == 1 && correctingPosBase)
        {
            PlayerPrefs.SetInt("isSelected", 0);

            correctingPosBase = false;

            LadderExitTop();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && PlayerPrefs.GetInt("isSelected") == 1)
        {
            correctingPosTop = true;
        }
    }

    void LadderExitTop()
    {
        this.GetComponent<BoxCollider2D>().isTrigger = false;
        PlayerPrefs.SetInt("isSelected", 0);
        PlayerPrefs.SetInt("pathChangable", 0);
        player.GetComponent<CustomPathAI>().enabled = true;
        playerDetector.SetActive(true);
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }

    void LadderExitBase()
    {
        this.GetComponent<BoxCollider2D>().isTrigger = true;
        PlayerPrefs.SetInt("isSelected", 0);
        PlayerPrefs.SetInt("pathChangable", 0);
        player.GetComponent<CustomPathAI>().enabled = true;
        playerDetector.SetActive(true);
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        correctingPosTop = false;
    }
}
