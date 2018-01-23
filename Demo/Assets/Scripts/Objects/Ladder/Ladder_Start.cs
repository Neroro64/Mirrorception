using UnityEngine;
using System.Collections;

public class Ladder_Start : MonoBehaviour {

    PlayerController playerScript;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
            playerScript.cancelClimb = true;
    }

    void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
            playerScript.cancelClimb = false;
    }
}
