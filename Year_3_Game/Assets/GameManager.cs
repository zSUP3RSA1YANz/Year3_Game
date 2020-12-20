using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerHolder;

    public Texture2D mouseLight;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("CharacterHeight", PlayerHolder.GetComponent<CircleCollider2D>().radius * 2);

        PlayerPrefs.SetInt("isSelected", 0);
        PlayerPrefs.SetInt("pathChangable", 0);

        Cursor.SetCursor(mouseLight, Vector2.zero, CursorMode.ForceSoftware);
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
