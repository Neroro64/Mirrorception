using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameSystem_02 : GameSystem {
    //GameObject sub2;
    bool notDone, flipped;
    Vector3 startRot, endRot;
    float currentLerpTime, lerpTime;
    GameObject pilar, info;
    string[] intro = { " " }; //{ "Ever heard of Schrondiger's cat?", "No? It's okay", "That theory is irrelevant to this game anyway...", ""};
    int i = 0;
    
    protected override void Start () {
        //stageName = "Prologue_2";
        base.Start();
        //introCanvas = GameObject.Find("Intro").GetComponent<Canvas>();
        //UI.printMultipleLines(intro, "Intro");
        //sub2 = UI.notice("Ever heard of Schrodinger's cat?");
        //Destroy(sub2, 7f);
    }
	
	protected override void Update () {
        base.Update();
        if (i != intro.Length)
        {
            if (info == null)
            {
                info = UI.infoText(intro[i++], "Intro");
                Destroy(info, 3f);
            }
        }
        else if (info != null)
            UI.DestroyTexts(info, "Intro");

        if (playerScript.isCollidingWithMirror)
        {
            if (sub == null)
                sub = UI.subtitle("按下空格键进入", "UI Display");//UI.subtitle("PRESS SPACE TO ENTER", "UI Display"); //
        }
        else if (playerScript.inLadderRange && !playerScript.isClimbing)
        {
            if (sub == null)
                sub = UI.subtitle("按下空格键开始爬", "UI Display");//UI.subtitle("PRESS SPACE TO CLIMB", "UI Display"); //
        }
        else if (isTriggered)
        {
            if (sub == null)
            {
                sub = UI.subtitle("按下空格键启动", "UI Display"); //UI.subtitle("PRESS SPACE TO ACTIVATE", "UI Display");//
            }
        }

        else
            UI.DestroyTexts(sub, "UI Display");
        
        if ((isTriggered && triggerID == 'R') || notDone)
        {
            if (!notDone && Input.GetKeyDown("space"))
            {
                notDone = true;

                pilar = GameObject.Find("Pilar");
                startRot = endRot = pilar.transform.localRotation.eulerAngles;//GameObject.Find("Pilar").transform.rotation.eulerAngles;
                endRot += new Vector3(0, 90f, 0);
                if (endRot.y == 360f)
                    endRot.y = 0;
                lerpTime = 1f;
                currentLerpTime = 0;
                //GameObject.Find("Pilar").transform.Rotate(new Vector3(0, 90f, 0));
            }
            else if (notDone)
            {
                if (pilar.transform.localRotation.eulerAngles != endRot)
                { 
                    
                    currentLerpTime += Time.deltaTime;
                    if (currentLerpTime > lerpTime)
                        currentLerpTime = lerpTime;
                    Quaternion rot = Quaternion.identity;
                    rot.eulerAngles = Vector3.Lerp(startRot, endRot, currentLerpTime/lerpTime);
                    if (rot.eulerAngles.y == 360f && endRot.y == 0)
                        rot.eulerAngles = endRot;
                    pilar.transform.localRotation = rot;
                }

                else
                {
                    pilar.GetComponent<Pilar>().updateDirection(); 
                    notDone = false;
                    triggerID = '\0';
                }
            }
        }

        if (Input.GetKeyDown("r"))
            SceneManager.LoadScene(1, LoadSceneMode.Single);

        /*if (sub2 == null && !done)
        {
            sub2 = UI.notice("You will never know what's on the other side until you see it");
            Destroy(sub2, 10f);
            done = true;
        }*/
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
    protected override Vector3 calcPlayerPosDiff(char d)
    {
        Vector3 posDiff = new Vector3();

        switch (d)
        {
            case 'N': posDiff.Set(0, 0, 0.7f); break;
            case 'E': posDiff.Set(0.7f, 0, 0); break;
            case 'S': posDiff.Set(0, 0, -0.7f); break;
            case 'W': posDiff.Set(-0.7f, 0, 0); break;
        }
        return posDiff;
        /*
        if (d == 'N')
        {
            if (endPos_P == startPos_P)
                endPos_P.z += 0.7f;
            if (!isAnimationPlayed)
                StartCoroutine(playEnterMirrorAnimation());
        }

        if (d == 'E')
        {
            if (endPos_P == startPos_P)
                endPos_P.x += 0.7f;
            if (!isAnimationPlayed)
                StartCoroutine(playEnterMirrorAnimation());
        }
        
        if (d == 'S')
        {
            if (endPos_P == startPos_P)
                endPos_P.z -= 0.7f;
            if (!isAnimationPlayed)
                StartCoroutine(playEnterMirrorAnimation());
        }

        if (d == 'W')
        {
            if (endPos_P == startPos_P)
                endPos_P.x -= 0.7f;
            if (!isAnimationPlayed)
                StartCoroutine(playEnterMirrorAnimation());
        }
        
        return (player.transform.localPosition == endPos_P);
        */

    }

    protected override Vector3 calcWorldRotDiff(char d)
    {
        movePilar(d);
        return new Vector3();
    }

    protected override int calcK(char d, ref Vector3 pos)
    {
        int k = 0;
        if (!flipped)
            k = 1;
        flipped = !flipped;
        return k;
    }
    protected override Vector3 calcWorldPosDiff(char d)
    {
        Vector3 posDiff = new Vector3();
        switch (d)
        {
            case 'N': posDiff.Set(0, 0, 32.5f); break;
            case 'E': posDiff.Set(32.5f, 0, 0); break;
            case 'S': posDiff.Set(0, 0, -32.5f); break;
            case 'W': posDiff.Set(-32.5f, 0, 0); break;
        }
        return posDiff;
    }

    void movePilar(char d)
    {
        Pilar pilar = FindObjectOfType<Pilar>();
        char r = '0';
        byte p = 0;

        if (pilar.trueUnknown)
        {
            System.Random rnd = new System.Random();
            if (rnd.Next(0, 2) == 1)
                r = 'N';
            else
                r = 'E';
        }

        if (d == 'N' || d == 'S')
        {
            if (!pilar.unknown)
            {
                if (pilar.direction == 'N')
                    r = 'S';
                else if (pilar.direction == 'S')
                    r = 'N';
            }

            if (pilar.transform.localPosition == pilar.posNE)
                p = 1;
            else if (pilar.transform.localPosition == pilar.posSE)
                p = 0;
            else if (pilar.transform.localPosition == pilar.posNW)
                p = 2;
            else if (pilar.transform.localPosition == pilar.posSW)
                p = 3;
        }
        else if (d == 'W' || d == 'E')
        {
            if (!pilar.unknown)
            {
                if (pilar.direction == 'E')
                    r = 'W';
                else if (pilar.direction == 'W')
                    r = 'E';
            }


            if (pilar.transform.localPosition == pilar.posNE)
                p = 3;
            else if (pilar.transform.localPosition == pilar.posSE)
                p = 2;
            else if (pilar.transform.localPosition == pilar.posNW)
                p = 0;
            else if (pilar.transform.localPosition == pilar.posSW)
                p = 1;
        }
        pilar.moveAndRotate(p, r);
    }

    /*void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
            SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    /*
    protected override void enteredTheMirror(char d)
    {
        if (d == 'N')
        {
            if (!isSetUp_W)
            {
                t = 0;
                byte p = 0;
                char r = '0';

                player.GetComponent<PlayerController>().anim.SetBool("IsEnteringMirror", false);

                GameObject original = GameObject.Find("Prologue_2");
                Vector3 newPos = original.transform.localPosition;
                newPos.z += 32.5f;
                GameObject clone = (GameObject)Instantiate(original, world.transform);
                clone.transform.localPosition = newPos;

                Pilar pilar = FindObjectOfType<Pilar>();
                if (pilar.trueUnknown)
                {
                    System.Random rnd = new System.Random();
                    if (rnd.Next(0, 2) == 1)
                        r = 'N';
                    else
                        r = 'E';
                }
                else if (!pilar.unknown)
                {
                    if (pilar.direction == 'N')
                        r = 'S';
                    else if (pilar.direction == 'S')
                        r = 'N';
                }
                

                if (pilar.transform.localPosition == pilar.posNE)
                    p = 1;
                else if (pilar.transform.localPosition == pilar.posSE)
                    p = 0;
                else if (pilar.transform.localPosition == pilar.posNW)
                    p = 2;
                else if (pilar.transform.localPosition == pilar.posSW)
                    p = 3;
                

                pilar.moveAndRotate(p, r);

                startPos_W = endPos_W = world.transform.position;
                endPos_W.z -= 32.5f;
                isSetUp_W = true;
            }

            else
            {
                if (world.transform.position != endPos_W)
                {
                    t += Time.deltaTime / 0.5f;
                    world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
                }

                else
                {
                    player.GetComponent<Rigidbody>().isKinematic = false;
                    player.GetComponent<PlayerController>().isControllable = true;

                    //clean up
                    Destroy(GameObject.Find("Prologue_2"));
                    GameObject.Find("Prologue_2(Clone)").name = "Prologue_2";
                    isSetUp_P = isSetUp_W = isAnimationPlayed = willEnterMirror = false;
                    Info = null;
                    mirror = null;
                }
            }
        }

        if (d == 'E')
        {
            if (!isSetUp_W)
            {
                t = 0;
                byte p = 0;
                char r = '0';

                player.GetComponent<PlayerController>().anim.SetBool("IsEnteringMirror", false);

                GameObject original = GameObject.Find("Prologue_2");
                Vector3 newPos = original.transform.localPosition;
                newPos.x += 32.5f;
                GameObject clone = (GameObject)Instantiate(original, world.transform);
                clone.transform.localPosition = newPos;

                Pilar pilar = FindObjectOfType<Pilar>();
                if (pilar.trueUnknown)
                {
                    System.Random rnd = new System.Random();
                    if (rnd.Next(0, 2) == 1)
                        r = 'N';
                    else
                        r = 'E';
                }
                else if (!pilar.unknown)
                {
                    if (pilar.direction == 'E')
                        r = 'W';
                    else if (pilar.direction == 'W')
                        r = 'E';
                }
             

                if (pilar.transform.localPosition == pilar.posNE)
                    p = 3;
                else if (pilar.transform.localPosition == pilar.posSE)
                    p = 2;
                else if (pilar.transform.localPosition == pilar.posNW)
                    p = 0;
                else if (pilar.transform.localPosition == pilar.posSW)
                    p = 1;

                pilar.moveAndRotate(p, r);

                startPos_W = endPos_W = world.transform.position;
                endPos_W.x -= 32.5f;
                isSetUp_W = true;
            }

            else
            {
                if (world.transform.position != endPos_W)
                {
                    t += Time.deltaTime / 0.5f;
                    world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
                }

                else
                {
                    player.GetComponent<Rigidbody>().isKinematic = false;
                    player.GetComponent<PlayerController>().isControllable = true;

                    //clean up
                    Destroy(GameObject.Find("Prologue_2"));
                    GameObject.Find("Prologue_2(Clone)").name = "Prologue_2";
                    isSetUp_P = isSetUp_W = isAnimationPlayed = willEnterMirror = false;
                    Info = null;
                    mirror = null;
                }
            }
        }

        if (d == 'S')
        {
            if (!isSetUp_W)
            {
                t = 0;
                byte p = 0;
                char r = '0';

                player.GetComponent<PlayerController>().anim.SetBool("IsEnteringMirror", false);

                GameObject original = GameObject.Find("Prologue_2");
                Vector3 newPos = original.transform.localPosition;
                newPos.z -= 32.5f;
                GameObject clone = (GameObject)Instantiate(original, world.transform);
                clone.transform.localPosition = newPos;

                Pilar pilar = FindObjectOfType<Pilar>();
                if (pilar.trueUnknown)
                {
                    System.Random rnd = new System.Random();
                    if (rnd.Next(0, 2) == 1)
                        r = 'N';
                    else
                        r = 'E';
                }
                else if (!pilar.unknown)
                {
                    if (pilar.direction == 'N')
                        r = 'S';
                    else if (pilar.direction == 'S')
                        r = 'N';
                }
               


                if (pilar.transform.localPosition == pilar.posNE)
                    p = 1;
                else if (pilar.transform.localPosition == pilar.posSE)
                    p = 0;
                else if (pilar.transform.localPosition == pilar.posNW)
                    p = 2;
                else if (pilar.transform.localPosition == pilar.posSW)
                    p = 3;

                pilar.moveAndRotate(p, r);

                startPos_W = endPos_W = world.transform.position;
                endPos_W.z += 32.5f;
                isSetUp_W = true;
            }

            else
            {
                if (world.transform.position != endPos_W)
                {
                    t += Time.deltaTime / 0.5f;
                    world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
                }

                else
                {
                    player.GetComponent<Rigidbody>().isKinematic = false;
                    player.GetComponent<PlayerController>().isControllable = true;

                    //clean up
                    Destroy(GameObject.Find("Prologue_2"));
                    GameObject.Find("Prologue_2(Clone)").name = "Prologue_2";
                    isSetUp_P = isSetUp_W = isAnimationPlayed = willEnterMirror = false;
                    Info = null;
                    mirror = null;
                }
            }
        }

        if (d == 'W')
        {
            if (!isSetUp_W)
            {
                t = 0;
                byte p = 0;
                char r = '0';

                player.GetComponent<PlayerController>().anim.SetBool("IsEnteringMirror", false);

                GameObject original = GameObject.Find("Prologue_2");
                Vector3 newPos = original.transform.localPosition;
                newPos.x -= 32.5f;
                GameObject clone = (GameObject)Instantiate(original, world.transform);
                clone.transform.localPosition = newPos;

                Pilar pilar = FindObjectOfType<Pilar>();
                if (pilar.trueUnknown)
                {
                    System.Random rnd = new System.Random();
                    if (rnd.Next(0, 2) == 1)
                        r = 'N';
                    else
                        r = 'E';
                }
                else if (!pilar.unknown)
                {
                    if (pilar.direction == 'E')
                        r = 'W';
                    else if (pilar.direction == 'W')
                        r = 'E';
                }
          


                if (pilar.transform.localPosition == pilar.posNE)
                    p = 3;
                else if (pilar.transform.localPosition == pilar.posSE)
                    p = 2;
                else if (pilar.transform.localPosition == pilar.posNW)
                    p = 0;
                else if (pilar.transform.localPosition == pilar.posSW)
                    p = 1;

                pilar.moveAndRotate(p, r);

                startPos_W = endPos_W = world.transform.position;
                endPos_W.x += 32.5f;
                isSetUp_W = true;
            }

            else
            {
                if (world.transform.position != endPos_W)
                {
                    t += Time.deltaTime / 0.5f;
                    world.transform.position = Vector3.Lerp(startPos_W, endPos_W, t);
                }

                else
                {
                    player.GetComponent<Rigidbody>().isKinematic = false;
                    player.GetComponent<PlayerController>().isControllable = true;

                    //clean up
                    Destroy(GameObject.Find("Prologue_2"));
                    GameObject.Find("Prologue_2(Clone)").name = "Prologue_2";
                    isSetUp_P = isSetUp_W = isAnimationPlayed = willEnterMirror = false;
                    Info = null;
                    mirror = null;
                }
            }
        }
    }*/
   
}
