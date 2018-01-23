using UnityEngine;
using System.Collections;

public abstract class GameSystem : MonoBehaviour {
    public string stageName;
    protected myCamera cam;
    // for entering mirror
    protected bool isSetUp_P, isSetUp_W, isAnimationPlayed; // might not need to public
    //public Collision Info;
    
    // World and its objects
    protected GameObject world;
    protected Mirror mirror;
    
    // Player and components
    protected GameObject player;
    protected PlayerController playerScript;
    protected Rigidbody playerRigidbody;
    
    float t;
    protected Vector3 startPos_P, endPos_P, startPos_C, endPos_C;

    //public bool startClimb, endClimb, cancelClimb;

    // for UI
    protected GameObject sub;
    // for proloue 3
    public bool isTriggered;
    public char triggerID;
    //public byte changeSector;
    // A methos to play the enter mirror animation

    //Background music
    //protected delegate void printOneLine(string s);
    protected IEnumerator playEnterMirrorAnimation()
    {
        //holder = player.GetComponent<Rigidbody>().constraints;
        //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        //player.GetComponent<PlayerController>().isControllable = false;
        isAnimationPlayed = true;
        playerScript.anim.SetBool("IsEnteringMirror", true);

        yield return new WaitForSeconds(1.3f);
        player.transform.localPosition = endPos_P;
        
    }
    protected virtual void Start () {
        world = GameObject.FindGameObjectWithTag("World");
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        cam = Camera.main.GetComponent<myCamera>();

        stageName = "Stage";
        // Add reset Text
        //UI.addResetText();
        
        //willEnterMirror = isSetUp_P = isSetUp_W = isAnimationPlayed = false;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
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

            if (isSetUp_W || hasEnteredTheMirror(mirror.direction))
            {
                afterEnteredTheMirror(mirror.direction);
            }
        }
        /*else if (startClimb)
            sClimb();
        else if (endClimb)
            eClimb();
        else if (cancelClimb)
            cClimb();*/
    }

    protected abstract Vector3 calcPlayerPosDiff(char d);
    protected abstract Vector3 calcWorldPosDiff(char d);
    protected abstract Vector3 calcWorldRotDiff(char d);
    //protected abstract Vector3 calcCameraṔosDiff(char d);
    protected abstract int calcK(char d, ref Vector3 pos);
    protected abstract Vector3 calcCamPosDiff(char d);
   
    protected virtual bool hasEnteredTheMirror(char d) {
        if (d == 'U') {
            if (endPos_P == startPos_P)
            {
                endPos_P += calcPlayerPosDiff(d);
                t = 0;
            }
            t += Time.deltaTime / 1f;
            player.transform.localPosition = Vector3.Lerp(startPos_P, endPos_P, t);
        }    
        else {
            if (endPos_P == startPos_P)
                endPos_P += calcPlayerPosDiff(d);
            if (!isAnimationPlayed)
                StartCoroutine(playEnterMirrorAnimation());
        }
        
        return (player.transform.localPosition == endPos_P);
    }
    
    protected virtual void afterEnteredTheMirror(char d){
        //GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (!isSetUp_W) {
            //GameObject stage = world.transform.FindChild(stageName).gameObject;
            //GameObject stage = transform.Find(stageName).gameObject;
            t = 0;
            
            Vector3 newPos = world.transform.position;
            Vector3 posDiff = calcWorldPosDiff(d); 
            newPos += posDiff;
        
            int k = calcK(d, ref newPos);
            playerScript.anim.SetBool("IsEnteringMirror", false);


            //GameObject clone = (GameObject) Instantiate(stage, world.transform);
            GameObject clone = (GameObject) Instantiate(world);
            clone.transform.position = newPos;
            clone.transform.Rotate(calcWorldRotDiff(d));
            //Quaternion newRot = Quaternion.identity;
            //newRot.eulerAngles = calcWorldRotDiff(d);
            //clone.transform.rotation = newRot;
            moveAbleObjects[] moveAbleObjects = FindObjectsOfType<moveAbleObjects>();
            foreach (moveAbleObjects ob in moveAbleObjects)
            {
                if (ob.transform.parent.name != stageName)
                    ob.move(k);
            }

            //startPos_W = endPos_W = world.transform.position;
            world = clone;
            cam.enabled = false;
            cam.recalcMinMax(posDiff);
            startPos_C =  cam.gameObject.transform.position;
            endPos_C = calcCamPosDiff(d);
            
            //endPos_P = cam.gameObject.transform.position + new Vector3(0.1f, 0, 0);
            isSetUp_W = true;
            
        }
        
        if (cam.gameObject.transform.position != endPos_C){
            t += Time.deltaTime / 0.5f;
            cam.gameObject.transform.position = Vector3.Lerp(startPos_C, endPos_C, t);
            /*cam.gameObject.transform.position = new Vector3(
            Mathf.Clamp(cam.gameObject.transform.position.x, cam.Min.x, cam.Max.x), 
            Mathf.Clamp(cam.gameObject.transform.position.y, cam.Min.y, cam.Max.y), 
            Mathf.Clamp(cam.gameObject.transform.position.z, cam.Min.z, cam.Max.z));

            endPos_P = cam.gameObject.transform.position;*/
        }
        
        else {
            //player.GetComponent<Rigidbody>().constraints = holder;
            playerRigidbody.isKinematic = false;
            playerScript.isControllable = true;
            playerScript.willEnterTheMirror = false;

            //Clean up
            Destroy(GameObject.Find(stageName));
            world.name = stageName;
            isSetUp_P = isSetUp_W = isAnimationPlayed = false;

            cam.enabled = true;
        }
    }
}


