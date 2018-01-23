using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameSystem_01 : GameSystem
{
    MeshRenderer mirrorN, mirrorS;
    bool once;
    protected override void Start()
    {
        //stageName = "Stage";
        base.Start();
        playerScript.gloves = true;
        //once = true;
        //mirrorN = GameObject.Find("Mirror (N)").GetComponentInChildren<MeshRenderer>();
        //mirrorS = GameObject.Find("Mirror (S)").GetComponentInChildren<MeshRenderer>();
        
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (playerScript.isCollidingWithMirror)
        {
            if (!once)
            {
                if (sub != null)
                    Destroy(sub);

                sub = UI.subtitle("按下空格键进入", "UI Display"); //UI.subtitle("PRESS SPACE TO ENTER", "UI Display");//
                once = true;
            }
        }
        /*else if (playerScript.inLadderRange && !playerScript.isClimbing)
        {
            if (sub == null)
                sub = UI.subtitle("Press Space");
        }*/
        else if (playerScript.isGrabbing)
        {
            if (!once)
            {
                if (sub != null)
                    Destroy(sub);
                sub = UI.subtitle("按下空格键放开", "UI Display"); //UI.subtitle("PRESS SPACE TO RELEASE", "UI Display");//
                once = true;
            }
        }
        else if (playerScript.inObjectRange)
        {
            if (sub == null)
                sub = UI.subtitle("按下空格键抓起", "UI Display");//UI.subtitle("PRESS SPACE TO GRABB", "UI Display"); //
        }

        else if (sub != null)
        {
            UI.DestroyTexts(sub, "UI Display");//Destroy(sub);
            once = false;
        }
        if (playerScript.isGrabbing && playerScript.objectRigidbody.gameObject.tag != "Mirror")
        {
            playerScript.freezeRotation = playerScript.freezeVerticalMovement = true;
            playerScript.speed = 1f;
        }
        else
        {
            playerScript.freezeRotation = playerScript.freezeVerticalMovement = false;
            playerScript.speed = 5f;
        }
        //playerRigidbody.constraints.FreezePositionZ = playerRigidbody.constraints.FreezeRotationY = true;
        if (Input.GetKeyDown("r"))
            SceneManager.LoadScene(0, LoadSceneMode.Single);

        if (isTriggered && triggerID == 'E')
        {
            GameObject.Find("END").GetComponent<Canvas>().enabled = true;
            GameObject.Find("True End").GetComponent<TrueEnd>().start = true;
        }
    }
    protected override void afterEnteredTheMirror(char d)
    {
        /*
        if (isSetUp_W && once)
        {
            if (d == 'N' || d == 'S')
                mirrorN.enabled = mirrorS.enabled = false;

            mirrorN.enabled = !mirrorN.enabled;
            mirrorN.enabled = !mirrorN.enabled;
            mirrorS.enabled = !mirrorS.enabled;
            mirrorS.enabled = !mirrorS.enabled;
            once = false;
            //else if (d == 'S')
            //    mirrorS.enabled = false;
        }
        */
        base.afterEnteredTheMirror(d);
        if (d == 'U')
            playerScript.isControllable = false;
        /*
        if (!isSetUp_W)
        {
            if (d == 'N' || d == 'S')
            {
                mirrorN = world.transform.Find("Mirror (N)").GetComponentInChildren<MeshRenderer>();
                mirrorS = world.transform.Find("Mirror (S)").GetComponentInChildren<MeshRenderer>();
                mirrorN.enabled = !mirrorN.enabled;
                mirrorS.enabled = !mirrorS.enabled;
            }
            /*else if (d == 'S')
            {
                mirrorN = world.transform.FindChild("Mirror (N)").GetComponentInChildren<MeshRenderer>();
                mirrorS = world.transform.FindChild("Mirror (S)").GetComponentInChildren<MeshRenderer>();
                mirrorN.enabled = false;
                mirrorS.enabled = true;
            }
            else
            {
                mirrorN = world.transform.Find("Mirror (N)").GetComponentInChildren<MeshRenderer>();
                mirrorS = world.transform.Find("Mirror (S)").GetComponentInChildren<MeshRenderer>();
                mirrorN.enabled = !mirrorN.enabled;
                mirrorN.enabled = !mirrorN.enabled;
                mirrorS.enabled = !mirrorS.enabled;
                mirrorS.enabled = !mirrorS.enabled;
            }
            if (world.transform.position.z < 1f && world.transform.position.z > 0)
                world.transform.position -= new Vector3(0, 0, world.transform.position.z);
            once = true;
        }
        if (d == 'U')
            world.transform.Find("Cube").GetComponent<MeshCollider>().enabled = false;
        */
    }
    protected override Vector3 calcPlayerPosDiff(char d)
    {
        Vector3 posDiff = new Vector3();
        switch (d)
        {
            case 'U': posDiff.Set(0, -4f, 0); break;
            case 'N': posDiff.Set(0, 0, 2f); break;
            case 'E': posDiff.Set(2f, 0, 0); break;
            case 'S': posDiff.Set(0, 0, -2f); break;
            case 'W': posDiff.Set(-2f, 0, 0); break;
        }
        return posDiff;
    }
    protected override Vector3 calcWorldPosDiff(char d)
    {
        Vector3 posDiff = new Vector3();
        switch (d)
        {
            case 'U': posDiff.Set(0, -10f, 0); break;
            case 'N':posDiff.Set(0, 0, 20f); break;
            case 'E': posDiff.Set(20f, 0, 0); break;
            case 'S':posDiff.Set(0, 0, -20f); break;
            case 'W': posDiff.Set(-20f, 0, 0); break;
        }
        return posDiff;
    }

    protected override Vector3 calcWorldRotDiff(char d)
    {
        Vector3 newRot = new Vector3();
        switch (d)
        {
            case 'U': newRot.Set(180f, 0, 0);  break;
            case 'E':
            case 'W': newRot.Set(0, 180f, 0); break;
            case 'N': 
            case 'S': newRot.Set(0, 0, 0); break;
        }
        return newRot;
    }
    protected override Vector3 calcCamPosDiff(char d)
    {
        Vector3 posDiff = new Vector3();
        Vector3 camPos = cam.gameObject.transform.position;
        switch (d)
        {
            case 'U': posDiff.Set(camPos.x, cam.Max.y, camPos.z); break; 
            case 'W': posDiff.Set(cam.Max.x, camPos.y, camPos.z); break;
            case 'E': posDiff.Set(cam.Min.x, camPos.y, camPos.z); break;
            case 'N': posDiff.Set(camPos.x, camPos.y, cam.Min.z); break;
            case 'S': posDiff.Set(camPos.x, camPos.y, cam.Max.z); break;
        }
        return posDiff;
    }

    protected override int calcK(char d, ref Vector3 pos)
    {
        int k = 0;
        if (d == 'U')
            k = 1;
        /*
        switch (d)
        {
            case 'U':
                break;
            case 'E':
            case 'W':
                if (pos.y / 10 != 0)
                {
                    //pos.y -= 10f;
                    k = 0;
                }
                else
                    pos.y += 10f;
                break;
            case 'N':
            case 'S':
                if (pos.y / 10 != 0)
                    k = 0;
                //pos.y += 10f;
                else
                {
                    pos.y += 10f;
                    k = 0;
                }
                break;
        }*/
        return k;
    }
    
}    
    // protected override Vector3 calcCameraPosDiff(char d) {
    //     Vector3 posDiff;
    //     switch (d) {
    //             case 'U': posDiff.Set(0, 10f, 0); break;
    //             case 'N': posDiff.Set(0, 0, -13f); break;
    //             case 'E': posDiff.Set(-24.5f, 0, 0); break;
    //             case 'S': posDiff.Set(0, 0, 12f); break; 
    //             case 'W': posDiff.Set(24.5f, 0, 0); break;
    //         }
    //     return posDiff;   
    //  }   


   
    // protected override bool hasEnteredTheMirror(char d)
    // {
    //     //if (player.transform.position == endPos_P && endPos_P != startPos_P)
    //         //return true;
    //     //else {
    //     if (d == 'U') // if the mirror is facing upward
    //     {
    //         if (endPos_P == startPos_P)
    //             endPos_P.y -= 3f;

    //         t += Time.deltaTime / 2f;
    //         player.transform.localPosition = Vector3.Lerp(startPos_P, endPos_P, t);
    //     }

    //     else if (d == 'W')
    //     {
    //         if (endPos_P == startPos_P)
    //             endPos_P.x -= 4f;
    //         if (!isAnimationPlayed)
    //             StartCoroutine(playEnterMirrorAnimation());
    //     }

    //     else if (d == 'E')
    //     {
    //         if (endPos_P == startPos_P)
    //             endPos_P.x += 4f;
    //         if (!isAnimationPlayed)
    //             StartCoroutine(playEnterMirrorAnimation());
    //     }

    //     else if (d == 'N')
    //     {
    //         if (endPos_P == startPos_P)
    //             endPos_P.z += 2f;
    //         if (!isAnimationPlayed)
    //             StartCoroutine(playEnterMirrorAnimation());
    //     }

    //     else if (d == 'S')
    //     {
    //         if (endPos_P == startPos_P)
    //             endPos_P.z -= 3f;
    //         if (!isAnimationPlayed)
    //             StartCoroutine(playEnterMirrorAnimation());
    //     }

    //     return (player.transform.localPosition == endPos_P);
        
    // }

    // protected override void enteredTheMirror(char d)
    // {
    //     if (d == 'U')
    //     {
    //         //if (GameObject.Find("Prologue_1(Clone)"))
    //         //    Destroy(GameObject.Find("Prologue_1(Clone)"));
           
    //         if (!isSetUp_W)
    //         {
    //             //holder = player.GetComponent<Rigidbody>().constraints;
    //             //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionZ;
    //             //player.GetComponent<Rigidbody>().isKinematic = true;
    //             //playerPos = player.transform.position;
    //             t = 0;

    //             Vector3 newPos = GameObject.Find("Prologue_1").transform.localPosition;
    //             newPos.y -= 10f;
    //             //Quaternion newRotation = Quaternion.identity;
    //             //newRotation.eulerAngles = new Vector3(0, -90f, 0);
    //             GameObject clone = (GameObject)Instantiate(GameObject.Find("Prologue_1"), world.transform);
    //             clone.transform.localPosition = newPos;
    //             //clone.transform.Rotate(new Vector3(0, -90f, 0));
    //             //clone.transform.localRotation = newRotation;

    //             moveAbleObjects[] moveAbleObjects = FindObjectsOfType<moveAbleObjects>();
    //             foreach (moveAbleObjects ob in moveAbleObjects)
    //                 ob.move(1);

    //             startPos_W = endPos_W = world.transform.position;
    //             endPos_W.y += 10f;
    //             isSetUp_W = true;
    //         }

    //         if (world.transform.position != endPos_W)
    //         {
    //             //startPos_W.y += (20f * Time.deltaTime);
    //             //world.transform.position = startPos_W;
    //             t += Time.deltaTime / 0.5f;
    //             world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
    //         }
    //         else
    //         {
    //             //player.transform.position = playerPos + new Vector3(0, 10f, 0);
    //             //player.transform.position = player.transform.position + new Vector3(0, 10f, 0);
               

    //             //player.GetComponent<Rigidbody>().constraints = holder;
    //             player.GetComponent<Rigidbody>().isKinematic = false;
    //             player.GetComponent<PlayerController>().isControllable = true;

    //             //Clean up
    //             Destroy(GameObject.Find("Prologue_1"));
    //             GameObject.Find("Prologue_1(Clone)").name = "Prologue_1";
    //             isSetUp_P = isSetUp_W = isAnimationPlayed = false;
    //             willEnterMirror = false;
    //         }
    //     }

    //     else if (d == 'W')
    //     {
    //         int k = 1;

    //         if (!isSetUp_W)
    //         {
    //             t = 0;

    //             player.GetComponent<PlayerController>().anim.SetBool("IsEnteringMirror", false);
    //             //playerPos = player.transform.position;
    //             //player.transform.localPosition += new Vector3(0, 30f, 0);
                
    //             Vector3 newPos = GameObject.Find("Prologue_1").transform.localPosition;
    //             newPos.x -= 24.5f;
    //             if ((newPos.y % 10) != 0)
    //             {
    //                 newPos.y -= 8;
    //                 k = 0;
    //             }
    //             else 
    //                 newPos.y += 8;

    //             //Quaternion newRotation = Quaternion.identity;
    //             //newRotation.eulerAngles = new Vector3(180f, -90f, 0);
    //             GameObject clone = (GameObject)Instantiate(GameObject.Find("Prologue_1"), world.transform);
    //             clone.transform.localPosition = newPos;
    //             clone.transform.Rotate(new Vector3(180f, 0, 0));
    //             //clone.transform.localRotation = newRotation;

    //             moveAbleObjects[] moveAbleObjects = FindObjectsOfType<moveAbleObjects>();
    //             foreach (moveAbleObjects ob in moveAbleObjects)
    //                 ob.move(k);



    //             startPos_W = endPos_W = world.transform.position;
    //             endPos_W.x += 24.5f;
    //             isSetUp_W = true;

    //         }
           
    //         if (world.transform.position != endPos_W)
    //         {
    //             //startPos_W.x += (49f * Time.deltaTime);
    //             //world.transform.position = startPos_W;
    //             t += Time.deltaTime / 0.5f;
    //             world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
    //         }
            
    //         else
    //         {
    //             //player.transform.position = endPos_P;// + new Vector3(24f, 0, 0);

    //             //player.GetComponent<Rigidbody>().constraints = holder;
    //             player.GetComponent<Rigidbody>().isKinematic = false;
    //             player.GetComponent<PlayerController>().isControllable = true;

    //             //Clean up
    //             Destroy(GameObject.Find("Prologue_1"));
    //             GameObject.Find("Prologue_1(Clone)").name = "Prologue_1";
    //             isSetUp_P = isSetUp_W = isAnimationPlayed = false;
    //             willEnterMirror = false;
    //             Info = null;
    //             mirror = null;
    //         }
    //     }

    //     else if (d == 'E')
    //     {
    //         if (!isSetUp_W)
    //         {
    //             int k = 1;
    //             t = 0;

    //             player.GetComponent<PlayerController>().anim.SetBool("IsEnteringMirror", false);
    //             //playerPos = player.transform.position;
    //             //player.transform.localPosition += new Vector3(0, 30f, 0);

    //             Vector3 newPos = GameObject.Find("Prologue_1").transform.localPosition;
    //             newPos.x += 24.5f;
    //             if ((newPos.y % 10) != 0)
    //             {
    //                 newPos.y -= 8f;
    //                 k = 0;
    //             }
    //             else
    //                 newPos.y += 8f;
    //             //Quaternion newRotation = Quaternion.identity;
    //             //newRotation.eulerAngles = new Vector3(180f, -90f, 0);
    //             GameObject clone = (GameObject)Instantiate(GameObject.Find("Prologue_1"), world.transform);
    //             clone.transform.localPosition = newPos;
    //             clone.transform.Rotate(new Vector3(180f, 0, 0));
    //             //clone.transform.localRotation = newRotation;

    //             moveAbleObjects[] moveAbleObjects = FindObjectsOfType<moveAbleObjects>();
    //             foreach (moveAbleObjects ob in moveAbleObjects)
    //                 ob.move(k);

    //             startPos_W = endPos_W = world.transform.position;
    //             endPos_W.x -= 24.5f;
    //             isSetUp_W = true;

    //         }

    //         if (world.transform.position != endPos_W)
    //         {
    //             //startPos_W.x -= (49f * Time.deltaTime);
    //             //world.transform.position = startPos_W;
    //             t += Time.deltaTime / 0.5f;
    //             world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
    //         }

    //         else
    //         {
    //             //player.transform.position = endPos_P;// + new Vector3(24f, 0, 0);

    //             //player.GetComponent<Rigidbody>().constraints = holder;
    //             player.GetComponent<Rigidbody>().isKinematic = false;
    //             player.GetComponent<PlayerController>().isControllable = true;

    //             //Clean up
    //             Destroy(GameObject.Find("Prologue_1"));
    //             GameObject.Find("Prologue_1(Clone)").name = "Prologue_1";
    //             isSetUp_P = isSetUp_W = isAnimationPlayed = false;
    //             willEnterMirror = false;
    //             Info = null;
    //             mirror = null;
    //         }
    //     }

    //     else if (d == 'N')
    //     {
    //         if (!isSetUp_W)
    //         {
    //             int k = 1;
    //             t = 0;

    //             player.GetComponent<PlayerController>().anim.SetBool("IsEnteringMirror", false);
    //             //playerPos = player.transform.position;
    //             //player.transform.localPosition += new Vector3(0, 30f, 0);

    //             Vector3 newPos = GameObject.Find("Prologue_1").transform.localPosition;
    //             newPos.z += 13f;
    //             if ((newPos.y % 10) == 0)
    //                 newPos.y += 8f;
    //             else
    //             {
    //                 newPos.y -= 8f;
    //                 k = 0;
    //             }
    //             //Quaternion newRotation = Quaternion.identity;
    //             //newRotation.eulerAngles = new Vector3(180f, -90f, 0);
    //             GameObject clone = (GameObject)Instantiate(GameObject.Find("Prologue_1"), world.transform);
    //             clone.transform.localPosition = newPos;
    //             clone.transform.Rotate(new Vector3(180f, 180f, 0));
    //             //clone.transform.localRotation = newRotation;

    //             moveAbleObjects[] moveAbleObjects = FindObjectsOfType<moveAbleObjects>();
    //             foreach (moveAbleObjects ob in moveAbleObjects)
    //                 ob.move(k);

    //             startPos_W = endPos_W = world.transform.position;
    //             endPos_W.z -= 13f;
    //             isSetUp_W = true;

    //         }

    //         if (world.transform.position != endPos_W)
    //         {
    //             //startPos_W.x -= (49f * Time.deltaTime);
    //             //world.transform.position = startPos_W;
    //             t += Time.deltaTime / 0.5f;
    //             world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
    //         }

    //         else
    //         {
    //             //player.transform.position = endPos_P;// + new Vector3(24f, 0, 0);

    //             //player.GetComponent<Rigidbody>().constraints = holder;
    //             player.GetComponent<Rigidbody>().isKinematic = false;
    //             player.GetComponent<PlayerController>().isControllable = true;

    //             //Clean up
    //             Destroy(GameObject.Find("Prologue_1"));
    //             GameObject.Find("Prologue_1(Clone)").name = "Prologue_1";
    //             isSetUp_P = isSetUp_W = isAnimationPlayed = false;
    //             willEnterMirror = false;
    //             Info = null;
    //             mirror = null;
    //         }
    //     }

    //     else if (d == 'S')
    //     {
    //         if (!isSetUp_W)
    //         {
    //             int k = 1;
    //             t = 0;

    //             player.GetComponent<PlayerController>().anim.SetBool("IsEnteringMirror", false);
    //             //playerPos = player.transform.position;
    //             //player.transform.localPosition += new Vector3(0, 30f, 0);

    //             Vector3 newPos = GameObject.Find("Prologue_1").transform.localPosition;
    //             newPos.z -= 12f;
    //             if ((newPos.y % 10) == 0)
    //                 newPos.y += 8f;
    //             else
    //             {
    //                 newPos.y -= 8f;
    //                 k = 0;
    //             }
    //             //Quaternion newRotation = Quaternion.identity;
    //             //newRotation.eulerAngles = new Vector3(180f, -90f, 0);
    //             GameObject clone = (GameObject)Instantiate(GameObject.Find("Prologue_1"), world.transform);
    //             clone.transform.localPosition = newPos;
    //             clone.transform.Rotate(new Vector3(0, 0, 180f));
    //             //clone.transform.localRotation = newRotation;

    //             moveAbleObjects[] moveAbleObjects = FindObjectsOfType<moveAbleObjects>();
    //             foreach (moveAbleObjects ob in moveAbleObjects)
    //                 ob.move(k);

    //             startPos_W = endPos_W = world.transform.position;
    //             endPos_W.z += 12f;
    //             isSetUp_W = true;

    //         }

    //         if (world.transform.position != endPos_W)
    //         {
    //             //startPos_W.x -= (49f * Time.deltaTime);
    //             //world.transform.position = startPos_W;
    //             t += Time.deltaTime / 0.5f;
    //             world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
    //         }

    //         else
    //         {
    //             //player.transform.position = endPos_P;// + new Vector3(24f, 0, 0);

    //             //player.GetComponent<Rigidbody>().constraints = holder;
    //             player.GetComponent<Rigidbody>().isKinematic = false;
    //             player.GetComponent<PlayerController>().isControllable = true;

    //             //Clean up
    //             Destroy(GameObject.Find("Prologue_1"));
    //             GameObject.Find("Prologue_1(Clone)").name = "Prologue_1";
    //             isSetUp_P = isSetUp_W = isAnimationPlayed = false;
    //             willEnterMirror = false;
    //             Info = null;
    //             mirror = null;
    //         }
    //     }

    
