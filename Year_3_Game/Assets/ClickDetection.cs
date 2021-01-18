using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public GameObject player;
    public GameObject lightEffect;
    private CustomPathAI path;

    // Start is called before the first frame update
    void Start()
    {
        path = player.GetComponent<CustomPathAI>();
        lightEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("Selection =" + " " + PlayerPrefs.GetInt("isSelected"));
        //}
    }

    void OnMouseDown()
    {
        PlayerPrefs.SetInt("isSelected", 0);
        path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        //Debug.Log("Selection due to background =" + " " + PlayerPrefs.GetInt("isSelected"));
        lightEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lightEffect.SetActive(true);
        lightEffect.GetComponent<ParticleSystem>().Play();

    }
}
