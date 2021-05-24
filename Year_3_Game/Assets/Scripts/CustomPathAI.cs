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

    AudioSource source;
    public AudioClip clip;

    [Range(0.01f,1f)]public float footstepSpeed;

    private Vector2 jumpDirection;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Makes Annie Look where she is going
        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        //plays footsteps
        if(isGrounded && Mathf.Abs(rb.velocity.x) >= 1f && !source.isPlaying)
        {
            footstep();
        }

        //starts jump
        if (((jumpFromLeftBox || jumpFromRightBox)  && reachedEndOfPath && !isAirborne))
        {
            StartCoroutine("waitShortJump");
            Debug.Log("HIIII again");
            Debug.Log(reachedEndOfPath);
        }
        else if (longJumpFromLeft && reachedEndOfPath && !isAirborne)
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

    //decides whether to go to mouse or interactable
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

        targetPosition.z = transform.position.z;
        seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        forceMove = true;

    }

    void move()
    {
        //checks if there is a path
        if (path == null)
        {
            return;
        }

        //checks if Annie has reached destination
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            forceMove = false;

            path = null;

            PlayerPrefs.SetInt("isSelected", 0);

            currentWaypoint = 0;
            Debug.Log("path ended =" + " " + reachedEndOfPath);
            return;
        }
        else
        {
            reachedEndOfPath = false;
            Debug.Log("path size =" + " " + path.vectorPath.Count);
            Debug.Log("Current way point =" + " " + currentWaypoint);
        }


        //calcuates where to go 
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        jumpDirection = force;

        //moves Annie
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        Debug.Log("entries");
    }

    //randomises footstep sounds
    void footstep()
    {
        source.volume = Random.Range(0.8f, 1f);
        source.pitch = Random.Range(0.8f, 1.1f);
        source.Play();
    }

    void OnPathComplete(Path P)
    {
        if(!P.error)
        {
            path = P;
            currentWaypoint = 0;
        }
    }

    //Annie jumps right
    public void jumpRight()
    {
        forceMove = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(((Vector2.up * jumpVForce) + (Vector2.right * jumpHForce)));
        isAirborne = true;
        isGrounded = false;
        jumpFromLeftBox = false;
        Debug.Log("right jump");
    }

    //Annie jumps left
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

    //Annie long jumps right
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

    //Annie respawns custom position
    public void respawnPos(float x, float y)
    {
        this.transform.position = new Vector2(x, y);
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        forceMove = false;
    }

    //Annie respawns marker position
    public void respawnPosMark(GameObject posMark)
    {
        this.transform.position = posMark.transform.position;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        forceMove = false;
    }

    //defines Annie prep time for small jump
    IEnumerator waitShortJump()
    {
        yield return new WaitForSeconds(jumpPrepTime);
        if (jumpFromLeftBox && !isAirborne && reachedEndOfPath)
            jumpRight();
        else if (jumpFromRightBox && !isAirborne && reachedEndOfPath)
            jumpLeft();
    }

    //defines Annie prep time for big jump
    IEnumerator waitLongJump()
    {
        yield return new WaitForSeconds(longJumpPrepTime);
        if(longJumpFromLeft && !isAirborne && reachedEndOfPath)
        longJumpRight();
    }
}
