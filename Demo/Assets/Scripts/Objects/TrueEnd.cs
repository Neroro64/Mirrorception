using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueEnd : MonoBehaviour {

    GameObject player;
    Vector3 offset;
    public bool start;

    private void Start()
    {
        start = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void LateUpdate()
    {
        if (start)
            transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + offset.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().isControllable = true;
            foreach (BoxCollider bc in GameObject.Find("Grabbable Mirror").GetComponentsInChildren<BoxCollider>())
                bc.enabled = false;
            //GameObject.Find("END").GetComponent<Canvas>().enabled = true;
            offset = transform.position - player.transform.position;
            Camera.main.gameObject.GetComponent<myCamera>().recalcMinMax2(new Vector3(0, 0, 1000f));
        }
    }


}
