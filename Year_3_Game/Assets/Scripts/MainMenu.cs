using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Mcamera;

    public void startGame()
    {
        Debug.Log("INtro sequence");
        Mcamera.GetComponent<Animator>().SetBool("Game Start", true);
    }

    public void quitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
