using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : MonoBehaviour
{
    Rigidbody2D rb;
    Rigidbody2D rbPlayer;

    public GameObject player;
    private CustomPathAI path;

    private bool contact = false;

    public float speed = 3f;

    private bool isSelected = false;
    public bool isMovable = false;
    private bool canPush = false;

    private int positon;

    public float jumpPrepTime = 1f;

    public GameObject playerDetector;
    public GameObject pathController;
    private AstarPath pathControllerScript;

    public GameObject leftPosMark;
    public GameObject leftJPosMark;

    public GameObject rightPosMark;
    public GameObject rightJPosMark;

    public GameObject topPosMark;

    public GameManager GM;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        path = player.GetComponent<CustomPathAI>();
        pathControllerScript = pathController.GetComponent<AstarPath>();
    }

    void Update()
    {
        if(leftMouseClicked())
        {
            checkForBoxInteraction();
        }
    }

    void FixedUpdate()
    {
        if (contact)
        {
            moveBox();
            Debug.Log("Moving");
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

    //Checks which side of the box the player has clicked
    public void checkForBoxInteraction()
    {
        if (mouseInLeftBoxArea())
        {
            playerDetector.SetActive(false);
            PlayerPrefs.SetInt("isSelected", 1);
            GM.playInteractableEffect();
            Debug.Log("LeftSelected");
            passOnInfo(1);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));

        }
        else if (mouseInRightBoxArea())
        {
            playerDetector.SetActive(false);
            PlayerPrefs.SetInt("isSelected", 1);
            GM.playInteractableEffect();
            Debug.Log("RightSelected");
            passOnInfo(2);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));

        }
        else if (mouseInTopBoxArea())
        {
            playerDetector.SetActive(true);
            PlayerPrefs.SetInt("isSelected", 1);
            GM.playInteractableEffect();
            Debug.Log("TopSelected = " + PlayerPrefs.GetInt("isSelected"));
            passOnInfo(3);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        }
        //Distinguishes between pushable box or not
        else if (mouseInBoxArea() && isMovable)
        {
            playerDetector.SetActive(false);
            PlayerPrefs.SetInt("isSelected", 1);
            GM.playInteractableEffect();
            Debug.Log("BoxSelected = " + PlayerPrefs.GetInt("isSelected"));
            canPush = true;
            passOnInfo(4);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        }
    }

    //Calculates mouse posiition
    Vector3 CalculatedMousePos()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

    //Simulates Annie and box moving
    void moveBox()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.MovePosition(rb.position + (Vector2.right * speed * Time.deltaTime));
        rbPlayer.MovePosition(rbPlayer.position + (Vector2.right * speed * Time.deltaTime));
    }

    void passOnInfo(int pos)
    {
        //LeftBox is clicked
        if (pos == 1)
        {
            if (playerIsLeftOfBox())
            {
                PlayerPrefs.SetFloat("newTargX", leftPosMark.transform.position.x);
                PlayerPrefs.SetFloat("newTargY", leftPosMark.transform.position.y);
                PlayerPrefs.SetFloat("newTargZ", leftPosMark.transform.position.z);
            }
            else if (playerIsRightOfBox())
            {
                PlayerPrefs.SetFloat("newTargX", rightJPosMark.transform.position.x);
                PlayerPrefs.SetFloat("newTargY", rightJPosMark.transform.position.y);
                PlayerPrefs.SetFloat("newTargZ", rightJPosMark.transform.position.z);

                player.GetComponent<CustomPathAI>().jumpFromRightBox = true;
            }
            else
            {
                PlayerPrefs.SetFloat("newTargX", leftJPosMark.transform.position.x);
                PlayerPrefs.SetFloat("newTargY", leftJPosMark.transform.position.y);
                PlayerPrefs.SetFloat("newTargZ", leftJPosMark.transform.position.z);
                Debug.Log("Player on top");
            }
        }
        //RightBox is clicked
        else if (pos == 2)
        {
            if(playerIsRightOfBox())
            {
                PlayerPrefs.SetFloat("newTargX", rightPosMark.transform.position.x);
                PlayerPrefs.SetFloat("newTargY", rightPosMark.transform.position.y);
                PlayerPrefs.SetFloat("newTargZ", rightPosMark.transform.position.z);
            }
            else if(playerIsLeftOfBox())
            {
                PlayerPrefs.SetFloat("newTargX", leftJPosMark.transform.position.x);
                PlayerPrefs.SetFloat("newTargY", leftJPosMark.transform.position.y);
                PlayerPrefs.SetFloat("newTargZ", leftJPosMark.transform.position.z);

                player.GetComponent<CustomPathAI>().jumpFromLeftBox = true;
            }
            else
            {
                PlayerPrefs.SetFloat("newTargX", rightJPosMark.transform.position.x);
                PlayerPrefs.SetFloat("newTargY", rightJPosMark.transform.position.y);
                PlayerPrefs.SetFloat("newTargZ", rightJPosMark.transform.position.z);
                Debug.Log("Player on top");
            }
        }
        //TopBox is clicked
        else if (pos == 3)
        {
            //player is left of box
            if(playerIsLeftOfBox())
            {
                PlayerPrefs.SetFloat("newTargX", leftJPosMark.transform.position.x);
                PlayerPrefs.SetFloat("newTargY", leftJPosMark.transform.position.y);
                PlayerPrefs.SetFloat("newTargZ", leftJPosMark.transform.position.z);
                
                player.GetComponent<CustomPathAI>().jumpFromLeftBox = true;
            }
            else if (playerIsRightOfBox())
            {
                PlayerPrefs.SetFloat("newTargX", rightJPosMark.transform.position.x);
                PlayerPrefs.SetFloat("newTargY", rightJPosMark.transform.position.y);
                PlayerPrefs.SetFloat("newTargZ", rightJPosMark.transform.position.z);

                player.GetComponent<CustomPathAI>().jumpFromRightBox = true;
            }
        }
        //Movebox 
        else if (pos == 4)
        {
            PlayerPrefs.SetFloat("newTargX", this.transform.position.x);
            PlayerPrefs.SetFloat("newTargY", this.transform.position.y);
            PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(canPush)
        {
            if (col.gameObject.tag == "Player" && playerIsLeftOfBox())
            {
                contact = true;
                Debug.Log("contact =" + contact);
                rbPlayer = col.gameObject.GetComponent<Rigidbody2D>();
            }
            //Stops box moving when collides agianst wall
            else if (col.gameObject.tag == "Obstacle")
            {
                contact = false;
                canPush = false;
                isMovable = false;
                pathControllerScript.Scan();
            }
        }
    }

    //Defines the left region of the clickable area
    bool mouseInLeftBoxArea()
    {
        if ((CalculatedMousePos().x > this.transform.position.x - (3*(this.transform.localScale.x / 2))) && (CalculatedMousePos().x < this.transform.position.x - (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > this.transform.position.y - (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (this.transform.localScale.y / 2)))
        {
            return true;
        }

        return false;
    }

    //Defines the right region of the clickable area
    bool mouseInRightBoxArea()
    {
        if ((CalculatedMousePos().x < this.transform.position.x + (3*(this.transform.localScale.x / 2))) && (CalculatedMousePos().x > this.transform.position.x + (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > this.transform.position.y - (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (this.transform.localScale.y / 2)))
        {
            return true;
        }

        return false;
    }

    //Defines the top region of the clickable area
    bool mouseInTopBoxArea()
    {
        if ((CalculatedMousePos().x > this.transform.position.x - (this.transform.localScale.x / 2)) && (CalculatedMousePos().x < this.transform.position.x + (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > this.transform.position.y + (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (3*(this.transform.localScale.y / 2))))
        {
            return true;
        }

        return false;
    }

    //Defines the region of the clickable area in the box
    bool mouseInBoxArea()
    {
        if ((CalculatedMousePos().x > this.transform.position.x - (this.transform.localScale.x / 2)) && (CalculatedMousePos().x < this.transform.position.x + (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > this.transform.position.y - (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (this.transform.localScale.y / 2)))
        {
            return true;
        }

        return false;
    }

    bool playerIsLeftOfBox()
    {
        if((player.transform.position.x + Mathf.Abs(player.transform.localScale.x / 2)) <= (this.transform.position.x - (this.transform.localScale.x / 2)))
        {
            Debug.Log("Player left of box");
            Debug.Log((player.transform.position.x + (player.transform.localScale.x / 2)));
            Debug.Log((this.transform.position.x - (this.transform.localScale.x / 2)));
            return true;
        }
        else
        {
            return false;
        }
    }

    bool playerIsRightOfBox()
    {
        if((player.transform.position.x - Mathf.Abs(player.transform.localScale.x / 2)) >= (this.transform.position.x + (this.transform.localScale.x / 2)))
        {
            Debug.Log("Player right of box");
            return true;
        }
        else
        {
            return false;
        }
    }

    bool playerReachedLeft()
    {
        return false;
    }
}
