using UnityEngine;
using System.Collections;

public class TreasureChest : MonoBehaviour {
    public char ID;

    Animation anim;
    public bool isOpened;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        isOpened = false;
    }
	
	public void Open()
    {
        anim.Play("open");
        isOpened = true;
    }
    
}
