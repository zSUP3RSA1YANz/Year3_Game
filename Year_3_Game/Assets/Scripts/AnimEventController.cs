using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventController : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        if (anim == null)
        {
            anim = null;
        }
    }

    public void animPlay(string myCondtion)
    {
        anim.GetComponent<Animator>().SetBool(myCondtion, true);
    }

    public void animStop(string myCondtion)
    {
        anim.GetComponent<Animator>().SetBool(myCondtion, false);
    }

    public void switchC(GameObject oldC, GameObject newC)
    {
        oldC.SetActive(false);
        newC.SetActive(true);
    }

    public void activateGameObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void deactivateGameObject()
    {
        this.gameObject.SetActive(false);
    }

    public void enableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void disableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
