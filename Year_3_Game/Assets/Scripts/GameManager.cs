using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerHolder;

    public Texture2D mouseLight;
    public Vector2 lightOffset;

    public GameObject interactableEffect;
    public GameObject interactableEffectLooped;

    public bool tpToStartPos;
    public Vector3 startPos;

    public GameObject boss1Respawn;

    private GameObject spritePos;

    [SerializeField] private LayerMask inputLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("CharacterHeight", PlayerHolder.GetComponent<CircleCollider2D>().radius * 2);

        PlayerPrefs.SetInt("isSelected", 0);
        PlayerPrefs.SetInt("pathChangable", 0);
        PlayerPrefs.SetInt("playerHit", 0);

        Cursor.SetCursor(mouseLight, Vector2.zero + lightOffset, CursorMode.Auto);

        Camera.main.eventMask = inputLayerMask;

        interactableEffect.SetActive(false);
        interactableEffectLooped.SetActive(false);

        if(tpToStartPos)
        {
            PlayerHolder.transform.position = startPos;
        }

        spritePos = GameObject.Find("Player");
        spritePos.SetActive(false);
    }

    //enables mouse
    public void enableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //enables mouse
    public void disableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //play light effect
    public void playInteractableEffect()
    {
        interactableEffect.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        interactableEffect.SetActive(true);
        interactableEffect.GetComponent<ParticleSystem>().Play();
    }

    //play light effect looped
    public void playInteractableEffectLooped()
    {
        interactableEffectLooped.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        interactableEffectLooped.SetActive(true);
        interactableEffectLooped.GetComponent<ParticleSystem>().Play();
    }

    //stops light effect
    public void stopInteractableEffectLooped()
    {
        interactableEffectLooped.SetActive(false);
        interactableEffectLooped.GetComponent<ParticleSystem>().Stop();
    }

    //checks if light effect playing
    public bool isPlayingInteractEffectLooped()
    {
        if (interactableEffectLooped.GetComponent<ParticleSystem>().isPlaying)
        {
            return true;
        }
        else
            return false;
    }

    //teleports Annie
    public void teleportPlayer(GameObject posMarker)
    {
        PlayerHolder.transform.position = posMarker.transform.position;
        Debug.Log("new level");
    }

    //teleports Annie in boss area
    public void teleportPlayerBoss1()
    {
        PlayerHolder.transform.position = boss1Respawn.transform.position;
        Debug.Log("new level");
    }

    //activates object
    public void activateObj(GameObject Obj)
    {
        Obj.SetActive(true);
    }

    //Deactivates object
    public void DeactivateObj(GameObject Obj)
    {
        Obj.SetActive(false);
    }
}
