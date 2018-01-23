using UnityEngine;
using System.Collections;

public class grabbableObject : MonoBehaviour
{
    public Transform originalParent;
    public Vector3 defaultPosition;
    PlayerController pScript;

    
    //public bool on;
    //Vector3 pos;
    //Transform childMirror;

    void Start()
    {
        originalParent = this.transform.parent;
        defaultPosition = this.transform.localPosition;

        pScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //childMirror = GetComponentInChildren<Mirror>().gameObject.transform;
        //pos = transform.position - childMirror.position;
    }
    

}
