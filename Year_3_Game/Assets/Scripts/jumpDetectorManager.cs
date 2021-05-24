using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpDetectorManager : MonoBehaviour
{
    //resets all jump points
    public void resetJumpDetectors()
    {
        BroadcastMessage("reset");
    }
}
