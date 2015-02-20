using UnityEngine;
using System.Collections;

public class ParallaxController : MonoBehaviour 
{
    public GameObject skybox;
    public float skyboxSpeed = 0.01f;

    public GameObject[] parallaxLayers;
    public float[] parallaxLayerSpeed;


    private Inputs _playerInputs;
    public Rigidbody2D _playerRB;

    void Start () 
    {
        _playerInputs = GetComponentInParent<CameraTracking>().player.GetComponent<Inputs>();
    }
    
    void Update () 
    {
     /*   skybox.transform.localPosition = new Vector3(skybox.transform.localPosition.x - skyboxSpeed, 
                                                     skybox.transform.localPosition.y, 
                                                     skybox.transform.localPosition.z);

        if (skybox.transform.localPosition.x < -35.54f)
        {
            skybox.transform.localPosition = new Vector3(0, skybox.transform.localPosition.y, skybox.transform.localPosition.z);
        }
*/
        float direction = 1f;

        if (_playerInputs.isFacingLeft)
            direction = -1;
        else
            direction = 1;

        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            if(_playerRB.velocity.x != 0)
            {
                parallaxLayers[i].transform.localPosition = new Vector3(parallaxLayers[i].transform.localPosition.x - parallaxLayerSpeed[i]*direction, 
                                                                        parallaxLayers[i].transform.localPosition.y, 
                                                                        parallaxLayers[i].transform.localPosition.z);
            }
            if(parallaxLayers[i].transform.localPosition.x > 54f)
            {
                parallaxLayers[i].transform.localPosition = new Vector3(0, 
                                                                        parallaxLayers[i].transform.localPosition.y, 
                                                                        parallaxLayers[i].transform.localPosition.z);
            }
        }

    }
}
