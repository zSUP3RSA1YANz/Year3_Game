using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpDetector : MonoBehaviour
{
    CustomPathAI path;
    private GameObject player;

    public GameObject jumpPosMark;

    public float jumpPrepTime;

    private GameManager GM;

    public GameObject clickDetector;

    private AstarPath pathController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GM = FindObjectOfType<GameManager>();
        path = player.GetComponent<CustomPathAI>();
        pathController = GameObject.Find("A*").GetComponent<AstarPath>();
    }

    //detects mouse
    void OnMouseDown()
    {
        PlayerPrefs.SetInt("isSelected", 1);
        GM.playInteractableEffect();
        passOnInfo();
        path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        Debug.Log("Jump Detector selected");
    }

    //activates next jump section
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            clickDetector.SetActive(true);
            pathController.Scan();
        }
    }
    
    //resets jump sections
    public void reset()
    {
        Debug.Log("Jumps reset");
        this.GetComponent<BoxCollider2D>().enabled = true;
        clickDetector.SetActive(false);
        pathController.Scan();
    }

    //carries info of when jump point is
    void passOnInfo()
    {
        PlayerPrefs.SetFloat("newTargX", jumpPosMark.transform.position.x);
        PlayerPrefs.SetFloat("newTargY", jumpPosMark.transform.position.y);
        PlayerPrefs.SetFloat("newTargZ", jumpPosMark.transform.position.z);

        path.longJumpFromLeft = true;
    }
}
