using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallBoxInteract : MonoBehaviour
{
    public GameObject topPosMark1;
    public GameObject topPosMark2;
    public GameObject leftJPosMark;


    public GameObject player;
    private CustomPathAI path;

    public GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        path = player.GetComponent<CustomPathAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftMouseClicked())
        {
            checkForBoxInteraction();
        }
    }

    Vector3 CalculatedMousePos()
    {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

    bool leftMouseClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        else
            return false;
    }

    //check where the mouse is pressed
    void checkForBoxInteraction()
    {
        if (mouseInTopBoxArea() && playerIsOnLeftBox())
        {
            PlayerPrefs.SetInt("isSelected", 1);
            GM.playInteractableEffect();
            Debug.Log(PlayerPrefs.GetInt("isSelected"));
            passOnInfo(3);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
            Debug.Log("YAAY");
            Debug.Log(PlayerPrefs.GetInt("isSelected"));
        }
    }

    bool mouseInTopBoxArea()
    {
        if ((CalculatedMousePos().x > topPosMark2.transform.position.x - (this.transform.localScale.x / 2)) && (CalculatedMousePos().x < topPosMark2.transform.position.x + (this.transform.localScale.x / 2)) &&
            (CalculatedMousePos().y > (topPosMark2.transform.position.y - (this.transform.localScale.y / 4))) && (CalculatedMousePos().y < topPosMark2.transform.position.y + (this.transform.localScale.y / 4)))
        {
            return true;
        }

        return false;
    }

    bool playerIsOnLeftBox()
    {
        if ((player.transform.position.x + Mathf.Abs(player.transform.localScale.x / 2)) > (topPosMark1.transform.position.x - (this.transform.localScale.x / 2)))
        {
            Debug.Log("Player is on left box");
            return true;
        }
        else
        {
            return false;
        }
    }

    void passOnInfo(int pos)
    {
   
        //TopBox
        if (pos == 3)
        {
            ////player is left of box
            PlayerPrefs.SetFloat("newTargX", leftJPosMark.transform.position.x);
            PlayerPrefs.SetFloat("newTargY", leftJPosMark.transform.position.y);
            PlayerPrefs.SetFloat("newTargZ", leftJPosMark.transform.position.z);

            Debug.Log(leftJPosMark.transform.position.x);
            player.GetComponent<CustomPathAI>().jumpFromLeftBox = true;
        }
    }
}
