using UnityEngine;
using System.Collections;

public class Ladder_End : MonoBehaviour {
    PlayerController playerScript;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
            playerScript.climbDone = true;
    }

    void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
            playerScript.climbDone = false;
    }
}
