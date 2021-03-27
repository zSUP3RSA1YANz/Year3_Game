﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posMarker : MonoBehaviour
{
    public GameObject posMark;
    public GameObject player;
    private CustomPathAI path;

    void Start()
    {
        path = player.GetComponent<CustomPathAI>();
    }

    void OnMouseDown()
    {
        if(leftMouseClicked())
        {
            setPos();
            goToPos();
        }
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

    void setPos()
    {
        PlayerPrefs.SetFloat("newTargX", posMark.transform.position.x);
        PlayerPrefs.SetFloat("newTargY", posMark.transform.position.y);
        PlayerPrefs.SetFloat("newTargZ", this.transform.position.z);
    }

    void goToPos()
    {
        PlayerPrefs.SetInt("isSelected", 1);
        path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        Debug.Log("Pillar");
    }
}
