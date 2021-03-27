using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomPathAI : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 newTargetPos;

    public float speed = 200f;
    public float jumpVForce = 200f;
    public float jumpHForce = 200f;

    public float longJumpForce = 400f;
    public float nextWaypointDistance = 3f;

    public float jumpPrepTime = 1f;
    public float longJumpPrepTime = 2f;

    public bool forceMove;
    public bool isGrounded = false;
    public bool isAirborne = false;

    private int steps;

    public Path path;
    int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    public bool jumpFromLeftBox = false;
    public bool jumpFromRightBox = false;
    public bool longJumpFromLeft = false;


    private Vector2 jumpDirection;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (leftMouseClicked())
        //{
        //    for(var i = 0; i < boxes.Length; i++)
        //    {
        //        boxes[i].GetComponent<BoxInteract>().checkForBoxInteraction();
        //    }

        //    setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        //}

        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (((jumpFromLeftBox || jumpFromRightBox)  && reachedEndOfPath && !isAirborne)/* || Input.GetKeyDown(KeyCode.Space)*/)
        {
            StartCoroutine("waitShortJump");
            //forceMove = false;
            Debug.Log("HIIII again");
            Debug.Log(reachedEndOfPath);
        }
        else if (longJumpFromLeft && reachedEndOfPath && !isAirborne || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("waitLongJump");
            Debug.Log("HIIII");
            Debug.Log(reachedEndOfPath);
        }
    }

    void FixedUpdate()
    {
        if (forceMove)
        {
            move();
            //Debug.Log("Moves =" + steps);
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

    public void setTargetPosition(float x, float y, float z)
    {
        if(PlayerPrefs.GetInt("isSelected") == 0)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (PlayerPrefs.GetInt("isSelected") == 1)
        {
            targetPosition = new Vector3(x, y, z); ;
        }

        //Debug.Log("AI script selection status = " + PlayerPrefs.GetInt("isSelected"));

        targetPosition.z = transform.position.z;
        seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        forceMove = true;

       // Debug.Log(PlayerPrefs.GetInt("isSelected"));
    }

    //public void setCustomTargetPosition()
    //{
    //    newTargetPos = new Vector3 (PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
    //    newTargetPos.z = transform.position.z;
    //    seeker.StartPath(rb.position, newTargetPos, OnPathComplete);
    //    forceMove = true;
    //}

    void move()
    {
        if (path == null)
        {
            return;
        }

        //Debug.Log("path size =" + " " + path.vectorPath.Count);
        //Debug.Log("Current way point =" + " " + currentWaypoint);
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            forceMove = false;

            path = null;

            PlayerPrefs.SetInt("isSelected", 0);

            currentWaypoint = 0;
            Debug.Log("path ended =" + " " + reachedEndOfPath);
        //    Debug.Log("Selection =" + " " + PlayerPrefs.GetInt("isSelected"));
            //if(jumpFromLeftBox && reachedEndOfPath)
            //{
            //    StartCoroutine("wait");
            //    //jumpRight();
            //    //jumpFromLeftBox = false;
            //}
            //Debug.Log("moving =" + " " + forceMove);
            return;
        }
        else
        {
            reachedEndOfPath = false;
            Debug.Log("path size =" + " " + path.vectorPath.Count);
            Debug.Log("Current way point =" + " " + currentWaypoint);
        }



        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        jumpDirection = force;

        rb.AddForce(force);
        //rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //rb.MovePosition(rb.position + (direction * speed * Time.deltaTime));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        Debug.Log("entries");
    }

    void OnPathComplete(Path P)
    {
        if(!P.error)
        {
            path = P;
            currentWaypoint = 0;
        }
    }

    public void jumpRight()
    {
        forceMove = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        //rb.AddForce((Vector2.up * jumpForce) + jumpDirection);
        rb.AddForce(((Vector2.up * jumpVForce) + (Vector2.right * jumpHForce)));
        isAirborne = true;
        isGrounded = false;
        //Debug.Log("Jump");
        //forceMove = true;
        //rb.MovePosition(rb.position + ((Vector2.up + Vector2.right) * speed * Time.deltaTime));
        jumpFromLeftBox = false;
        Debug.Log("right jump");
    }
    public void jumpLeft()
    {
        forceMove = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(((Vector2.up * jumpVForce) + (Vector2.left * jumpHForce)));
        isAirborne = true;
        isGrounded = false;
        jumpFromRightBox = false;
        Debug.Log("left jump");
    }

    public void longJumpRight()
    {
        forceMove = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce((Vector2.up + Vector2.right) * longJumpForce);
        isAirborne = true;
        isGrounded = false;
        longJumpFromLeft = false;
        Debug.Log("LongJump");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.tag == "Obstacle")
        //{
        //    jumpRight();
        //}
        //else if (col.tag == "Gap")
        //{
        //    //longJump();
        //}
    }

    public void respawnPos(float x, float y)
    {
        this.transform.position = new Vector2(x, y);
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        forceMove = false;
    }

    public void respawnPosMark(GameObject posMark)
    {
        this.transform.position = posMark.transform.position;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        forceMove = false;
    }

    IEnumerator waitShortJump()
    {
        yield return new WaitForSeconds(jumpPrepTime);
        if (jumpFromLeftBox && !isAirborne && reachedEndOfPath)
            jumpRight();
        else if (jumpFromRightBox && !isAirborne && reachedEndOfPath)
            jumpLeft();
        //jumpFromLeftBox = false;
    }

    IEnumerator waitLongJump()
    {
        yield return new WaitForSeconds(longJumpPrepTime);
        if(longJumpFromLeft && !isAirborne && reachedEndOfPath)
        longJumpRight();
    }
}
