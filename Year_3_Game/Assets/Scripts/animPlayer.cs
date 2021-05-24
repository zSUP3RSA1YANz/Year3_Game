using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animPlayer : MonoBehaviour
{
    public Animator anim;

    //Plays animation based on string
    public void animPlay(string myCondtion)
    {
        anim.GetComponent<Animator>().SetBool(myCondtion, true);
    }

    //Stops animation based on string
    public void animStop(string myCondtion)
    {
        anim.GetComponent<Animator>().SetBool(myCondtion, false);
    }
}
