using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomPathAI : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 newTargetPos;

    public float speed = 200f;
    public float jumpForce = 200f;
    public float longJumpForce = 400f;
    public float nextWaypointDistance = 3f;

    public bool forceMove;

    private int steps;

    public Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    private Vector2 jumpDirection;

    Seeker seeker;
    Rigidbody2D rb;

    private List<GameObject> Boxes = new List<GameObject>();

    GameObject[] boxes; 

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        boxes  = GameObject.FindGameObjectsWithTag("Obstacle");
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

        if (Input.GetMouseButtonDown(1))
        {
            longJumpRight();
        }
    }

    void FixedUpdate()
    {
        if (forceMove)
        {
            move();
            Debug.Log("Moves =" + steps);
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
            return;

        Debug.Log("path size =" + " " + path.vectorPath.Count);
        Debug.Log("Current way point =" + " " + currentWaypoint);
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            forceMove = false;

            PlayerPrefs.SetInt("isSelected", 0);

            currentWaypoint = 0;
        Debug.Log("path ended =" + " " + reachedEndOfPath);
        //    Debug.Log("Selection =" + " " + PlayerPrefs.GetInt("isSelected"));
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
    }

    void OnPathComplete(Path P)
    {
        if(!P.error)
        {
            path = P;
            currentWaypoint = 0;
        }
    }

    void jump()
    {
        forceMove = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce((Vector2.up * jumpForce) + jumpDirection);
        Debug.Log("Jump");
        forceMove = true;
        //rb.MovePosition(rb.position + ((Vector2.up + Vector2.right) * speed * Time.deltaTime));
    }

    public void longJumpRight()
    {
        forceMove = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce((Vector2.up + Vector2.right) * longJumpForce);
        Debug.Log("LongJump");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Obstacle")
        {
            jump();
        }
        else if (col.tag == "Gap")
        {
            //longJump();
        }
    }

    public void respawn(float x, float y)
    {
        this.transform.position = new Vector2(x, y);
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        forceMove = false;
    }
}
