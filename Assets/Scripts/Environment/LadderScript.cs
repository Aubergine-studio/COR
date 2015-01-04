using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour
{
    public GameObject ActionButtonIcon;
    public GameObject[] grounds;
    private Inputs playerInputs;

    void Stat()
    {
        Transform parent = GetComponentInParent<Transform>();
        transform.localScale = new Vector3(parent.localScale.x, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {   
        if (coll.gameObject.tag == "Player")
        {
            ActionButtonIcon.SetActive(true);
            playerInputs = coll.GetComponent<Inputs>();
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (playerInputs == null)
        {
            foreach (GameObject g in grounds)
            {
                g.SetActive(true);
            }
        } else
        {

            foreach (GameObject g in grounds)
            {
                g.SetActive(!playerInputs.action);
            }

        }

    }

    void OnTriggerExit2D(Collider2D coll)
    {   
        if (coll.gameObject.tag == "Player")
        {
            ActionButtonIcon.SetActive(false);
            playerInputs = null;
        }
    }

}
