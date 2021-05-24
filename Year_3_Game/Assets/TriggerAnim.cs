using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnim : MonoBehaviour
{
    public Animator anim;
    public string clip;

    //triggers animation
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            anim.SetBool(clip, true);
    }
}
