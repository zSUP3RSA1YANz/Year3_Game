using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateLight : MonoBehaviour
{
    public GameObject lightBall;

    // Starts the light Animation
    void Start()
    {
        lightBall.SetActive(true);
    }
}
