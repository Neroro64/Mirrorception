using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
    GameObject player, parent;
 
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        parent = transform.parent.gameObject;
    }
    /*
    void OnTriggerEnter()
    {
        if (parent.GetComponent<Pilar>() == null)
            player.GetComponent<PlayerController>().inLadderRange = true;
        else if (parent.GetComponent<Pilar>().direction == 'S' || parent.GetComponent<Pilar>().direction == 'W')
            player.GetComponent<PlayerController>().inLadderRange = true;
    }

    void OnTriggerExit()
    {
        if (player.GetComponent<PlayerController>().inLadderRange == true)
            player.GetComponent<PlayerController>().inLadderRange = false;
    }*/
}
