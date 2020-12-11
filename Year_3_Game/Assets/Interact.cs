using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Interact : MonoBehaviour
{
    private bool isSelected = false;
    private bool isClimbing = false;
    private bool correctingPos = false;

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


    private Vector2 LadderBase;
    private Vector2 direction;

    void Start()
    {
        path = player.GetComponent<CustomPathAI>();
    }

    void Update()
    {
        //if(isSelected)
        //{
        //    PlayerPrefs.SetInt("isSelected", 1);

        //    PlayerPrefs.SetFloat("newTargX", this.transform.position.x);
        //    PlayerPrefs.SetFloat("newTargY", this.transform.position.y - ((CalculateInteractHeight() / 2) - (PlayerPrefs.GetFloat("CharacterHeight") / 2)));
        //    PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);


        //    LadderBase = new Vector2(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"));


        //    //Debug.Log(LadderBase.x);
        //    //Debug.Log(LadderBase.y);

        //    //targetPosAchieved();
        //}
    }

    void FixedUpdate()
    {
        if (correctingPos)
        {
            updateDir();
            correctPos();
        }
    }

    void passOnInfo()
    {
        PlayerPrefs.SetFloat("newTargX", this.transform.position.x);
        PlayerPrefs.SetFloat("newTargY", this.transform.position.y - ((CalculateInteractHeight() / 2) - (PlayerPrefs.GetFloat("CharacterHeight") / 2)));
        PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);

        LadderBase = new Vector2(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"));
    }

    void correctPos()
    {
        if (Mathf.Abs(rb.position.x - LadderBase.x) > interactDistance)
        {
            rb.MovePosition((Vector2)rb.position + (direction * speed * Time.deltaTime));
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            climb();
        }
    }

    void updateDir()
    {
        direction = (LadderBase - rb.position).normalized;
    }

    private void OnMouseDown()
    {
        if (leftMouseClicked())
        {
            //isSelected = true;
            PlayerPrefs.SetInt("isSelected", 1);
            passOnInfo();
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));

            playerDetector.SetActive(false);
            Debug.Log("ladder selected");

            //if(PlayerPrefs.GetInt("isSelected") == 1)
            //{
            //    PlayerPrefs.SetInt("isSelected", 0);
            //}
        }


        //Debug.Log(isSelected);
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
        return this.transform.localPosition;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerPrefs.SetInt("pathChangable", 0);

        rb = col.GetComponent<Rigidbody2D>();

        player = col.gameObject;
        player.GetComponent<CustomPathAI>().enabled = false;

        if (PlayerPrefs.GetInt("isSelected") == 1 && col.tag == "Player")
        {
            correctingPos = true;
        }
        //Debug.Log((rb.position - LadderBase).magnitude);
    }

    void climb()
    {
        rb.MovePosition(rb.position + (Vector2.up * climbSpeed * Time.deltaTime));
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Exit");

            correctingPos = false;

            LadderExit();
        }
    }

    void LadderExit()
    {
        this.GetComponent<BoxCollider2D>().isTrigger = false;
        PlayerPrefs.SetInt("isSelected", 0);
        PlayerPrefs.SetInt("pathChangable", 0);
        player.GetComponent<CustomPathAI>().enabled = true;
        playerDetector.SetActive(true);
        Debug.Log("on");
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce((Vector2.up + Vector2.right) * jumpForce);
    }
}
