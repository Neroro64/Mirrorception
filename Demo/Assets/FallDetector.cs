using UnityEngine;
using System.Collections;

public class FallDetector : MonoBehaviour {
    void OnTriggerStay (Collider c)
    {
        if (c == null)
        {
            transform.parent.GetComponent<PlayerController>().isControllable = false;
        }
        else
            transform.parent.GetComponent<PlayerController>().isControllable = true;
    }
	
}
