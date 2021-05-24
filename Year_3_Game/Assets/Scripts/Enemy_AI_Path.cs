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

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (forceMove)
        {
            //moves enemy
            move();
        }
    }

    //Enemy targets player
    public void targetPlayer()
    {
        setTargetPosition(player.transform.position);
        InvokeRepeating("UpdatePathPlayer", 0f, .5f);
    }

    //Enemy targets mouse
    public void targetMouse()
    {
        targetingP = false;
        targetingM = true;
        setTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        InvokeRepeating("UpdatePathMouse", 0f, .5f);
        Debug.Log("setting target pos");
    }

    //Defines Enemy target
    public void setTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
        targetPosition.z = transform.position.z;
        seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        forceMove = true;
    }

    //When to calcualte next route when target player
    void UpdatePathPlayer()
    {
        if(seeker.IsDone() && targetingP)
        {
            setTargetPosition(player.transform.position);
            Debug.Log("Repeating Player");
        }
    }

    //When to calcualte next route when target playe
    void UpdatePathMouse()
    {
        if(seeker.IsDone() && targetingM)
        {
            setTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    //moves enemy
    void move()
    {
        //checks if path
        if (path == null)
            return;

        //checks if completed path
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


        //calculates direction
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //moves enemy
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

    //kills Annie, resets Enemy
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

    //call to reset all Enememies
    void resetCall()
    {
        this.GetComponentInParent<EnemyFlapperMangager>().resetAllPos();
    }

    //goes to orinal pos
    public void reset()
    {
        targetingP = false;
        setTargetPosition(originPos);
        Debug.Log("Reset");
    }

    //distracts enemy with mouse
    void OnMouseDrag()
    {
        if(targetingM)
        return;
        Debug.Log("mouse Dragged");
        GetComponentInParent<EnemyFlapperMangager>().startTargetingMouse();
    }

    //enemy resumes attack when mouse released
    void OnMouseUp()
    {
        GetComponentInParent<EnemyFlapperMangager>().stopEffect();
        {
            targetingM = false;
            GetComponentInParent<EnemyFlapperMangager>().targetingM = false;
            GetComponentInParent<EnemyFlapperMangager>().startTargetingPlayer();
        }
    }

    //plays particale effect
    void OnMouseDown()
    {
        GetComponentInParent<EnemyFlapperMangager>().playEffect();
    }

}
