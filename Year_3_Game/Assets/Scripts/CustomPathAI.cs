using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomPathAI : MonoBehaviour
{
    private Vector3 targetPosition;

    public float speed = 200f;
    public float jumpForce = 200f;
    public float nextWaypointDistance = 3f;

    private bool forceMove;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            setTargetPosition();
        }

        if(rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void FixedUpdate()
    {
        if (forceMove)
        {
            move();
        }
    }

    void setTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
        seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        forceMove = true;
    }

    void move()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        //else
        //    isMoving = false;
    }

    //void UpdatePath()
    //{
    //    if(/*seeker.IsDone() &&*/ Input.GetMouseButton(0))
    //    {
    //        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        seeker.StartPath(rb.position, targetPosition, OnPathComplete);
    //    }
    //}

    void OnPathComplete(Path P)
    {
        if(!P.error)
        {
            path = P;
            currentWaypoint = 0;
        }
    }

    //void FixedUpdate()
    //{
    //    if (path == null)
    //        return;

    //    if(currentWaypoint >= path.vectorPath.Count)
    //    {
    //        reachedEndOfPath = true;
    //        return;
    //    }
    //    else
    //    {
    //        reachedEndOfPath = false;
    //    }

    //    //Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
    //    //Vector2 force = direction * speed * Time.deltaTime;

    //    //rb.AddForce(force);

    //    float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

    //    if(distance < nextWaypointDistance)
    //    {
    //        currentWaypoint++;
    //    }
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Obstacle")
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}
