using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlapperMangager : MonoBehaviour
{
    public GameObject flapper1;
    public GameObject flapper2;
    public GameObject flapper3;
    public GameObject flapper4;

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startTargetingPlayer();
        }
    }

    public void startTargetingPlayer()
    {
        flapper1.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper2.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper3.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper4.GetComponent<Enemy_AI_Path>().targetingP = false;

        flapper1.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper2.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper3.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper4.GetComponent<Enemy_AI_Path>().targetingM = false;

        flapper1.GetComponent<Enemy_AI_Path>().targetPlayer();
        flapper2.GetComponent<Enemy_AI_Path>().targetPlayer();
        flapper3.GetComponent<Enemy_AI_Path>().targetPlayer();
        flapper4.GetComponent<Enemy_AI_Path>().targetPlayer();
    }

    public void startTargetingMouse()
    {
        flapper1.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper2.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper3.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper4.GetComponent<Enemy_AI_Path>().targetingP = false;

        flapper1.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper2.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper3.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper4.GetComponent<Enemy_AI_Path>().targetingM = false;

        flapper1.GetComponent<Enemy_AI_Path>().targetMouse();
        flapper2.GetComponent<Enemy_AI_Path>().targetMouse();
        flapper3.GetComponent<Enemy_AI_Path>().targetMouse();
        flapper4.GetComponent<Enemy_AI_Path>().targetMouse();
    }

    public void resetAllPos()
    {
        flapper1.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper2.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper3.GetComponent<Enemy_AI_Path>().targetingP = false;
        flapper4.GetComponent<Enemy_AI_Path>().targetingP = false;

        flapper1.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper2.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper3.GetComponent<Enemy_AI_Path>().targetingM = false;
        flapper4.GetComponent<Enemy_AI_Path>().targetingM = false;

        flapper1.GetComponent<Enemy_AI_Path>().reset();
        flapper2.GetComponent<Enemy_AI_Path>().reset();
        flapper3.GetComponent<Enemy_AI_Path>().reset();
        flapper4.GetComponent<Enemy_AI_Path>().reset();


        //Debug.Log("ResetAll");
    }
}
