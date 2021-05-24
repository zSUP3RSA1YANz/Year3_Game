using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject backGround;
    public GameManager GM;

    //Allows Annie to move freely
    public void playerActivate()
    {
        player.GetComponent<Animator>().SetBool("CanWalk", true);
        player.GetComponent<CustomPathAI>().enabled = true;
        backGround.GetComponent<BoxCollider2D>().enabled = true;
        backGround.GetComponent<ClickDetection>().enabled = true;
        GM.enableCursor();
        this.gameObject.SetActive(false);
    }
}
