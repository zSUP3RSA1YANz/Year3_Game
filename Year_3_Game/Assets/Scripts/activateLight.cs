using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateLight : MonoBehaviour
{
    public GameObject lightBall;

    // Start is called before the first frame update
    void Start()
    {
        lightBall.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
