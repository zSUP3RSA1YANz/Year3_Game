using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlapperMangager : MonoBehaviour
{
    public GameObject flapper1;
    public GameObject flapper2;
    public GameObject flapper3;
    public GameObject flapper4;

    private Enemy_AI_Path flapper1Path;
    private Enemy_AI_Path flapper2Path;
    private Enemy_AI_Path flapper3Path;
    private Enemy_AI_Path flapper4Path;

    public bool targetingM;
    public bool targetingP;

    public GameObject player;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        //get script of all of this enemy type
        flapper1Path = flapper1.GetComponent<Enemy_AI_Path>();
        flapper2Path = flapper2.GetComponent<Enemy_AI_Path>();
        flapper3Path = flapper3.GetComponent<Enemy_AI_Path>();
        flapper4Path = flapper4.GetComponent<Enemy_AI_Path>();

        targetingM = false;
        targetingP = false;

        GM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startTargetingPlayer();
        }

        if (targetingM)
        {
            GM.interactableEffectLooped.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    //targets player
    public void startTargetingPlayer()
    {
        targetingM = false;
        targetingP = true;

        BroadcastMessage("targetPlayer");

        flapper1Path.targetingP = true;

        flapper2Path.targetingP = true;

        flapper3Path.targetingP = true;

        flapper4Path.targetingP = true;
    }

    //targets mouse
    public void startTargetingMouse()
    {
        targetingM = true;
        targetingP = false;
        BroadcastMessage("targetMouse");
    }

    //reset all enemy pos
    public void resetAllPos()
    {
        BroadcastMessage("reset");
    }

    //play particle effect
    public void playEffect()
    {
        GM.playInteractableEffectLooped();
    }

    //stop particle effect
    public void stopEffect()
    {
        if(GM.isPlayingInteractEffectLooped())
        GM.stopInteractableEffectLooped();
    }
}
