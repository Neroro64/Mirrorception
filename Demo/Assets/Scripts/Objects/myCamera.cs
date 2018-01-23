using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class myCamera : MonoBehaviour {
    public Vector3 Max, Min;
    Vector3 offset;
    GameObject player, stage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }
	
	void LateUpdate () {

        transform.position = player.transform.position + offset;
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, Min.x, Max.x), 
        Mathf.Clamp(transform.position.y, Min.y, Max.y), 
        Mathf.Clamp(transform.position.z, Min.z, Max.z));

    }

    public void recalcMinMax(Vector3 posDiff)
    {
        //Vector3 posDiff = new Vector3(w.transform.position.x, 0, w.transform.position.z);
        Max += posDiff;
        Min += posDiff;
    }
    public void recalcMinMax2(Vector3 posDiff)
    {
        //Vector3 posDiff = new Vector3(w.transform.position.x, 0, w.transform.position.z);
        Max += posDiff;
        Min -= posDiff;
    }

    public void recalcOffset()
    {
        offset = transform.position - player.transform.position;
    }
}
