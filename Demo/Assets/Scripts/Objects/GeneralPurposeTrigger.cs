using UnityEngine;
using System.Collections;

public class GeneralPurposeTrigger : MonoBehaviour {
    public char triggerID;
    GameObject gameSystem;
    void Start()
    {
        gameSystem = GameObject.FindGameObjectWithTag("GameController");
    }

	void OnTriggerEnter(Collider info)
    {
        if (info.gameObject.tag == "Player")
        {
            gameSystem.GetComponent<GameSystem>().isTriggered = true;
            gameSystem.GetComponent<GameSystem>().triggerID = this.triggerID;
        }
    }

    void OnTriggerExit(Collider info)
    {
        if (info.gameObject.tag == "Player")
        {
            //gameSystem.GetComponent<GameSystem>().triggerID = '\0';
            gameSystem.GetComponent<GameSystem>().isTriggered = false;
        }
    }
}
