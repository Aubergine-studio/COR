using UnityEngine;

public class PlanController : MonoBehaviour
{
    #region Parametry planu

    public float speed = 0.1f;      //  Szybkość ruchu planu

    //  Referencja na segmenty planu a i b.
    private GameObject _planSegmentA;
    private GameObject _planSegmentB;

    #region Prawy i lewy triger starujący planem parakalsy
    
    public BoxCollider2D LeftTrigger;
    public BoxCollider2D RightTrigger;
    
    #endregion

    #region Sprajty segmentów paralaksy.
    
    //  Tablica sprajtów
    public Sprite[] PlanSegmentSprites;
    //  Indeks pozwalający zmieniać sprajty
    private int _planSegmentSpriteIndex = 0;
    
    #endregion

    private Rigidbody2D _playerRb;               //  Referencja na ciało fizyczne gracza, która odpowiada za sterowanie trigerami.

    #endregion

    #region Metody

    void Start()
    {
        //  Wyłączenie obydwu trigerrów.
        LeftTrigger.enabled = RightTrigger.enabled = false;
    }
    
    /// <summary>
    /// Metoda starująca całym planem paralaksy.
    /// Odpowiada na odpowiednie przesuwanie segmentów
    /// planu paralaksy.
    /// </summary>
    void FixedUpdate()
    {
        #region Sterowanie triggerami.

        //  Jeśli nie ma referencji na ciało fizyczne gracza
        if (_playerRb == null)
            //  Zostaje ono pobrane z rodzica. Głownego obiektu paralaksy.
            _playerRb = GetComponentInParent<ParallaxController>().PlayerRB;
    
        //  W zależności od kierunku poruszania się gracza następuje włączenie bąć wyłączenie odpowiednich trigerrów.
        if (_playerRb.velocity.x > 0)
        {
            LeftTrigger.enabled = true;
            RightTrigger.enabled = false;
        }
        if (_playerRb.velocity.x < 0)
        {
            LeftTrigger.enabled = false;
            RightTrigger.enabled = true;
        }
        
        #endregion
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        #region Przesuwanie segmentów paralaksy.

        //  Sprawdzenie czy segment nalezy do planu
        if (coll.gameObject == _planSegmentA || coll.gameObject == _planSegmentB)
        {
            //  Jeżeli gracz prousza się w lewo.
            if (_playerRb.velocity.x < 0)
            {
                //  Zwiększenie indekus 
                ++_planSegmentSpriteIndex;
                //  Sprawdzenie czy indeks nie przekrasza rozmiaru tablicy ze sprajtami
                if (_planSegmentSpriteIndex > (PlanSegmentSprites.Length - 1))
                    _planSegmentSpriteIndex = 0;    
                
                //  Pobranie referencji na na renderer sprjów planu i ustawienie nowego sprajta.
                //coll.gameObject.GetComponent<SpriteRenderer>().sprite
                //= PlanSegmentSprites[_planSegmentSpriteIndex];
                
                //  Sprawdzenie który segment wywołał kolizje ustawienie tego segmentu za poprzednim
                if (coll.name == "PlanSegment_a")
                    coll.transform.localPosition = new Vector2(_planSegmentB.transform.localPosition.x - 80f,
                    _planSegmentB.transform.localPosition.y);

                if (coll.name == "PlanSegment_b")
                    coll.transform.localPosition = new Vector2(_planSegmentA.transform.localPosition.x - 80f,
                    _planSegmentA.transform.localPosition.y);
            }

            //  Jeżeli gracz prousza się w prawo.
            if (_playerRb.velocity.x > 0)
            {
                //  Zmniejszenie indeksu
                --_planSegmentSpriteIndex;
                //  Sprawdzenie czy indeks ma wartości ujemnej
                if (_planSegmentSpriteIndex < 0)
                    _planSegmentSpriteIndex = (PlanSegmentSprites.Length - 1);
             
                //  Pobranie referencji na na renderer sprjów planu i ustawienie nowego sprajta.
                //coll.gameObject.GetComponent<SpriteRenderer>().sprite
                //= PlanSegmentSprites[_planSegmentSpriteIndex];

                //  Sprawdzenie który segment wywołał kolizje ustawienie tego segmentu za poprzednim
                if (coll.name == "PlanSegment_a")
                    coll.transform.localPosition = new Vector2(_planSegmentB.transform.localPosition.x + 80f,
                    _planSegmentB.transform.localPosition.y);

                if (coll.name == "PlanSegment_b")
                    coll.transform.localPosition = new Vector2(_planSegmentA.transform.localPosition.x + 80f,
                    _planSegmentA.transform.localPosition.y);
            }
        }

        #endregion
    }

    /// <summary>
    /// Poieranie referencji na segmenty paralaksy
    /// </summary>
    /// <param name="_planSegment">Referencja na obiekt parakalsy</param>
    public void AddPlan(GameObject _planSegment)
    {
        if (_planSegment.name == "PlanSegment_a")
            _planSegmentA = _planSegment;
        else
            _planSegmentB = _planSegment;
    }
    
    #endregion
}
