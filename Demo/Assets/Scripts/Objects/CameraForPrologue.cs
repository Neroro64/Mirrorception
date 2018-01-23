using UnityEngine;
using System.Collections;
/*      - 5  -  / - 6 -
 * -----------
 * - 3  - 4  -
 * -----------
 * - 1  - 2  -
 * -----------
 * 
 * POSITIONS
 * [0] = default; [1] = 2; [2] = 3; [3] = 4  
 * [4] = 5(F); [5] = 6(C)
 * 
 * ROTATIONS
 * [0] = default; [1] = Looking at left; [2] = Looking at right
 */
public class CameraForPrologue : MonoBehaviour
{
    public Vector3[] Positions;
    public Vector3[] Rotations;
    GameSystem_03 gaSystem;

    void Start()
    {
        gaSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSystem_03>();
    }

    bool V3Equal(Vector3 a, Vector3 b)
    {
        return (Vector3.SqrMagnitude(a - b) < 0.0001);
    }

    public Vector3 returnCurrentPos()
    {
        return transform.position;
    }
    public Vector3 returnCurrentRot()
    {
        return transform.rotation.eulerAngles;
    }

    public int returnCurrentPosIndex()
    {
        for (int i = 0; i < Positions.Length; i++)
        {
            if (V3Equal(transform.position, Positions[i]))
                return i;
        }
        return -1;
    }

    public int returnCurrentRotIndex()
    {
        for (int i = 0; i < Rotations.Length; i++)
        {
            if (V3Equal(transform.rotation.eulerAngles, Rotations[i]))
                return i;
        }
        return -1;
    }
    public Vector3 returnNewPos(char triggerID)
    {
        int p = returnCurrentPosIndex();
        switch (p)
        {
            case 0:
                if (triggerID == 'H')
                    p = 1;
                else if (triggerID == 'V')
                    p = 2;
                else if (triggerID == 'R')
                    p = 4;
                break;
            case 1:
                if (triggerID == 'H')
                    p = 0;

                else if (triggerID == 'D')
                {
                    Quaternion r = Quaternion.identity;
                    r.eulerAngles = Rotations[0];
                    transform.localRotation = r;

                }
                break;
            case 2:
                if (triggerID == 'V')
                    p = 0;
                break;
            case 3:
                if (triggerID == 'H')
                    p = 6;
                break;
            case 4:
                if (triggerID == 'H')
                {
                    p = 6;
                    gaSystem.turnLeft2 = false;
                }
                else if (triggerID == 'R')
                    p = 0;
                break;
            case 6:
                if (triggerID == 'H')
                {
                    if (gaSystem.isTurned)
                    {
                        p = 4;
                        gaSystem.turnLeft2 = true;
                    }
                    else
                    {
                        p = 3;
                        gaSystem.turnRight = gaSystem.turnForward = true;
                    }
                    //p = (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameSystem_03>().isTurned) ? 4 : 3;
                }
                else if (triggerID == 'V')
                    p = 8;
                else if (triggerID == 'D' || triggerID == 'B')
                {
                    p = 7;
                    Quaternion r = Quaternion.identity;
                    r.eulerAngles = Rotations[1];
                    transform.localRotation = r;
                }
                break;
            case 7:
                if (triggerID == 'B')
                {
                    p = 6;
                    Quaternion r = Quaternion.identity;
                    r.eulerAngles = Rotations[0];
                    transform.localRotation = r;
                }
                break;
            case 8:
                if (triggerID == 'V')
                    p = 6;
                //else if (triggerID == 'D')
                //{
                    /*
                    Quaternion r = Quaternion.identity;
                    r.eulerAngles = Rotations[0];
                    transform.localRotation = r;
                    */
                //}
                break;
            /*case 9:
                if (triggerID == 'D')
                {
                    /*
                    Quaternion r = Quaternion.identity;
                    r.eulerAngles = Rotations[2];
                    transform.localRotation = r;
                    
                }
                break;*/
        }
        return Positions[p];
    }
    /*public Vector3 returnNewPos(char triggerID)
    {
        int p = returnCurrentPosIndex();
        switch (p)
        {
            case 0:
                if (triggerID == 'H')
                    p = 1;
                else if (triggerID == 'R')
                    return returnCurrentPos() + new Vector3(0, 20f, 0);
                else if (triggerID == 'C')
                    p = 6;
                else
                    p = 2;
                break;
            case 1:
                if (triggerID == 'H')
                    p = 0;
                else if (triggerID == 'L')
                    return returnCurrentPos() + new Vector3(0, 20f, 0);
                break;
            case 2:
                if (triggerID == 'V')
                    p = 3;
                else
                    p = 0;

                break;
            case 3:
                if (triggerID == 'H')
                    p = 6;
                else
                    p = 2;
                break;
            case 4:
                if (triggerID == 'H')
                    p = 5;
                else
                    p = 8;
                break;
            case 5:
                p = 4;
                break;
            case 6:
                if (triggerID == 'H')
                    p = 7;
                else
                    p = 0;
                break;
            case 7:
                p = 6;
                break;
            case 8:
                if (triggerID == 'R')
                    return returnCurrentPos() + new Vector3(0, 20f, 0);
                else if (triggerID == 'H')
                    p = 9;
                else
                    p = 4;
                break;
            case 9:
                if (triggerID == 'L')
                    return returnCurrentPos() + new Vector3(0, 20f, 0);
                p = 8;
                break;    
            default:
                Vector3 posDiff = new Vector3(0, 20f, 0);
                if (triggerID == 'H')
                {
                    if (returnCurrentPos() - posDiff == Positions[0])
                        return Positions[1] + posDiff;
                    else
                        return Positions[0] + posDiff;
                }

                return (returnCurrentPos() - posDiff);
        }
        return Positions[p];
    }*/

    public Vector3 returnNewRot(char triggerID)
    {
        int r = returnCurrentRotIndex();
        switch (r)
        {
            case 0:
                if (triggerID == 'L')
                    r = 1;
                else
                    r = 2;
                break;
            case 1:
            case 2:
                if (triggerID == 'H')
                {
                    r = (r == 1) ? 2 : 1;
                    break;
                }
                r = 0;
                break;
        }

        return Rotations[r];
    }
}
