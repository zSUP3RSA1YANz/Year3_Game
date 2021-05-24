using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnMarker;

    //reset all spike pos
    public void resetAllSpikes()
    {
        BroadcastMessage("reset");
    }

    //respawn player
    public void playerRespawn()
    {
        player.GetComponent<CustomPathAI>().respawnPos(respawnMarker.transform.position.x, respawnMarker.transform.position.y);
    }
}
