using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallBoxInteract : MonoBehaviour
{
    public GameObject topPosMark1;
    public GameObject topPosMark2;
    public GameObject leftJPosMark;

    //public GameObject rightPosMark;
    //public GameObject rightJPosMark;

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

    void checkForBoxInteraction()
    {
        //if (mouseInLeftBoxArea())
        //{
        //    playerDetector.SetActive(false);
        //    PlayerPrefs.SetInt("isSelected", 1);
        //    Debug.Log("LeftSelected");
        //    passOnInfo(1);
        //    path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        //    //Debug.Log(PlayerPrefs.GetFloat("newTargX"));
        //    //Debug.Log(PlayerPrefs.GetFloat("newTargY"));

        //}
        //else if (mouseInRightBoxArea())
        //{
        //    playerDetector.SetActive(false);
        //    PlayerPrefs.SetInt("isSelected", 1);
        //    Debug.Log("RightSelected");
        //    passOnInfo(2);
        //    path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        //    //Debug.Log(PlayerPrefs.GetFloat("newTargX"));
        //    //Debug.Log(PlayerPrefs.GetFloat("newTargY"));

        //}
        if (mouseInTopBoxArea() && playerIsOnLeftBox())
        {
            //playerDetector.SetActive(true);
            PlayerPrefs.SetInt("isSelected", 1);
            GM.playInteractableEffect();
            //Debug.Log("TopSelected = " + PlayerPrefs.GetInt("isSelected"));
            Debug.Log(PlayerPrefs.GetInt("isSelected"));
            passOnInfo(3);
            path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
            //path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
            //Debug.Log(PlayerPrefs.GetFloat("newTargX"));
            //Debug.Log(PlayerPrefs.GetFloat("newTargY"));
            Debug.Log("YAAY");
            Debug.Log(PlayerPrefs.GetInt("isSelected"));
        }
    }

    //bool mouseInLeftBoxArea()
    //{
    //    if ((CalculatedMousePos().x > this.transform.position.x - (3 * (this.transform.localScale.x / 2))) && (CalculatedMousePos().x < this.transform.position.x - (this.transform.localScale.x / 2)) &&
    //        (CalculatedMousePos().y > this.transform.position.y - (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (this.transform.localScale.y / 2)))
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    //bool mouseInRightBoxArea()
    //{
    //    if ((CalculatedMousePos().x < this.transform.position.x + (3 * (this.transform.localScale.x / 2))) && (CalculatedMousePos().x > this.transform.position.x + (this.transform.localScale.x / 2)) &&
    //        (CalculatedMousePos().y > this.transform.position.y - (this.transform.localScale.y / 2)) && (CalculatedMousePos().y < this.transform.position.y + (this.transform.localScale.y / 2)))
    //    {
    //        return true;
    //    }

    //    return false;
    //}

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
            //Debug.Log((player.transform.position.x + (player.transform.localScale.x / 2)));
            //Debug.Log((this.transform.position.x - (this.transform.localScale.x / 2)));
            return true;
        }
        else
        {
            return false;
        }
    }

    void passOnInfo(int pos)
    {
    //    //LeftBox
    //    if (pos == 1)
    //    {
    //        if (playerIsLeftOfBox())
    //        {
    //            PlayerPrefs.SetFloat("newTargX", leftPosMark.transform.position.x);
    //            PlayerPrefs.SetFloat("newTargY", leftPosMark.transform.position.y);
    //            PlayerPrefs.SetFloat("newTargZ", leftPosMark.transform.position.z);
    //        }
    //        else if (playerIsRightOfBox())
    //        {
    //            PlayerPrefs.SetFloat("newTargX", rightJPosMark.transform.position.x);
    //            PlayerPrefs.SetFloat("newTargY", rightJPosMark.transform.position.y);
    //            PlayerPrefs.SetFloat("newTargZ", rightJPosMark.transform.position.z);

    //            player.GetComponent<CustomPathAI>().jumpFromRightBox = true;
    //        }
    //        else
    //        {
    //            PlayerPrefs.SetFloat("newTargX", leftJPosMark.transform.position.x);
    //            PlayerPrefs.SetFloat("newTargY", leftJPosMark.transform.position.y);
    //            PlayerPrefs.SetFloat("newTargZ", leftJPosMark.transform.position.z);
    //            Debug.Log("Player on top");
    //        }
    //    }
    //    //RightBox
    //    else if (pos == 2)
    //    {
    //        if (playerIsRightOfBox())
    //        {
    //            PlayerPrefs.SetFloat("newTargX", rightPosMark.transform.position.x);
    //            PlayerPrefs.SetFloat("newTargY", rightPosMark.transform.position.y);
    //            PlayerPrefs.SetFloat("newTargZ", rightPosMark.transform.position.z);
    //        }
    //        else if (playerIsLeftOfBox())
    //        {
    //            PlayerPrefs.SetFloat("newTargX", leftJPosMark.transform.position.x);
    //            PlayerPrefs.SetFloat("newTargY", leftJPosMark.transform.position.y);
    //            PlayerPrefs.SetFloat("newTargZ", leftJPosMark.transform.position.z);

    //            player.GetComponent<CustomPathAI>().jumpFromLeftBox = true;
    //        }
    //        else
    //        {
    //            PlayerPrefs.SetFloat("newTargX", rightJPosMark.transform.position.x);
    //            PlayerPrefs.SetFloat("newTargY", rightJPosMark.transform.position.y);
    //            PlayerPrefs.SetFloat("newTargZ", rightJPosMark.transform.position.z);
    //            Debug.Log("Player on top");
    //        }
    //    }
    //    //TopBox
        if (pos == 3)
        {
            ////player is left of box
            //if (playerIsLeftOfBox())
            //{
            PlayerPrefs.SetFloat("newTargX", leftJPosMark.transform.position.x);
            PlayerPrefs.SetFloat("newTargY", leftJPosMark.transform.position.y);
            PlayerPrefs.SetFloat("newTargZ", leftJPosMark.transform.position.z);

            Debug.Log(leftJPosMark.transform.position.x);
            player.GetComponent<CustomPathAI>().jumpFromLeftBox = true;
            //}
            //else if (playerIsRightOfBox())
            //{
            //    PlayerPrefs.SetFloat("newTargX", rightJPosMark.transform.position.x);
            //    PlayerPrefs.SetFloat("newTargY", rightJPosMark.transform.position.y);
            //    PlayerPrefs.SetFloat("newTargZ", rightJPosMark.transform.position.z);

            //    player.GetComponent<CustomPathAI>().jumpFromRightBox = true;
            //}
        }
    //    //Movebox 
    //    else if (pos == 4)
    //    {
    //        PlayerPrefs.SetFloat("newTargX", this.transform.position.x);
    //        PlayerPrefs.SetFloat("newTargY", this.transform.position.y);
    //        PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
    //    }
    }
}
