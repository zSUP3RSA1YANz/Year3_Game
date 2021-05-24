using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventController : MonoBehaviour
{
    public Animator anim;
    public AudioSource source;
    public GameObject player;

    void Start()
    {
        //Allows the public classes to be optional
        if (anim == null)
        {
            anim = null;
        }

        if (source == null)
        {
            source = null;
        }

        if (player == null)
        {
            player = null;
        }
    }

    //plays animation based on string name
    public void animPlay(string myCondtion)
    {
        anim.GetComponent<Animator>().SetBool(myCondtion, true);
    }

    //stops animation based on string name
    public void animStop(string myCondtion)
    {
        anim.GetComponent<Animator>().SetBool(myCondtion, false);
    }

    //changes camera
    public void switchC(GameObject oldC, GameObject newC)
    {
        oldC.SetActive(false);
        newC.SetActive(true);
    }

    //activates specific object
    public void activateGameObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    //Deactivates specific object
    public void deactivateGameObject()
    {
        this.gameObject.SetActive(false);
    }

    //Makes cursor visible on screen
    public void enableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //Disables cursor on screen
    public void disableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //plays audio clip in Audiosource
    public void playSound()
    {
        source.Play();
    }

    //stops Annie's abiltiy to move
    public void stopMovement()
    {
        player.GetComponent<CustomPathAI>().enabled = false;
    }

    //gives Annie abiltiy to move
    public void startMovement()
    {
        player.GetComponent<CustomPathAI>().enabled = true;
    }
}
