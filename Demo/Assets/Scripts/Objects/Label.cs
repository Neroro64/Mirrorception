using UnityEngine;
using System.Collections;

public class Label : MonoBehaviour {
    RE_GameSystem_3 gaSystem;
    public char ID;
	
	// Update is called once per frame
	void Start() {
        gaSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<RE_GameSystem_3>();
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            switch(ID){
                case 'D':
                    gaSystem.doorSwitch = true;
                    break;
                case 'R':
                    gaSystem.resetButton = true;
                    break;
                case 'H':
                    gaSystem.hint = true;
                    break;
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
            gaSystem.doorSwitch = gaSystem.resetButton = gaSystem.hint = false;
    }
}
