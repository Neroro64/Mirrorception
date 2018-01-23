using UnityEngine;
using System.Collections;

public class Pilar : MonoBehaviour {
    Quaternion newRotation;
    //Vector3 startRotation;
    public bool unknown, trueUnknown;
    // 'N' && 1 or 3 || 'E' && 1 or 2
    public char direction;
    // N, S, E, W
    public Vector3 posNE, posSE, posSW, posNW;
    // 0, 1, 2, 3
    public Vector3 rotN, rotE, rotS, rotW;
    // N, E, S, W

    bool V3Equal(Vector3 a, Vector3 b)
    {
        return (Vector3.SqrMagnitude(a - b) < 0.0001);
    }

    void Start()
    {
        //trueUnknown = false;
        updateDirection();
        //Vector3 pos = transform.localPosition;

        /*if ((direction == 'E' || direction == 'N') && V3Equal(pos, posSW))
            trueUnknown = true;
        if (direction == 'N' && V3Equal(pos, posSE))
            unknown = true;
        else if (direction == 'N' && V3Equal(pos, posSW))
            unknown = true;
        else
            unknown = false;
        */
    }

    public void moveAndRotate(byte p, char r)
    {
        
        switch (r)
        {
            case 'N':
                newRotation.eulerAngles = rotN;
                transform.localRotation = newRotation;
                break;
            case 'E':
                newRotation.eulerAngles = rotE;
                transform.localRotation = newRotation;
                break;
            case 'S':
                newRotation.eulerAngles = rotS;
                transform.localRotation = newRotation;
                break;
            case 'W':
                newRotation.eulerAngles = rotW;
                transform.localRotation = newRotation;
                break;
        }

        switch (p)
        {
            case 0:
                transform.localPosition = posNE;
                break;
            case 1:
                transform.localPosition = posSE;
                break;
            case 2:
                transform.localPosition = posSW;
                break;
            case 3:
                transform.localPosition = posNW;
                break;
        }    
    }
     public char returnCurrentDirection()
    {
        
        Vector3 rot = transform.localRotation.eulerAngles;
        char d = '\0';
        if (V3Equal(rot, rotE))
            d = 'E';
        else if (V3Equal(rot, rotS))
            d = 'S';
        else if (V3Equal(rot, rotW))
            d = 'W';
        else if (V3Equal(rot, rotN))
            d = 'N';
        return d;
    }

    public void updateDirection()
    {
        direction = returnCurrentDirection();
    }


}
