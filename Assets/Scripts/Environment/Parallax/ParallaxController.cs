using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour
{
    #region Parametry tła nieba.

    public GameObject skybox;
    public Sprite skyboxSprite;

    #endregion

    #region Parametry instancjonowaia chmur

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

    #endregion

    #region Ciało fizyczne gracza

    private Rigidbody2D _playerRB;                  //  Ciało fizyczne gracza. Wartości przyspieszenia tego ciała 
    //  starują ruchem paralaksy
    public Rigidbody2D PlayerRB
    {
        set { _playerRB = value; }
        get { return _playerRB; }
    }

    #endregion

    #region Metody

    /// <summary>
    /// Metoda inicjalizująca zmienne
    /// </summary>
    void Start()
    {
        #region Inicjalizowanie wartości zmiennych
        
   //     cloud = Resources.Load("Cloud") as GameObject;
        // Pobranie referencji na ciało fizyczne gracza
        _playerRB = GetComponentInParent<CameraTracking>().player.GetComponent<Rigidbody2D>();
        //  Pobranie renderera spritów z obiektu tła nieba
        skybox.GetComponent<SpriteRenderer>().sprite = skyboxSprite;
        //  Inicjalizacja licznika kontrolującego czas pokzywania się chmur
        cloudsSpawnTimer = cloudsSpawnTime;
        
        #endregion

        #region Instancjonowanie chmur
            for (int i = 0; i < cloudsCount; ++i)
            {
                GameObject o = (Instantiate(cloud, new Vector3(20, Random.Range(cloudsMinSpawnHeight, cloudsMaxSpawnHeight), 0),
                                                          Quaternion.identity) as GameObject);
                cloudsObjects.Add(o);
                cloudsObjects[i].transform.parent = transform;
                cloudsObjects[i].SetActive(false);
            }
        #endregion
    }

    /// <summary>
    /// W tej metodzie kontrolowane są chmurki.
    /// </summary>
    void Update()
    {
        #region Spowner chmur

        // Jeśli upłynął czas po, którym miała pojawić się chmurka.
        if (cloudsSpawnTimer <= 0)
        {
            //  Wyszukiwania jest pierwsza nieaktywny obiekt chmurki
            for (int i = 0; i < cloudsObjects.Count; ++i)
            {
                if (!cloudsObjects[i].activeSelf)
                {
                    //  Losowana jest nowa pozycja chmurki
                    cloudsObjects[i].transform.localPosition = new Vector3(20, Random.Range(cloudsMinSpawnHeight, cloudsMaxSpawnHeight));
                    //  Następuje ponowna aktywacja chmurki
                    cloudsObjects[i].SetActive(true);
                    //  Nadanie chmurce nowej wartości przyśpieszenia
                    cloudsObjects[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(cloudsMinSpeed, cloudsMaxSpeed), 0);
                    //  Wylosowanie nowego sprajta
                    cloudsObjects[i].GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length)];
                    break;
                }
            }
            //  Ponowna inicjalizacja licznika
            cloudsSpawnTimer = cloudsSpawnTime;
        }
        else
        {
            //  Dekrementacja licznika.
            cloudsSpawnTimer -= Time.deltaTime;
        }

        #endregion
    }

    #endregion
}
