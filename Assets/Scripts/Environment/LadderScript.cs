using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour 
{
    public GameObject ActionButtonIcon;
	// Use this for initialization

    void OnTriggerEnter2D(Collider2D coll) 
    {   
        if (coll.gameObject.tag == "Player")
        {
            ActionButtonIcon.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D coll) 
    {   
        if (coll.gameObject.tag == "Player")
        {
            ActionButtonIcon.SetActive(false);
        }
    }

}
