using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cageFall : MonoBehaviour
{
    public GameObject player;
    private CustomPathAI path;

    public GameObject spike;
    private Rigidbody2D spikeRb;

    private GameManager GM;

    public Transform posMarker;

    private bool throwing = false;

    public Vector2 spikeThrowForce;

    void Start()
    {
        path = player.GetComponent<CustomPathAI>();
        GM = FindObjectOfType<GameManager>();
        spikeRb = spike.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //throws spike when Annie is airborne
        if(path.isAirborne && spike.GetComponent<interactableSpike>().carry)
        {
            spike.GetComponent<interactableSpike>().canCarry = false;
            throwing = true;
        }
        else if(throwing)
        {
            throwing = false;
        }
    }

    void FixedUpdate()
    {
        if(throwing)
            spikeThrow();
    }

    //moves Annie in positon to throw spike
    void OnMouseDown()
    {
        PlayerPrefs.SetInt("isSelected", 1);
        GM.playInteractableEffect();
        path.setTargetPosition(posMarker.position.x, posMarker.position.y, posMarker.position.z);
        player.GetComponent<CustomPathAI>().jumpFromRightBox = true;
    }

    //throws spike
    void spikeThrow()
    {
        spikeRb.AddForce((Vector2.up + Vector2.left) * spikeThrowForce);
        spike.GetComponent<PolygonCollider2D>().isTrigger = false;
    }
}
