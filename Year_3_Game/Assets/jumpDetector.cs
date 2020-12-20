using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpDetector : MonoBehaviour
{
    CustomPathAI path;
    public GameObject player;
    public GameObject prevBackground;
    public GameObject newBackground;
    public GameObject newJumpDetector;

    public GameObject background1;
    public GameObject background2;
    public GameObject background3;

    public GameObject jumpPosMark;

    public float jumpPrepTime;


    void Start()
    {
        path = player.GetComponent<CustomPathAI>();
    }

    void OnMouseDown()
    {
        PlayerPrefs.SetInt("isSelected", 1);
        passOnInfo();
        path.setTargetPosition(PlayerPrefs.GetFloat("newTargX"), PlayerPrefs.GetFloat("newTargY"), PlayerPrefs.GetFloat("newTargZ"));
        StartCoroutine("wait");
    }

    void OnTriggerEnter2D()
    {
        newBackground.SetActive(true);
        newJumpDetector.GetComponent<BoxCollider2D>().enabled = true;
        prevBackground.SetActive(false);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void reset()
    {
        background1.SetActive(true);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        background2.SetActive(false);
        background3.SetActive(false);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(jumpPrepTime);
        path.longJumpRight();
    }

    void passOnInfo()
    {
        PlayerPrefs.SetFloat("newTargX", jumpPosMark.transform.position.x);
        PlayerPrefs.SetFloat("newTargY", jumpPosMark.transform.position.y);
        PlayerPrefs.SetFloat("newTargZ", jumpPosMark.transform.position.z);
    }
}
