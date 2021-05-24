using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cageHitGround : MonoBehaviour
{
    private GameManager GM;

    public GameObject cageCrash;
    public GameObject character;

    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    //activates objects for cutscene
    public void activateCageCrash()
    {
        GM.activateObj(cageCrash);
        GM.activateObj(character);
    }
}
