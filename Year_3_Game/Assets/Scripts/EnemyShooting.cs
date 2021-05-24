using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;

    public bool inBossZone;

    public float startTimeBtwShots;
    private float timeBtwShots;

    public GameObject respawnMarker;

    private GameManager GM;

    [SerializeField] private bool targetP = true;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    void Update()
    {   
        //enemy attacks player while they are in zone
        if(inBossZone && targetP)
        {
            shootPlayer();
        }
    }

    void shootPlayer()
    {
        //frequency of attacks
        if(timeBtwShots <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    //player clears zone
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            targetP = false;
        }
    }

    void OnMouseDown()
    {
        //enemy distacted by mouse
        if(Input.GetMouseButtonDown(0))
        {
            GM.playInteractableEffectLooped();
            Debug.Log("mousepress");
            blind();
        }
    }

    //enemy resumes attack
    void OnMouseUp()
    {
        GM.stopInteractableEffectLooped();
        targetP = true;
    }

    //enemy stops firing
    void blind()
    {
        timeBtwShots = startTimeBtwShots;
        targetP = false; 
    }
}
