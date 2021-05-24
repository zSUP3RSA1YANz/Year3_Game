using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateObject : MonoBehaviour
{
    public GameObject obj;
    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    //activate obj
    public void activate()
    {
        GM.activateObj(obj);
    }

    //deactivate obj
    public void deActivate()
    {
        GM.DeactivateObj(obj);
    }

    //activate obj on trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        activate();
    }
}
