using UnityEngine;
using System.Collections;

public class moveAbleObjects : MonoBehaviour {
    public Vector3 startPos; //endPos_DOWN
    public Vector3 startRotation;
    public Vector3 endPos_UP;
    public Vector3 endRotation;
    public Vector3 posDiff;

    public void move(int key)
    {
        // flip
        if (key == 1)
        {
            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = endRotation;
            transform.localPosition = endPos_UP;
            transform.localRotation = newRotation;
        }

        // set to default
        else if (key == 0)
        {
            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = startRotation;
            transform.localPosition = startPos;
            transform.localRotation = newRotation;
        }
    }

    public void flip(int k){
        if (k == 1)
        {
            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles.Set(0, 0, 180f);
            transform.localPosition += posDiff;
            transform.localRotation = newRotation;
        }
    
    }
}
