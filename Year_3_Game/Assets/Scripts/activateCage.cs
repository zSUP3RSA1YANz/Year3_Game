using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateCage : MonoBehaviour
{
    public GameObject cage;
    public GameManager GM;
    public GameObject canvasFlashBack;
    public GameObject cam3;

    [Range(1,5)]
    public float transitionTime;

    //Activates cutscene when player enters
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            GM.disableCursor();
            cam3.SetActive(true);
            canvasFlashBack.GetComponent<Animator>().SetBool("FlashBack", true);
            StartCoroutine("activeCage");
        }
    }

    //Makes sure that the trigger cannot be activated again after cutscene
    IEnumerator activeCage()
    {
        yield return new WaitForSeconds(transitionTime);
        cage.SetActive(true);
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

}
