using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    private GameManager GM;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        GM = FindObjectOfType<GameManager>();

        target = new Vector2(player.position.x, player.position.y);

    }

void Update()
    {
        //moves bullet towards target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        //Destroys bullet when reaches target
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            destroyBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Destroys bullet when hits target
        if (col.CompareTag("Player"))
        {
            Debug.Log("PlayerHit");
            GM.teleportPlayerBoss1();
            destroyBullet();
        }
        //Destroys bullet when hits wall
        else if (col.CompareTag("Obstacle"))
        {
            destroyBullet();
        }
    }

    //Destroys bullet 
    void destroyBullet()
    {
        Destroy(gameObject);
    }
}
