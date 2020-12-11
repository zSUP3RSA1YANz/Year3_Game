using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerHolder;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("CharacterHeight", PlayerHolder.GetComponent<CircleCollider2D>().radius * 2);

        PlayerPrefs.SetInt("isSelected", 0);
        PlayerPrefs.SetInt("pathChangable", 0);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if(leftMouseClicked())
    //    {
    //        PlayerHolder.GetComponent<CustomPathAI>().setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
    //    }
    //}

    //bool leftMouseClicked()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        return true;
    //    }
    //    else
    //        return false;
    //}
}
