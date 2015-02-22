using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour 
{
    public GameObject skybox;
    public Sprite skyboxSprite;
    public float skyboxSpeed = 0.01f;

    public GameObject cloud;
    private List<GameObject> cloudsObjects = new List<GameObject>();
    public Sprite[] clouds;

    public int cloudsCount = 10;
    public float cloudsSpawnTime = 300f;
    private float cloudsSpawnTimer;
    public float cloudsMinSpeed = 10f;
    public float cloudsMaxSpeed = 20f;
    public float cloudsMinSpawnHeight = 1f;
    public float cloudsMaxSpawnHeight = 5f;

    public GameObject[] parallaxLayers;
    public float[] parallaxLayerSpeed;


    private Inputs _playerInputs;
    private Rigidbody2D _playerRB;

    void Start () 
    {
        _playerRB = GetComponentInParent<CameraTracking>().player.GetComponent<Rigidbody2D>();
        _playerInputs = GetComponentInParent<CameraTracking>().player.GetComponent<Inputs>();
        skybox.GetComponent<SpriteRenderer>().sprite = skyboxSprite;
        cloudsSpawnTimer = cloudsSpawnTime;

          for (int i = 0; i < cloudsCount; ++i)
        {
            cloudsObjects.Add(            (Instantiate(cloud, new Vector3(20, Random.Range(cloudsMinSpawnHeight, cloudsMaxSpawnHeight) ,0),
                                                      Quaternion.identity) as GameObject)
                              );
       /*    Instantiate(cloudsObjects[i], new Vector3(20, Random.Range(cloudsMinSpawnHeight, cloudsMaxSpawnHeight) ,0),
                        Quaternion.identity); */
            cloudsObjects[i].transform.parent = transform;
            cloudsObjects[i].SetActive(false);

        }
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

        if (cloudsSpawnTimer <= 0)
        {
            for (int i = 0; i < cloudsCount; ++i)
            {
                if(!cloudsObjects[i].activeSelf)
                {
                    cloudsObjects[i].transform.localPosition = new Vector3(20, Random.Range(cloudsMinSpawnHeight, cloudsMaxSpawnHeight));
                    cloudsObjects[i].SetActive(true);
                    cloudsObjects[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(cloudsMinSpeed, cloudsMaxSpeed), 0);
                    cloudsObjects[i].GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length)];
                    break;
                }
            }
                cloudsSpawnTimer = cloudsSpawnTime;

      /*      Object o = Instantiate(cloud, new Vector3(20, Random.Range(cloudsMinSpawnHeight, cloudsMaxSpawnHeight) ,0),
                                   Quaternion.identity);

            (o as GameObject).GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(cloudsMinSpeed, cloudsMaxSpeed), 0);
            (o as GameObject).GetComponent<Transform>().parent = transform;

            cloudsObjects.Add(o); */

        } else
        {
            cloudsSpawnTimer -= Time.deltaTime;
        }

    }
}
