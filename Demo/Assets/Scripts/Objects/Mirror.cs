using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {
    public char direction, sector;
    Vector3 rot;
    // U = up, D = down, N = north, S = south, W = west, E = east

    bool V3Equal(Vector3 a, Vector3 b)
    {
        return (Vector3.SqrMagnitude(a - b) < 0.0001);
    }

    void Start()
    {
        Quaternion mirrorRot;
        mirrorRot = transform.rotation;
        rot = mirrorRot.eulerAngles;

        /*
         * (0, 0, 0) = West [default]
         * (0, 180, 0) = East
         * (90, 0, 0) = Up
         * (-90, 0, 0) = Down
         * (0, 90, 0) = South
         * (0, -90, 0) = North
         */

        if (V3Equal(rot, new Vector3(0, 270f, 0)) || V3Equal(rot, new Vector3(0, 270f, 180f)))
            direction = 'W';
        else if (V3Equal(rot, new Vector3(0, 90f, 180f)) || V3Equal(rot, new Vector3(0, 90f, 0)))
            direction = 'E';
        else if (V3Equal(rot, new Vector3(90f, 0, 0))) // || V3Equal(rot, new Vector3(270f, 90f, 0)))
            direction = 'U';
        else if (V3Equal(rot, new Vector3(270f, 270f, 0))) //|| V3Equal(rot, new Vector3(90f, 90f, 0)))
            direction = 'D';
        else if (V3Equal(rot, new Vector3(0, 180f, 0)) || V3Equal(rot, new Vector3(0, 180f, 180f)))
            direction = 'S';
        else if (V3Equal(rot, new Vector3(0, 0, 0)) || V3Equal(rot, new Vector3(0, 0, 180f)))
            direction = 'N';
    }
}

