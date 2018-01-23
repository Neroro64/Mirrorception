using UnityEngine;
using System.Collections;

public class RotatingSphere : MonoBehaviour {
    Vector3 rot;
    
    void Start()
    {
        rot = new Vector3(0, 5f, 0);
    }
	void Update () {
        transform.Rotate(Time.deltaTime * rot);
	}
}
