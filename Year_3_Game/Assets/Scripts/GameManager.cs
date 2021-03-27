using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerHolder;

    public Texture2D mouseLight;

    public GameObject interactableEffect;
    public GameObject interactableEffectLooped;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("CharacterHeight", PlayerHolder.GetComponent<CircleCollider2D>().radius * 2);

        PlayerPrefs.SetInt("isSelected", 0);
        PlayerPrefs.SetInt("pathChangable", 0);

        Cursor.SetCursor(mouseLight, Vector2.zero, CursorMode.ForceSoftware);

        interactableEffect.SetActive(false);
        interactableEffectLooped.SetActive(false);
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

    public void playInteractableEffect()
    {
        interactableEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        interactableEffect.SetActive(true);
        interactableEffect.GetComponent<ParticleSystem>().Play();
    }

    public void playInteractableEffectLooped()
    {
        interactableEffectLooped.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        interactableEffectLooped.SetActive(true);
        interactableEffectLooped.GetComponent<ParticleSystem>().Play();
    }

    public void stopInteractableEffectLooped()
    {
        interactableEffectLooped.SetActive(false);
        interactableEffectLooped.GetComponent<ParticleSystem>().Stop();
    }

    public bool isPlayingInteractEffectLooped()
    {
        if (interactableEffectLooped.GetComponent<ParticleSystem>().isPlaying)
        {
            return true;
        }
        else
            return false;
    }

    public void teleportPlayer(GameObject posMarker)
    {
        PlayerHolder.transform.position = posMarker.transform.position;
        Debug.Log("new level");
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (leftMouseClicked() && (PlayerPrefs.GetInt("isSelected") == 1))
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
