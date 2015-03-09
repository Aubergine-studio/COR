using UnityEngine;
using System.Collections;

[System.Serializable]
public class CloudController : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D coll)
    {  
        //  Sprawdzanie czy nienastąpiła klizja z trigerem dezaktywującym chmurke.
        if(coll.tag == "Parallax")
           //   Dezaktywacja chmurki.
           gameObject.SetActive(false);
    }

}
