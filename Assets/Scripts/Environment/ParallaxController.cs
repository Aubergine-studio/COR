using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour 
{
    public GameObject skybox;
    public Sprite skyboxSprite;
    public float skyboxSpeed = 0.01f;

    public GameObject cloud;
    private List<Object> cloudsObjects = new List<Object>();
    private SpriteRenderer cloudsRenderer;
    public Sprite[] clouds;

    public float cloudsSpawnTime = 300f;
    private float cloudsSpawnTimer;
    public float cloudsMinSpeed = 10f;
    public float cloudsMaxSpeed = 20f;

    public GameObject[] parallaxLayers;
    public float[] parallaxLayerSpeed;


    private Inputs _playerInputs;
    public Rigidbody2D _playerRB;

    void Start () 
    {
        _playerInputs = GetComponentInParent<CameraTracking>().player.GetComponent<Inputs>();
        skybox.GetComponent<SpriteRenderer>().sprite = skyboxSprite;
        cloudsSpawnTimer = cloudsSpawnTime;
        cloudsRenderer = cloud.GetComponent<SpriteRenderer>();
    }
    
    void Update () 
    {
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

        if (cloudsSpawnTimer <= 0 && cloudsObjects.Count <= 10)
        {
            cloudsSpawnTimer = cloudsSpawnTime;
            cloudsRenderer.sprite = clouds[Random.Range(0, clouds.Length)];
            Object o = Instantiate(cloud, new Vector3(20, Random.Range(5,1) ,0),
                                   Quaternion.identity);

            (o as GameObject).GetComponent<Rigidbody2D>().velocity = new Vector2(
                -Random.Range(cloudsMinSpeed, cloudsMaxSpeed)
                , 0
                );
            (o as GameObject).GetComponent<Transform>().parent = transform;

            cloudsObjects.Add(o);

        } else
        {
            cloudsSpawnTimer -= Time.deltaTime;
        }

    }
}
