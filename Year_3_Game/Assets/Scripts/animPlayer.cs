using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPlayer : MonoBehaviour
{
    public Animator anim;

    public void animPlay(string myCondtion)
    {
        anim.GetComponent<Animator>().SetBool(myCondtion, true);
    }

    public void animStop(string myCondtion)
    {
        anim.GetComponent<Animator>().SetBool(myCondtion, false);
    }
}
