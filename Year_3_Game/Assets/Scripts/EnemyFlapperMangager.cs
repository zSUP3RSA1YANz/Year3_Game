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

    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        flapper1Path = flapper1.GetComponent<Enemy_AI_Path>();
        flapper2Path = flapper2.GetComponent<Enemy_AI_Path>();
        flapper3Path = flapper3.GetComponent<Enemy_AI_Path>();
        flapper4Path = flapper4.GetComponent<Enemy_AI_Path>();

        targetingM = false;
        targetingP = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    startTargetingPlayer();
        //}

        //if(targetingM)
        //{
        //    startTargetingMouse();
        //}
    }

    public void startTargetingPlayer()
    {
        flapper1Path.targetPlayer();
        flapper1Path.targetingP = true;

        flapper2Path.targetPlayer();
        flapper2Path.targetingP = true;

        flapper3Path.targetPlayer();
        flapper3Path.targetingP = true;

        flapper4Path.targetPlayer();
        flapper4Path.targetingP = true;
    }

    public void startTargetingMouse()
    {
        flapper1Path.targetMouse();
        flapper2Path.targetMouse();
        flapper3Path.targetMouse();
        flapper4Path.targetMouse();
    }

    public void resetAllPos()
    {
        flapper1Path.reset();
        flapper2Path.reset();
        flapper3Path.reset();
        flapper4Path.reset();
    }

    public void playEffect()
    {
        GM.playInteractableEffectLooped();
    }

    public void stopEffect()
    {
        if(GM.isPlayingInteractEffectLooped())
        GM.stopInteractableEffectLooped();
    }
}
