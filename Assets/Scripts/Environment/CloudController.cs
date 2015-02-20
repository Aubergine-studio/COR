using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D coll)
    {  
        if(coll.tag == "Parallax")
           Destroy(this.gameObject);
    }

}
