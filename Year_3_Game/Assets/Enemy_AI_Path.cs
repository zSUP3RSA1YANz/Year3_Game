using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI_Path : MonoBehaviour
{
    private Vector3 targetPosition;

    Seeker seeker;
    public Path path;
    Rigidbody2D rb;

    int currentWaypoint = 0;

    public bool forceMove;

    bool reachedEndOfPath = false;

    public float nextWaypointDistance = 3f;

    public float speed = 200f;

    public GameObject player;

    public GameObject respawnMarker;

    Vector3 originPos;

    Vector3 targetLocation;


    public bool targetingP = false;
    public bool targetingM = false;
    bool contact = false;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        originPos = this.transform.position;
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (forceMove)
        {
            move();
        }
    }

    public void targetPlayer()
    {
        targetingP = true;
        targetingM = false;
        setTargetPosition(player.transform.position);
        InvokeRepeating("UpdatePathPlayer", 0f, .5f);
    }

    public void targetMouse()
    {
        targetingP = false;
        targetingM = true;
        setTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        InvokeRepeating("UpdatePathMouse", 0f, .5f);
    }

    public void setTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
        targetPosition.z = transform.position.z;
        seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        forceMove = true;

        // Debug.Log(PlayerPrefs.GetInt("isSelected"));
    }

    void UpdatePathPlayer()
    {
        if(seeker.IsDone() && targetingP)
        {
            setTargetPosition(player.transform.position);
            Debug.Log("Repeating");
        }
    }

    void UpdatePathMouse()
    {
        if(seeker.IsDone() && targetingM)
        {
            setTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("Repeating");
        }
    }

    void move()
    {
        if (path == null)
            return;

        //Debug.Log("path size =" + " " + path.vectorPath.Count);
        //Debug.Log("Current way point =" + " " + currentWaypoint);
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            forceMove = false;

            PlayerPrefs.SetInt("isSelected", 0);

            currentWaypoint = 0;
            //Debug.Log("path ended =" + " " + reachedEndOfPath);
            //    Debug.Log("Selection =" + " " + PlayerPrefs.GetInt("isSelected"));
            //Debug.Log("moving =" + " " + forceMove);
            return;
        }
        else
        {
            reachedEndOfPath = false;
            //Debug.Log("path size =" + " " + path.vectorPath.Count);
            //Debug.Log("Current way point =" + " " + currentWaypoint);
        }


        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

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
        if (!P.error)
        {
            path = P;
            currentWaypoint = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            contact = true;
            targetingP = false;
            player.GetComponent<CustomPathAI>().respawn(respawnMarker.transform.position.x, respawnMarker.transform.position.y);
            resetCall();
            Debug.Log("grabbed");
        }
    }

    void resetCall()
    {
        this.GetComponentInParent<EnemyFlapperMangager>().resetAllPos();
    }

    public void reset()
    {
        setTargetPosition(originPos);
        Debug.Log("Reset");
    }

    void OnMouseDrag()
    {
        targetingP = false;
        GetComponentInParent<EnemyFlapperMangager>().startTargetingMouse();
        Debug.Log("OnMouse");
    }

    void OnMouseUp()
    {
        if(targetingM)
        {
            GetComponentInParent<EnemyFlapperMangager>().startTargetingPlayer();
        }
    }

}
