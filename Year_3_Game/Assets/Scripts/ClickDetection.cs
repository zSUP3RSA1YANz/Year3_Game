using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public GameObject player;
    public GameObject lightEffect;
    private CustomPathAI path;

    private bool playLoop = false;

    void Start()
    {
        path = player.GetComponent<CustomPathAI>();
        lightEffect.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(player.transform.position + " " + player.GetComponent<Rigidbody2D>().position);
        }
    }

    void OnMouseDown()
    {
        //Make Annie go to mouse
        PlayerPrefs.SetInt("isSelected", 0);
        path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));

        //Play particle affect when clicked
        lightEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lightEffect.SetActive(true);
        lightEffect.GetComponent<ParticleSystem>().Play();
    }
}
