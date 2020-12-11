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

    public GameObject playerDetector;
    public GameObject pathController;
    private AstarPath pathControllerScript;


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
        //if (isMovable)
        //{
        //    if (isSelected)
        //    {
        //        PlayerPrefs.SetInt("moveToInteractable", 1);

        //        PlayerPrefs.SetFloat("newTargX", this.transform.position.x);
        //        PlayerPrefs.SetFloat("newTargY", this.transform.position.y);
        //        PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
        //    }

        //    //if (leftMouseClicked() && mouseOutsideInteractable())
        //    //{
        //    //    isSelected = false;
        //    //    PlayerPrefs.SetInt("moveToInteractable", 0);
        //    //}

        //}

        //if (!isMovable)
        //{
        //    if(leftMouseClicked() && mouseInLeftBoxArea())
        //    {
        //        playerDetector.SetActive(false);
        //        PlayerPrefs.SetInt("isSelected", 1);
        //        Debug.Log("LeftSelected");
        //        passOnInfo(1);
        //        Debug.Log(PlayerPrefs.GetFloat("newTargX"));
        //        Debug.Log(PlayerPrefs.GetFloat("newTargY"));
        //        //Debug.Log(mouseInLeftBoxArea());

        //    }
        //    else if (leftMouseClicked() && mouseInRightBoxArea())
        //    {
        //        playerDetector.SetActive(false);
        //        PlayerPrefs.SetInt("isSelected", 1);
        //        Debug.Log("RightSelected");
        //        passOnInfo(2);
        //        Debug.Log(PlayerPrefs.GetFloat("newTargX"));
        //        Debug.Log(PlayerPrefs.GetFloat("newTargY"));

        //    }
        //    else if (leftMouseClicked() && mouseInTopBoxArea())
        //    {
        //        playerDetector.SetActive(true);
        //        PlayerPrefs.SetInt("isSelected", 1);
        //        Debug.Log("TopSelected = " + PlayerPrefs.GetInt("isSelected"));
        //        passOnInfo(3);
        //        Debug.Log(PlayerPrefs.GetFloat("newTargX"));
        //        Debug.Log(PlayerPrefs.GetFloat("newTargY"));
        //    }
        //}
    }

    void FixedUpdate()
    {
        if (contact)
        {
            moveBox();
            Debug.Log("Moving");
        }
    }

    //void OnMouseDown()
    //{
    //    if (leftMouseClicked())
    //    {
    //        PlayerPrefs.SetInt("isSelected", 1);
    //        playerDetector.SetActive(false);
    //        passOnInfo(4);

    //        //isSelected = true;
    //        Debug.Log("Selected");
    //        //this.gameObject.tag = "Untagged";
    //    }
    //}

    bool leftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
            return false;
    }

    //bool mouseOutsideInteractable()
    //{
    //    if ((CalculatedMousePos().x > this.transform.position.x + (this.transform.localScale.x / 2)) || (CalculatedMousePos().x < this.transform.position.x - (this.transform.localScale.x / 2)) ||
    //        (CalculatedMousePos().y > this.transform.position.y + (this.transform.localScale.y / 2)) || (CalculatedMousePos().y < this.transform.position.y - (this.transform.localScale.y / 2)))
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    public void checkForBoxInteraction()
    {
        if (mouseInLeftBoxArea())
        {
            playerDetector.SetActive(false);
            PlayerPrefs.SetInt("isSelected", 1);
            Debug.Log("LeftSelected");
            passOnInfo(1);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
            Debug.Log(PlayerPrefs.GetFloat("newTargX"));
            Debug.Log(PlayerPrefs.GetFloat("newTargY"));
            //Debug.Log(mouseInLeftBoxArea());

        }
        else if (mouseInRightBoxArea())
        {
            playerDetector.SetActive(false);
            PlayerPrefs.SetInt("isSelected", 1);
            Debug.Log("RightSelected");
            passOnInfo(2);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
            Debug.Log(PlayerPrefs.GetFloat("newTargX"));
            Debug.Log(PlayerPrefs.GetFloat("newTargY"));

        }
        else if (mouseInTopBoxArea())
        {
            playerDetector.SetActive(true);
            PlayerPrefs.SetInt("isSelected", 1);
            Debug.Log("TopSelected = " + PlayerPrefs.GetInt("isSelected"));
            passOnInfo(3);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
            Debug.Log(PlayerPrefs.GetFloat("newTargX"));
            Debug.Log(PlayerPrefs.GetFloat("newTargY"));
        }
        else if (mouseInBoxArea() && isMovable)
        {
            playerDetector.SetActive(false);
            PlayerPrefs.SetInt("isSelected", 1);
            Debug.Log("BoxSelected = " + PlayerPrefs.GetInt("isSelected"));
            canPush = true;
            passOnInfo(4);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
            Debug.Log(PlayerPrefs.GetFloat("newTargX"));
            Debug.Log(PlayerPrefs.GetFloat("newTargY"));
        }
    }

    Vector3 CalculatedMousePos()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

    void moveBox()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.MovePosition(rb.position + (Vector2.right * speed * Time.deltaTime));
        rbPlayer.MovePosition(rbPlayer.position + (Vector2.right * speed * Time.deltaTime));
    }

    void passOnInfo(int pos)
    {
        //PlayerPrefs.SetInt("moveToInteractable", 1);
        //LeftBox
        if (pos == 1)
        {
            PlayerPrefs.SetFloat("newTargX", this.transform.position.x - (this.transform.localScale.x));
            PlayerPrefs.SetFloat("newTargY", this.transform.position.y);
            PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
        }
        //RightBox
        else if (pos == 2)
        {
            PlayerPrefs.SetFloat("newTargX", this.transform.position.x + (this.transform.localScale.x));
            PlayerPrefs.SetFloat("newTargY", this.transform.position.y);
            PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
        }
        //TopBox
        else if (pos == 3)
        {
            PlayerPrefs.SetFloat("newTargX", this.transform.position.x);
            PlayerPrefs.SetFloat("newTargY", this.transform.position.y + (this.transform.localScale.y));
            PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
        }
        //Moveox 
        else if (pos == 4)
        {
            PlayerPrefs.SetFloat("newTargX", this.transform.position.x);
            PlayerPrefs.SetFloat("newTargY", this.transform.position.y);
            PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
        }
        //Debug.Log("Moving");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(canPush)
        {
            if (col.gameObject.tag == "Player")
            {
                contact = true;
                Debug.Log("contact =" + contact);
                rbPlayer = col.gameObject.GetComponent<Rigidbody2D>();
            }
            else if (col.gameObject.tag == "Obstacle")
            {
                contact = false;
                canPush = false;
                pathControllerScript.Scan();
            }
        }
    }

    bool mouseInLeftBoxArea()
    {
        if ((CalculatedMousePos().x > this.transform.position.x - (3*(this.transform.localScale.x / 2))) && (CalculatedMousePos().x < this.transform.position.x - (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > this.transform.position.y - (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (this.transform.localScale.y / 2)))
        {
            return true;
        }

        return false;
    }

    bool mouseInRightBoxArea()
    {
        if ((CalculatedMousePos().x < this.transform.position.x + (3*(this.transform.localScale.x / 2))) && (CalculatedMousePos().x > this.transform.position.x + (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > this.transform.position.y - (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (this.transform.localScale.y / 2)))
        {
            return true;
        }

        return false;
    }

    bool mouseInTopBoxArea()
    {
        if ((CalculatedMousePos().x > this.transform.position.x - (this.transform.localScale.x / 2)) && (CalculatedMousePos().x < this.transform.position.x + (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > this.transform.position.y + (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (3*(this.transform.localScale.y / 2))))
        {
            return true;
        }

        return false;
    }
    bool mouseInBoxArea()
    {
        if ((CalculatedMousePos().x > this.transform.position.x - (this.transform.localScale.x / 2)) && (CalculatedMousePos().x < this.transform.position.x + (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > this.transform.position.y - (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (this.transform.localScale.y / 2)))
        {
            return true;
        }

        return false;
    }
}
