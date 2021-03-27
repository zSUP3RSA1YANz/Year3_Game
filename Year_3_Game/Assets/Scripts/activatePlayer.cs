using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject backGround;
    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
