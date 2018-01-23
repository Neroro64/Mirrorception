using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RE_GameSystem_3 : GameSystem {
    public bool doorSwitch, resetButton, hint, isTurned;
    public AudioClip switchSound;
    AudioSource aSource;
    GameObject stage;
    Animator stageAnim;

    int stringCounter;
    GameObject info;
    bool once;
    string[] glovesInfo = { "发现【巨人手环】!", "拥有这个手环你将可以把原本钉在地上的\n物件拔起来并拿走" };//{ "Obtained <Power Bracelet>!", "With this item you will be able to rip objects\n from their nailed position", "and move them freely" };//
    string[] cheat = { "发现【作弊码】! ", "按下N键可以控制门的开关" };//{ "Obtained <Cheat code>!", "You can press N to switch gates" };//
    string[] hintText = { "提示", "找到并按下最低端的按钮", "这会帮助你抵达你想去的地方" };//{ "Hint", "Find the button at the bottom", "It will help you to get closer to the goal" };//
    protected override void Start () {
        base.Start();
        stage = GameObject.Find(stageName);
        stageAnim = stage.GetComponent<Animator>();
        stringCounter = 0;
        aSource = GetComponent<AudioSource>();

        playerScript.anim.SetBool("Start", true);
        playerScript.anim.SetBool("Climbing", true);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("n"))
            stageAnim.SetBool("Gate", !stageAnim.GetBool("Gate"));
        else if (Input.GetKeyDown("r"))
            SceneManager.LoadScene(2, LoadSceneMode.Single);

        if (playerScript.isCollidingWithMirror)
        {
            if (!once) { 
                if (sub != null)
                    Destroy(sub);

                sub = UI.subtitle("按下空格键进入", "UI Display");//UI.subtitle("PRESS SPACE TO ENTER", "UI Display"); //
                once = true;
            }
        }
        else if (playerScript.inLadderRange && !playerScript.isClimbing)
        {
            if (sub == null)
                sub = UI.subtitle("按下空格键开始爬", "UI Display");//UI.subtitle("PRESS SPACE TO CLIMB", "UI Display"); //
        }
        else if (playerScript.isGrabbing)
        {
            if (!once)
            {
                if (sub != null)
                    Destroy(sub);

                sub = UI.subtitle("按下空格键放开", "UI Display");//UI.subtitle("PRESS SPACE TO RELEASE", "UI Display"); //
                once = true;
            }
        }
        else if (playerScript.inObjectRange && playerScript.gloves)
        {
            if (sub == null)
                sub = UI.subtitle("按下空格键抓起", "UI Display");//UI.subtitle("PRESS SPACE TO GRABB", "UI Display"); //
        }
        else if (playerScript.inChestRange)
        {
            if (sub == null)
                sub = UI.subtitle("按下空格键打开", "UI Display");//UI.subtitle("PRESS SPACE TO OPEN", "UI Display"); //
        }
        else if (doorSwitch)
        {
            if (sub == null)
                sub = UI.subtitle("门的开关", "UI Display");//UI.subtitle("Gate Switch", "UI Display"); //
        }
        else if (resetButton)
        {
            if (sub == null)
                sub = UI.subtitle("重置物件位置的开关", "UI Display");//UI.subtitle("Reset", "UI Display"); //
        }
        else if (hint)
        {
            if (sub == null)
                sub = UI.subtitle("请狠狠地踩我！", "UI Display");//UI.subtitle("Please stamp on me!", "UI Display"); //
        }
        else if (sub != null)
        {
            UI.DestroyTexts(sub, "UI Display");
            once = false;
        }
        if (isTriggered)
        {
            switch (triggerID)
            {
                case 'G':
                    aSource.PlayOneShot(switchSound, 0.5f);
                    stageAnim.SetBool("Gate", !stageAnim.GetBool("Gate"));
                    isTriggered = false;
                    triggerID = '\0';
                    break;
                case 'R':
                    resetObjects();
                    isTriggered = false;
                    triggerID = '\0';
                    break;
                case 'H':
                    aSource.PlayOneShot(switchSound, 0.5f);
                    GameObject.Find("Hint").GetComponent<Light>().enabled = true;
                   
                    if (sub != null)
                        UI.DestroyTexts(sub, "UI Display");
                    hint = false;
                    info = UI.infoText("你点亮了灯", "Info");//UI.infoText("(Light) has been turned on!", "Info"); //

                    Destroy(info, 3f);
                    Destroy(GameObject.Find("Hint Button"), 3f);
                    isTriggered = false;
                    triggerID = '\0';
                    break;
                case 'A':
                    //cam.recalcMinMax(calcCamPosDiff('A'));
                    adjuststCam('A');

                    isTriggered = false;
                    triggerID = '\0';
                    break;
            }   
        }
      

        if (playerScript.chestID != '\0')
        {
            switch (playerScript.chestID)
            {
                case 'T':
                    playerScript.inChestRange = false;
                    if (info == null){
                        info = UI.infoText("发现【火把】！", "Info");//UI.infoText("Obtained <Torch>!", "Info"); //
                        Destroy(info, 3f);
                        playerScript.chestID = '\0';
                    }
                    break;
                case 'G':
                    playerScript.inChestRange = false;
                    if (info == null) {
                        info = UI.infoText(glovesInfo[stringCounter++], "Info");
                        Destroy(info, 4f);
                    }
                    if (stringCounter == glovesInfo.Length)
                    {
                        stringCounter = 0;
                        playerScript.chestID = '\0';
                    }
                    break;
                case 'C':
                    playerScript.inChestRange = false;
                    if (info == null)
                    {
                        info = UI.infoText(cheat[stringCounter++], "Info");
                        Destroy(info, 3f);
                    }
                    if (stringCounter == cheat.Length) {
                        stringCounter = 0;
                        playerScript.chestID = '\0';
                    }
                    break;
                case 'H':
                    playerScript.inChestRange = false;
                    if (info == null)
                    {
                        info = UI.infoText(hintText[stringCounter++], "Info");
                        Destroy(info, 3f);
                    }
                    if (stringCounter == hintText.Length)
                    {
                        stringCounter = 0;
                        playerScript.chestID = '\0';
                    }
                    break;
            }
        }
        if (info == null)
            GameObject.Find("Info").GetComponent<Canvas>().enabled = false;//UI.DestroyTexts(info, "Info");
    }

    protected override Vector3 calcCamPosDiff(char d)
    {
        /*Vector3 posDiff;

        if (isTurned && d == 'A')
            posDiff = new Vector3(0, 0, -5f);
        else if (!isTurned && d == 'A')
            posDiff = new Vector3(0, 0, 5f);
        else if (isTurned)
            posDiff = new Vector3(-3f, 0, -3f);
        else
            posDiff = new Vector3(3f, 0, 3f);

        */
        return new Vector3();
        
    }

    void adjuststCam(char d)
    {
        if (d == 'A' && isTurned)
        {
            cam.Max = new Vector3(27f, 100f, 27f);
            cam.Min = new Vector3(-27f, -100f, -27f);
            cam.enabled = false;
            cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x, cam.gameObject.transform.position.y - 2.5f, -27f);
            cam.recalcOffset();
            cam.enabled = true;
        }
        else if (d == 'A' && !isTurned)
        {
            cam.Max = new Vector3(22f, 100f, 22f);
            cam.Min = new Vector3(-22f, -100f, -22f);
            cam.enabled = false;
            cam.gameObject.transform.position = new Vector3(cam.gameObject.transform.position.x, cam.gameObject.transform.position.y + 2.5f, 22f);
            cam.recalcOffset();
            cam.enabled = true;
        }

        else if (isTurned)
        {
            cam.Max = new Vector3(22f, 100f, 22f);
            cam.Min = new Vector3(-22f, -100f, -22f);
        }
        else if (!isTurned)
        {
            cam.Max = new Vector3(19f, 100f, 19f);
            cam.Min = new Vector3(-19f, -100f, -19f);
        }
            
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

    Vector3 calcWorldPosDiff()
    {
        if (!isTurned)
            return new Vector3(0, -20f, -5f);
        return new Vector3(0, 0, 0);
    }
    protected override Vector3 calcWorldRotDiff(char d)
    {
        return new Vector3(180f, 0, 0);
    }
    protected override int calcK(char d, ref Vector3 pos)
    {
        return 0;
    }

    protected override void afterEnteredTheMirror(char d)
    {
        //Vector3 newPos = calcWorldPosDiff();

        //world.transform.position = newPos;
        world.transform.Rotate(calcWorldRotDiff(d));

        player.transform.Rotate(new Vector3(180f, 0, 0));
        if (!isTurned)
            player.transform.localPosition += new Vector3(0, 10f, 0);
        else
            player.transform.localPosition -= new Vector3(0, 10f, 0);

        if (playerScript.objectRigidbody != null)
        {
            foreach (grabbableObject ob in FindObjectsOfType<grabbableObject>())
                ob.gameObject.transform.Rotate(new Vector3(180f, 0, 0));
            
        }

        //cam.recalcMinMax(calcCamPosDiff(d));
        adjuststCam(d);

        playerRigidbody.isKinematic = false;
        playerScript.isControllable = true;
        playerScript.willEnterTheMirror = false;
        isSetUp_P = false;
        isTurned = !isTurned;
    }

    void resetObjects()
    {
        aSource.PlayOneShot(switchSound, 0.5f);
        grabbableObject[] gObjects = FindObjectsOfType<grabbableObject>();
        foreach (grabbableObject ob in gObjects)
        {
            GameObject clone = (GameObject)Instantiate(ob.gameObject, ob.originalParent);
            clone.transform.localPosition = ob.defaultPosition;
            Destroy(ob.gameObject);
        }
    }
}
