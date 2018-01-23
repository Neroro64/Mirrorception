using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    // Movement
    public float speed;            
    public Animator anim;               
    Vector3 movement;                   
    Rigidbody playerRigidbody;
    public bool reverseControll;
    public bool freezeRotation, freezeVerticalMovement;

    //GameObject bTips;
    //GameObject gameSystem;

    // Interaction with mirror
    public bool isCollidingWithMirror, willEnterTheMirror;
    //public bool isTouchingMirror;
    public Collision Info;
    Collider col;
    // Interaction with grabbable objects
    public bool inObjectRange;
    public bool isGrabbing;
    public GameObject grabbableObject, objectHolder;
    public Rigidbody objectRigidbody;

    // Restriction
    public bool isControllable;

    //Interaction with ladder
    public bool inLadderRange, isClimbing, climbDone, cancelClimb; // test
    bool isSetup;
    float currentLerpTime, lerpTime;
    Vector3 startPos, endPos;

    //Interaction with chests
    public char chestID;
    public bool inChestRange;
    //Items
    public bool gloves;
    // For testing camera ray in prologue 3
    //public bool cameraRayTest;
    //Vector3 direction;
    //RaycastHit hitInfo;
    //Camera mainCamera;

    IEnumerator waitForSeconds(float s) // Might not work
    {
        yield return new WaitForSeconds(s);
    }
    bool V3Equal(Vector3 a, Vector3 b)
    {
        return (Vector3.SqrMagnitude(a - b) < 0.0001);
    }

    void Awake() {
        // Set up references.
        speed = 5f;
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        

        //gameSystem = GameObject.FindGameObjectWithTag("GameController");
        //bTips = null;
        //isCollidingWithMirror = isTouchingMirror = false;
        isControllable = true;
    }

    void Update()
    {
        if (isCollidingWithMirror && !isGrabbing)
        {
            if (Input.GetKeyDown("space"))
            {
                // gameSystem.GetComponent<GameSystem>().willEnterMirror = true;
                // gameSystem.GetComponent<GameSystem>().Info = Info;
                willEnterTheMirror = true;
            }
        }
        else if (gloves && inObjectRange && !isGrabbing && !anim.GetBool("IsWalking")) {
            if (Input.GetKeyDown("space"))
            {
                isGrabbing = true;
                speed = 3f;
                anim.SetBool("IsGrabbing", isGrabbing);
                //grabbableObject = col.gameObject;
                grabbableObject.transform.parent = this.transform;

                objectRigidbody = grabbableObject.GetComponent<Rigidbody>();
                objectRigidbody.isKinematic = true;
                objectRigidbody.useGravity = false;
               
            }
        }
        else if (inChestRange && !isGrabbing)
        {
            if (Input.GetKeyDown("space"))
            {
                Info.gameObject.GetComponent<TreasureChest>().Open();
                chestID = Info.gameObject.GetComponent<TreasureChest>().ID;
                if (chestID == 'T')
                    GetComponentInChildren<Light>().enabled = true;
                else if (chestID == 'G')
                    gloves = true;
            }
        }
        else if (isGrabbing && !inLadderRange){
            if (Input.GetKeyDown("space")){
                isGrabbing = false;
                speed = 5f;
                anim.SetBool("IsGrabbing", isGrabbing);
                objectRigidbody.gameObject.transform.parent = objectRigidbody.gameObject.GetComponent<grabbableObject>().originalParent;
                objectRigidbody.isKinematic = false;
                objectRigidbody.useGravity = true;
                
            }
        }
        if (inLadderRange && !isClimbing && !climbDone && !isGrabbing)
        {
            if (V3Equal(transform.rotation.eulerAngles, new Vector3(0, 0, 0)) ||
                V3Equal(transform.rotation.eulerAngles, new Vector3(0, 90f, 0)) ||
                V3Equal(transform.rotation.eulerAngles, new Vector3(0, 180f, 0)) ||
                V3Equal(transform.rotation.eulerAngles, new Vector3(0, 270f, 0)))
            {
                if (isSetup || Input.GetKeyDown("space"))
                    //gameSystem.GetComponent<GameSystem>().startClimb = true;
                    sClimb();
            }
        }

        else if (isClimbing && climbDone)
            //gameSystem.GetComponent<GameSystem>().endClimb = true;
            eClimb();
        

        else if (isClimbing && cancelClimb)
            //gameSystem.GetComponent<GameSystem>().cancelClimb = true;
            cClimb();
        

        //if (grabbableObject != null)
        //    mirrorRigidbody = grabbableObject.GetComponentInChildren<Rigidbody>();
       /* if (cameraRayTest)
        {
            direction = transform.position - Camera.main.transform.position;
            if (Physics.Raycast(Camera.main.transform.position, direction, out hitInfo, 500f))
            {
                Debug.Log(hitInfo.collider.name + ", " + hitInfo.collider.tag);
                Debug.DrawRay(Camera.main.transform.position, direction, Color.red, Mathf.Infinity, false);

                //if (hitInfo.collider.gameObject.tag != "Player")

            } 
        }*/
    }
    void FixedUpdate()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (isClimbing && isControllable)
            Climb(v);

        else if (isControllable)
        {
            if (!freezeVerticalMovement)
                Move(h, v);
            else
                Move(h, 0);
            if (!freezeRotation)
                Turning(h, v);
            //Animating(h, v);
        } 

       
        
    }
    
    public void MoveForward(float factor){
        playerRigidbody.MovePosition(transform.position + transform.forward * factor);
        anim.SetBool("IsWalking", true);
    }
    public void ClimbUp(float factor)
    {
        playerRigidbody.MovePosition(transform.position + new Vector3(0, 1f, 0) * factor);
        anim.SetBool("IsClimbing", true);
    }
    
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        if (reverseControll)
            movement *= -1;
        playerRigidbody.MovePosition(transform.position + movement);

        bool isWalking = v != 0 || h != 0;
        if (!isGrabbing)
            anim.SetBool("IsWalking", isWalking);
        else 
            anim.SetBool("IsGrabbWalking", isWalking);
    }

    void Turning(float h, float v)
    {
        Quaternion newRotation = Quaternion.identity;
        //float k = (reverseControll) ? -1f : 1f;
        if (!reverseControll)
        {
            if (h != 0f && v == 0)
            {
                if (h > 0)
                    newRotation.eulerAngles = new Vector3(0, 90f, 0);
                else
                    newRotation.eulerAngles = new Vector3(0, -90f, 0);
                playerRigidbody.MoveRotation(newRotation);
            }
            else if (v != 0f && h == 0)
            {
                if (v > 0)
                    newRotation.eulerAngles = new Vector3(0, 0, 0);
                else
                    newRotation.eulerAngles = new Vector3(0, 180f, 0);
                playerRigidbody.MoveRotation(newRotation);
            }
            else if (h != 0 && v != 0)
            {
                if (v > 0 && h > 0)
                    newRotation.eulerAngles = new Vector3(0, 45f, 0);
                else if (v > 0 && h < 0)
                    newRotation.eulerAngles = new Vector3(0, -45f, 0);
                else if (v < 0 && h > 0)
                    newRotation.eulerAngles = new Vector3(0, 135f, 0);
                else
                    newRotation.eulerAngles = new Vector3(0, -135f, 0);
                playerRigidbody.MoveRotation(newRotation);
            }
        }
        else
        {
            if(h != 0f && v == 0)
            {
                if (h > 0)
                    newRotation.eulerAngles = new Vector3(0, -90f, 0);
                else
                    newRotation.eulerAngles = new Vector3(0, 90f, 0);
                playerRigidbody.MoveRotation(newRotation);
            }
            else if (v != 0f && h == 0)
            {
                if (v > 0)
                    newRotation.eulerAngles = new Vector3(0, 180f, 0);
                else
                    newRotation.eulerAngles = new Vector3(0, 0, 0);
                playerRigidbody.MoveRotation(newRotation);
            }
            else if (h != 0 && v != 0)
            {
                if (v > 0 && h > 0)
                    newRotation.eulerAngles = new Vector3(0, -135f, 0);
                else if (v > 0 && h < 0)
                    newRotation.eulerAngles = new Vector3(0, 135f, 0);
                else if (v < 0 && h > 0)
                    newRotation.eulerAngles = new Vector3(0, -45f, 0);
                else
                    newRotation.eulerAngles = new Vector3(0, 45f, 0);
                playerRigidbody.MoveRotation(newRotation);
            }
        }
        

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        
        if (collisionInfo.gameObject.tag == "Mirror")
        {
            //isTouchingMirror = (collisionInfo.gameObject.GetComponent<Mirror>().direction != 'U');
            Info = collisionInfo;
            isCollidingWithMirror = true;
        }
        else if (collisionInfo.gameObject.tag == "Chest")
        {
            if (!collisionInfo.gameObject.GetComponent<TreasureChest>().isOpened)
                inChestRange = true;
            Info = collisionInfo;
        }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        
        if (collisionInfo.gameObject.tag == "Mirror")
        {
            //if (bTips != null)
            //    Destroy(bTips);
            Info = null;
            isCollidingWithMirror = false;
            //willEnterTheMirror = false;
        }
        else if (collisionInfo.gameObject.tag == "Chest")
        {
            inChestRange = false;
            Info = null;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (gloves && collider.gameObject.GetComponent<grabbableObject>() != null)
        {
            inObjectRange = true;
            grabbableObject = collider.gameObject;
        }

        else if (collider.gameObject.tag == "Mirror" && !inObjectRange)
            anim.SetBool("IsTouchingMirror", true);
       
        
        //else if (collider.gameObject.tag == "Trigger")
        //gameSystem.GetComponent<GameSystem_03>.changeSector = collider.gameObject.GetComponent<Sector>.s;
        /*else if (collider.gameObject.tag == "Grabbable"){
            inObjectRange = true;
            col = collider;
            //grabbableObject = collider.gameObject;
        }*/
        
        
    }

    void OnTriggerStay(Collider collider)
    {
         if (collider.gameObject.tag == "Ladder")
            inLadderRange = true;
    }

    void OnTriggerExit(Collider collider)
    {
        if (gloves && collider.gameObject.GetComponent<grabbableObject>() != null)
        {
            inObjectRange = false;
            grabbableObject = null;
        }
        else if (collider.gameObject.tag == "Mirror")
            anim.SetBool("IsTouchingMirror", false);
        else if (collider.gameObject.tag == "Ladder")
            inLadderRange = false;
      
        //else if (collider.gameObject.tag == "Grabbable")
        //    inObjectRange = false;

    }

    void Climb (float v)
    {
        bool c = (v != 0);
        anim.SetBool("IsClimbing", c);

        movement.Set(0, v, 0);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    public virtual void sClimb()
    {
        if (!isSetup)
        {
            startPos = endPos = transform.localPosition;
            if (transform.localRotation.eulerAngles.z > 1 || transform.localRotation.eulerAngles.z < -1)
                endPos.y -= 0.5f;
            else
                endPos.y += 0.5f;
            playerRigidbody.isKinematic = true;
            isControllable = false;
            anim.SetBool("IsGonnaClimb", true);
            lerpTime = 1f;
            currentLerpTime = 0;
            isSetup = true;
        }

        if (transform.localPosition.y != endPos.y)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
                currentLerpTime = lerpTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, currentLerpTime/lerpTime);
        }
        else
        {
            isSetup = false;
            //startClimb = false;
            playerRigidbody.isKinematic = false;
            playerRigidbody.useGravity = false;
            anim.SetBool("IsGonnaClimb", false);
            isControllable = true;
            // setting up for climbing
            isClimbing = true;
            anim.SetBool("Climbing", true);
        }
    }

    public virtual void eClimb()
    {
        if (!isSetup)
        {
            startPos = endPos = transform.localPosition;
            if (transform.localRotation.eulerAngles.z > 1 || transform.localRotation.eulerAngles.z < -1)
                endPos.y -= 0.8f;
            else
                endPos.y += 0.8f;
            endPos += transform.forward*1.1f;
            playerRigidbody.isKinematic = true;
            isControllable = false;
            anim.SetBool("IsGonnaClimbUp", true);
            anim.SetBool("IsClimbing", false);

            lerpTime = 2.5f;
            currentLerpTime = 0;

            isSetup = true;
        }

        if (transform.localPosition != endPos)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
                currentLerpTime = lerpTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, currentLerpTime / lerpTime);
        }
        else
        {
           /* if (transform.localRotation.eulerAngles.z > 1 || transform.localRotation.eulerAngles.z < -1)
                transform.localPosition -= new Vector3(0, 0.5f, 0);
            else
                transform.localPosition += new Vector3(0, 0.5f, 0);
            */
            isSetup = false;
            isClimbing = false;
            //endClimb = false;
            anim.SetBool("IsGonnaClimbUp", false);
            anim.SetBool("Start", false);
            isControllable = true;
            playerRigidbody.isKinematic = false;
            playerRigidbody.useGravity = true;
        }
    }

    public virtual void cClimb()
    {
        cancelClimb = false;
        isClimbing = false;
        playerRigidbody.useGravity = true;
        anim.SetBool("Climbing", false);
        isControllable = true;
    }
    // void Animating(float h, float v)
    // {
    //     // Create a boolean that is true if either of the input axes is non-zero.
    //     bool walking = h != 0f || v != 0f;

    //     // Tell the animator whether or not the player is walking.
    //     anim.SetBool("IsWalking", walking);
    //     anim.SetBool("IsTouchingMirror", isTouchingMirror);
    // }

    /*void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Mirror")
         {
             bTips = UI.subtitle("Press Space");
         }
     }*/
}
