using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Exit : MonoBehaviour {
    public int index;
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(index, LoadSceneMode.Single);
            c.gameObject.GetComponent<PlayerController>().isControllable = false;
        }
        else
        {
            Destroy(c.gameObject);
        }
    }

}
