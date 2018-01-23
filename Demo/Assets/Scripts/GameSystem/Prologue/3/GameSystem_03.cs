using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;


 public class GameSystem_03 : GameSystem {
    GameObject stage;
    Animator stageAnim;
    //   for mirror         | for camera
    bool isSetup_C, turnUp, turnLeft, turnForward2, turnL, oneTime;
    public bool isTurned, turnRight, turnForward, turnLeft2;
    //Vector3 startPos_C, endPos_C, currentPos_C;
    Vector3 startRot_C, endRot_C;
    float a;
    //char old_d;
    //private GameObject sub;
    //private Mirror mirror;
    CameraForPrologue Camera;

    BoxCollider triggerFC, triggerUP;

    //chest
    GameObject notis, sub2;

    //UI
    public bool doorSwitch, resetButton, goal;
	protected override void Start () {
        base.Start();
        stage = GameObject.Find(stageName);
        stageAnim = stage.GetComponent<Animator>();
        isTriggered = isSetup_C = isTurned = turnLeft = turnRight = turnForward = turnForward2 = turnLeft2 = turnL = false;
        //Camera = FindObjectOfType<CameraForPrologue>();

        //triggerFC = GameObject.Find("3-3 (FC)").GetComponent<BoxCollider>();
        //triggerUP = GameObject.Find("1-2 (C)(U)").GetComponent<BoxCollider>();

        playerScript.anim.SetBool("Start", true);
        playerScript.anim.SetBool("Climbing", true);
    }
	
	// Update is called once per frame
	protected override void Update () {
        //base.Update();
        if (Input.GetKeyDown("o"))
            stageAnim.SetBool("Gate", !stageAnim.GetBool("Gate"));
        else if (Input.GetKeyDown("r"))
            SceneManager.LoadScene(2, LoadSceneMode.Single);

        /*UI 
        if (sub2 == null)
        {
            if (playerScript.isCollidingWithMirror)
            {
                if (sub != null)
                    Destroy(sub);
                sub = UI.subtitle("Press Space To Enter");
            }
            else if (playerScript.inLadderRange && !playerScript.isClimbing)
            {
                if (sub == null)
                    sub = UI.subtitle("Press Space To Climb");
            }
            else if (playerScript.isGrabbing)
            {
                if (sub != null)
                    Destroy(sub);
                sub = UI.subtitle("Press Space To Release");
            }

            else if (playerScript.inObjectRange && playerScript.gloves)
            {
                if (sub != null)
                    Destroy(sub);
                sub = UI.subtitle("Press Space To Grabb");
            }
            else if (playerScript.inChestRange)
            {
                if (sub == null)
                    sub = UI.subtitle("Press Space To Open");
            }
            else if (doorSwitch)
            {
                if (sub == null)
                    sub = UI.subtitle("Gate Switch");
            }
            else if (resetButton)
            {
                if (sub == null)
                    sub = UI.subtitle("Object Position Reset Button");
            }
            else if (goal)
            {
                if (sub == null)
                    sub = UI.subtitle("Congratulations! You have reached the end of this demo!");
            }
            else if (sub != null)
                Destroy(sub);
        }*/
        if (playerScript.willEnterTheMirror)
        {
            if (!isSetUp_P)
            {
                startPos_P = endPos_P = player.transform.localPosition;
                playerRigidbody.isKinematic = true;
                playerScript.isControllable = false;


                mirror = playerScript.Info.gameObject.GetComponent<Mirror>();
                isSetUp_P = true;
            }

            if (base.hasEnteredTheMirror(mirror.direction))
            {
                afterEnteredTheMirror(mirror.sector);
            }
        }
        else if (playerScript.isClimbing)
            triggerUP.enabled = true;
        else if (playerScript.chestID != '\0')
        {
            if (notis != null)
                Destroy(notis);
            if (sub != null)
                Destroy(sub);
            /*switch (playerScript.chestID)
            {
                case 'T':
                    playerScript.inChestRange = false;
                    notis = UI.notice("Obtained <Torch>!");
                    //sub2 = UI.subtitle("This item will give you a bit more vision in the darkness");
                    Destroy(notis, 5f);
                    //Destroy(sub2, 5f);
                    break;
                case 'G':
                    playerScript.inChestRange = false;
                    notis = UI.notice("Obtained <Power Gloves>!");
                    sub2 = UI.subtitle("With this item you can move small objects\nand the object will no longer be attached to the ground");
                    Destroy(notis, 5f);
                    Destroy(sub2, 5f);
                    break;
                case 'C':
                    playerScript.inChestRange = false;
                    notis = UI.notice("Obtained <Secret code 1>!");
                    sub2 = UI.subtitle("8260");
                    Destroy(notis, 5f);
                    Destroy(sub2, 5f);
                    break;
                case 'c':
                    playerScript.inChestRange = false;
                    notis = UI.notice("Obtained <Cheat code>!");
                    sub2 = UI.subtitle("Press O to switch gates");
                    Destroy(notis, 5f);
                    Destroy(sub2, 5f);
                    break;
            }*/
            playerScript.chestID = '\0';
        }


        if (isSetup_C || isTriggered)
        {
            //if (!isTriggered)
            //    changeCameraPos(true);
            //else
            //{
            //if (!(triggerID == 'L' || triggerID == 'R' || Camera.returnCurrentRotIndex() == 1 || Camera.returnCurrentRotIndex() == 2))
            //changeCameraPos(false);
            //changeCameraRot();
            //else
            //{
            //if (Camera.returnCurrentRotIndex() == 1 || Camera.returnCurrentRotIndex() == 2)
            //    changeCameraRot();
            //changeCameraPos(true);

            switch (triggerID)
            {
                case 'G':
                    stageAnim.SetBool("Gate", !stageAnim.GetBool("Gate"));
                    isTriggered = false;
                    triggerID = '\0';
                    break;
                case 'U':
                    if (!turnUp)
                    {
                        changeCameraPos(new Vector3(0, 18f, 0), ref turnUp);
                        playerScript.ClimbUp(Time.deltaTime / 0.3f);
                    }
                    else
                    {
                        changeCameraPos(new Vector3(0, -18f, 0), ref turnUp);
                        playerScript.ClimbUp((Time.deltaTime / 0.3f) * -1f);
                    }
                    break;
                case 'W':
                    if (!turnLeft)
                        changeCameraPos(new Vector3(-11.5f, 0, 0), ref turnLeft);

                    else
                        changeCameraPos(new Vector3(11.5f, 0, 0), ref turnLeft);
                    break;
                /*case 'F':
                    if (!turnForward)
                        changeCameraPos(new Vector3(0, 0, 12f), ref turnForward);
                    else
                        changeCameraPos(new Vector3(0, 0, -12f), ref turnForward);
                    break;*/
                case 'F':
                    if (!turnForward)
                        changeCameraPos(new Vector3(0, 0, 8f), ref turnForward);
                    else
                        changeCameraPos(new Vector3(0, 0, -8f), ref turnForward);
                    break;
                case 'T':
                    if (!turnForward2)
                        changeCameraPos(new Vector3(0, 0, 13f), ref turnForward2);
                    else
                        changeCameraPos(new Vector3(0, 0, -13f), ref turnForward2);
                    break;
                case 'E':
                    if (!turnRight)
                        changeCameraPos(new Vector3(10f, 0, 0), ref turnRight);
                    else
                        changeCameraPos(new Vector3(-10f, 0, 0), ref turnRight);
                    break;
                case 'C':
                    if (!turnLeft2)
                        changeCameraPos(new Vector3(-28f, 0, 0), ref turnLeft2);
                    else
                        changeCameraPos(new Vector3(28f, 0, 0), ref turnLeft2);
                    break;
                case 'L':
                    //changeCameraPos(2);
                    changeCameraPos(new Vector3(0, -29f, 7f), ref turnL);
                    turnForward = true;
                    break;
                case 'M':
                    resetObjects();
                    isTriggered = false;
                    triggerID = '\0';
                    break;
                case 'Z':
                    if (!oneTime)
                    {
                        GameObject.Find("Hint").GetComponent<Light>().enabled = true;
                        if (sub2 == null)
                        {
                            //sub2 = UI.subtitle("Spot Light On");
                            Destroy(sub2, 5f);
                        }

                        oneTime = true;
                        isTriggered = false;
                        triggerID = '\0';
                    }
                    break;
                default:
                    changeCameraPos();
                    break;

            }
            /*
            if (triggerID == 'G')
            {
                world.GetComponent<Animator>().SetBool("Gate", !world.GetComponent<Animator>().GetBool("Gate"));
                isTriggered = false;
                triggerID = '\0';
            }
            else if (triggerID == 'A')
            {
                if (!left)
                    changeCameraPos(new Vector3(-11.5f, 0, 0), ref left);

                else
                    changeCameraPos(new Vector3(11.5f, 0, 0), ref left);

            }
            else if (triggerID == 'B')
            {
                if (!forward)
                    changeCameraPos(new Vector3(0, 0, 8f), ref forward);

                else
                    changeCameraPos(new Vector3(0, 0, -8f), ref forward);

            }
            else if (triggerID == 'V' && left)
            {
                if (!left_forward)
                    changeCameraPos(new Vector3(0, 0, 14f), ref left_forward);
                else
                    changeCameraPos(new Vector3(0, 0, -14f), ref left_forward);
            }
            else if (triggerID == 'L')
                changeCameraPos(2);
            else
                changeCameraPos();*/    
            //}    //currentPos_C = camera.returnCurrentPos();

            //}
        }


	}

    void changeCameraPos()
    {
        if (!isSetup_C)
        {
            startPos_C = Camera.returnCurrentPos();
            endPos_C = Camera.returnNewPos(triggerID);
            playerScript.isControllable = false;
            a = 0;
            isSetup_C = true;
        }
        else if (Camera.returnCurrentPos() != endPos_C)
        {   a += Time.deltaTime / 0.5f;
            Camera.gameObject.transform.position = Vector3.Lerp(startPos_C, endPos_C, a);
            playerScript.MoveForward(Time.deltaTime / 0.2f);
        	
        }
        else
        {
            if (playerScript.isGrabbing)
            {
                if (playerScript.grabbableObject.gameObject.tag == "Mirror")
                {
                    char newSector = '0';
                    int p = Camera.returnCurrentPosIndex();
                    if (!isTurned)
                    {
                        switch (p)
                        {
                            case 0:
                                newSector = '1';
                                break;
                            case 1:
                                newSector = '2';
                                break;
                            case 2:
                            case 3:
                                newSector = '3';
                                break;
                            case 6:
                                newSector = '4';
                                break;
                            case 7:
                                newSector = '5';
                                break;
                            case 8:
                                newSector = '6';
                                break;
                        }
                    }
                    else
                    {
                        switch (p)
                        {
                            case 0:
                                newSector = '3';
                                break;
                            case 1:
                                newSector = '4';
                                break;
                            case 4:
                                newSector = '1';
                                break;
                            case 6:
                                newSector = '2';
                                break;

                        }
                    }
                    Mirror m = playerScript.objectRigidbody.gameObject.GetComponent<Mirror>();
                    if (m != null)
                        m.sector = newSector;
                }
            }
            playerScript.isControllable = true;
            isSetup_C = false;
            triggerID = '\0';
            //isTriggered = false;
        }
    }
    void changeCameraPos(Vector3 newPos, ref bool toggle)
    {
        if (!isSetup_C)
        {
            startPos_C = endPos_C = Camera.returnCurrentPos();
            endPos_C += newPos;
            playerScript.isControllable = false;
            a = 0;
            isSetup_C = true;
        }
        else if (Camera.returnCurrentPos() != endPos_C)
        {
            a += Time.deltaTime / 0.5f;
            Camera.gameObject.transform.position = Vector3.Lerp(startPos_C, endPos_C, a);
            playerScript.MoveForward(Time.deltaTime / 0.2f);
        }
        else
        {
            isSetup_C = false;
            playerScript.isControllable = true;
            toggle = !toggle;
            triggerID = '\0';
        }
    }

    void changeCameraPos(int posIndex)
    {
        Camera.gameObject.transform.position = Camera.Positions[posIndex];
    }

    /*
    Vector3 calcCameraPosDiff()
    {
        Vector3 posDiff = new Vector3();
        switch (triggerID)
        {
            case 'L': posDiff.Set(30f, 0, 0); break;
            case 'R': posDiff.Set(-30f, 0, 0); break;
        }
        return posDiff;
    }
    */

    /*void changeCameraPos(bool changeRot)
    {
        if (!isSetup_C)
        {
            startPos_C = Camera.returnCurrentPos();
            //endPos_C += calcCameraPosDiff();
            endPos_C = Camera.returnNewPos(triggerID);

            if (changeRot)
            {
                startRot_C = Camera.returnCurrentRot();
                endRot_C = Camera.returnNewRot(triggerID);
            }

            a = 0;
            isSetup_C = true;
        }
        else if (Camera.returnCurrentPos() != endPos_C || Camera.returnCurrentRot() != endRot_C)
        {
            a += Time.deltaTime / 0.5f;
            Camera.gameObject.transform.position = Vector3.Lerp(startPos_C, endPos_C, a);
            if (changeRot)
            {
                Quaternion q = Quaternion.identity;
                q.eulerAngles = Vector3.Lerp(startRot_C, endRot_C, a);
                Camera.gameObject.transform.rotation = q;
            }
        }
        /*else if(cancel && Camera.main.transform.position != startPos_C)
        {
            a += Time.deltaTime / 1f;
            Camera.main.transform.position = Vector3.Lerp(currentPos_C, startPos_C, a);
        }
        else
        {
            print("YO");
            isSetup_C = false;
            triggerID = '\0';
            //isTriggered = false;
        }
    }*/

    /*void changeCameraRot()
    {
        if (!isSetup_C)
        {
            startPos_C = Camera.returnCurrentRot();
            endPos_C = Camera.returnNewRot(triggerID);

            a = 0;
            isSetup_C = true;
        }
        else if(Camera.returnCurrentRot() != endPos_C)
        {
            a += Time.deltaTime / 0.5f;
            Quaternion q = Quaternion.identity;
            q.eulerAngles = Vector3.Lerp(startPos_C, endPos_C, a);
            Camera.gameObject.transform.rotation = q;
        }
        else
        {
            isSetup_C = false;
            triggerID = '\0';
        }
    }*/
    protected override Vector3 calcCamPosDiff(char d)
    {
        throw new NotImplementedException();
    }
    protected override Vector3 calcPlayerPosDiff(char d)
    {
        //Vector3 posDiff = new Vector3();
        //if (d == 'U')
        //    posDiff.Set(0, -15.5f, 0);
        
        if (isTurned)
            return new Vector3(0, 8f, 0);
        
        return new Vector3(0, -8f, 0);
    }
    protected override Vector3 calcWorldPosDiff(char d)
    {
        return new Vector3();
    }
    Vector3 calcWorldPosDiff(bool turned)
    {
        if (!turned)
            return new Vector3(0, -20f, -5f);
        return new Vector3(0, 0, 0);
    }
    protected override Vector3 calcWorldRotDiff(char d)
    {
        //Vector3 rotDiff = new Vector3();
        //if (d == 'U')
        //    rotDiff.Set(180f, 0, 0);
        return new Vector3(180f, 0, 0);
    }
    protected override int calcK(char d, ref Vector3 pos)
    {
        return 0;
    }

    protected override void afterEnteredTheMirror(char sector)
    {
        //GameObject stage = GameObject.Find(stageName);
        //Vector3 newPos = world.transform.position;
        //Vector3 posDiff = calcWorldPosDiff(d);
        Vector3 newPos = calcWorldPosDiff(isTurned);
        /*if (!isTurned)
        {
            newPos = calcWorldPosDiff(s);
        }
        else
            newPos = ;*/


        //Destroy(world);
        //world = (GameObject)Instantiate(world);
        world.transform.position = newPos;
        world.transform.Rotate(calcWorldRotDiff(sector));

        //player.transform.position += new Vector3(0, 36f, 0);
        player.transform.Rotate(new Vector3(180f, 0, 0));
        if (playerScript.objectRigidbody != null)
        {
            //playerScript.objectRigidbody.gameObject.transform.Rotate(new Vector3(180f, 0, 0));
            //playerScript.grabbableObject.GetComponent<grabbableObject>().on = true;
            foreach (grabbableObject ob in FindObjectsOfType<grabbableObject>()) {
                ob.gameObject.transform.Rotate(new Vector3(180f, 0, 0));
            }
        }

        if (!isTurned)
            player.transform.localPosition += new Vector3(0, 10f, 0);
        else
            player.transform.localPosition -= new Vector3(0, 10f, 0);

        if (turnL)
            triggerFC.enabled = false;
        else
            triggerFC.enabled = true;
        triggerUP.enabled = false;

        playerRigidbody.isKinematic = false;
        playerScript.isControllable = true;
        playerScript.willEnterTheMirror = false;
        
        int newP = 0;
        switch (sector)
        {    
            case '1': newP = (isTurned) ? 0 : 4; break;
            case '2': newP = (isTurned) ? 1 : 6; break;
            case '3': newP = (isTurned) ? 5 : 0; break;
            case '4': newP = (isTurned) ? 6 : 1; break;
            case '5':
                newP = 1;
                Quaternion r = Quaternion.identity;
                r.eulerAngles = Camera.Rotations[0];
                Camera.gameObject.transform.localRotation = r;
                break;
            case '6': //newP = (isTurned) ? 8 : 9; break;
                if (isTurned)
                {
                    newP = 8;
                    Quaternion r1 = Quaternion.identity;
                    r1.eulerAngles = Camera.Rotations[0];
                    Camera.gameObject.transform.localRotation = r1;
                    playerScript.reverseControll = false;
                }
                else
                {
                    newP = 9;
                    Quaternion r2 = Quaternion.identity;
                    r2.eulerAngles = Camera.Rotations[2];
                    Camera.gameObject.transform.localRotation = r2;
                    playerScript.reverseControll = true;
                }
                break;

        }
        changeCameraPos(newP);
        /*if (d == '3')
        {
            /*if (!isTurned)
            {
                changeCameraPos(4);
                //player.transform.position -= posDiff;
            }
            else
                changeCameraPos(4);
            
            changeCameraPos(4);    
        }*/

        //GameObject.Find(stageName + "(Clone)").name = stageName;
        //world.GetComponent<Animator>().enabled = true;   
        isSetUp_P = turnLeft = turnRight = turnForward = turnForward2 = turnL = false; 
        isTurned = !isTurned;
    }

    void resetObjects()
    {
        grabbableObject[] gObjects = FindObjectsOfType<grabbableObject>();
        foreach (grabbableObject ob in gObjects)
        {
            GameObject clone = (GameObject)Instantiate(ob.gameObject, ob.originalParent);
            clone.transform.localPosition = ob.defaultPosition;
            Destroy(ob.gameObject);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
            SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}

