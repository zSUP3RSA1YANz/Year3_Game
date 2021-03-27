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
        //targetingP = true;
        //targetingM = false;
        setTargetPosition(player.transform.position);
        InvokeRepeating("UpdatePathPlayer", 0f, .5f);
    }

    public void targetMouse()
    {
        targetingP = false;
        targetingM = true;
        setTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        InvokeRepeating("UpdatePathMouse", 0f, .5f);
        Debug.Log("setting target pos");
    }

    public void setTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
        targetPosition.z = transform.position.z;
        seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        forceMove = true;
    }

    void UpdatePathPlayer()
    {
        if(seeker.IsDone() && targetingP)
        {
            setTargetPosition(player.transform.position);
            Debug.Log("Repeating Player");
        }
    }

    void UpdatePathMouse()
    {
        if(seeker.IsDone() && targetingM)
        {
            setTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void move()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            forceMove = false;

            PlayerPrefs.SetInt("isSelected", 0);

            currentWaypoint = 0;
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
            player.GetComponent<CustomPathAI>().respawnPos(respawnMarker.transform.position.x, respawnMarker.transform.position.y);
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
        targetingP = false;
        setTargetPosition(originPos);
        Debug.Log("Reset");
    }

    void OnMouseDrag()
    {
        if(targetingM)
        return;
        Debug.Log("mouse Dragged");
        GetComponentInParent<EnemyFlapperMangager>().startTargetingMouse();
    }

    void OnMouseUp()
    {
        GetComponentInParent<EnemyFlapperMangager>().stopEffect();
        //if(GetComponentInParent<EnemyFlapperMangager>().targetingM)
        {
            targetingM = false;
            GetComponentInParent<EnemyFlapperMangager>().targetingM = false;
            GetComponentInParent<EnemyFlapperMangager>().startTargetingPlayer();
        }
    }

    void OnMouseDown()
    {
        GetComponentInParent<EnemyFlapperMangager>().playEffect();
    }

}
