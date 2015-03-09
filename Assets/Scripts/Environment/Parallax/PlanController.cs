using UnityEngine;
using System.Collections;

public class PlanController : MonoBehaviour
{
    #region Parametry planu

    public float speed = 0.1f;      //  Szybkość ruchu planu

    //  Referencja na segmenty planu a i b.
    private GameObject planSegment_a;
    private GameObject planSegment_b;

    #region Prawy i lewy triger starujący planem parakalsy
    
    public BoxCollider2D leftTrigger;
    public BoxCollider2D rightTrigger;
    
    #endregion

    #region Sprajty segmentów paralaksy.
    
    //  Tablica sprajtów
    public Sprite[] planSegmentSprites;
    //  Indeks pozwalający zmieniać sprajty
    private int planSegmentSpriteIndex = 0;
    
    #endregion

    private Rigidbody2D playerRB;               //  Referencja na ciało fizyczne gracza, która odpowiada za sterowanie trigerami.

    #endregion

    #region Metody

    void Start()
    {
        //  Wyłączenie obydwu trigerrów.
        leftTrigger.enabled = rightTrigger.enabled = false;
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
        if (playerRB == null)
            //  Zostaje ono pobrane z rodzica. Głownego obiektu paralaksy.
            playerRB = GetComponentInParent<ParallaxController>().PlayerRB;
    
        //  W zależności od kierunku poruszania się gracza następuje włączenie bąć wyłączenie odpowiednich trigerrów.
        if (playerRB.velocity.x > 0)
        {
            leftTrigger.enabled = true;
            rightTrigger.enabled = false;
        }
        if (playerRB.velocity.x < 0)
        {
            leftTrigger.enabled = false;
            rightTrigger.enabled = true;
        }
        
        #endregion
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        #region Przesuwanie segmentów paralaksy.

        //  Sprawdzenie czy segment nalezy do planu
        if (coll.gameObject == planSegment_a || coll.gameObject == planSegment_b)
        {
            //  Jeżeli gracz prousza się w lewo.
            if (playerRB.velocity.x < 0)
            {
                //  Zwiększenie indekus 
                ++planSegmentSpriteIndex;
                //  Sprawdzenie czy indeks nie przekrasza rozmiaru tablicy ze sprajtami
                if (planSegmentSpriteIndex > (planSegmentSprites.Length - 1))
                    planSegmentSpriteIndex = 0;
                
                //  Pobranie referencji na na renderer sprjów planu i ustawienie nowego sprajta.
                coll.gameObject.GetComponent<SpriteRenderer>().sprite
                = planSegmentSprites[planSegmentSpriteIndex];
                
                //  Sprawdzenie który segment wywołał kolizje ustawienie tego segmentu za poprzednim
                if (coll.name == "PlanSegment_a")
                    coll.transform.localPosition = new Vector2(planSegment_b.transform.localPosition.x - 54.64f,
                    planSegment_b.transform.localPosition.y);

                if (coll.name == "PlanSegment_b")
                    coll.transform.localPosition = new Vector2(planSegment_a.transform.localPosition.x - 54.64f,
                    planSegment_a.transform.localPosition.y);
            }

            //  Jeżeli gracz prousza się w prawo.
            if (playerRB.velocity.x > 0)
            {
                //  Zmniejszenie indeksu
                --planSegmentSpriteIndex;
                //  Sprawdzenie czy indeks ma wartości ujemnej
                if (planSegmentSpriteIndex < 0)
                    planSegmentSpriteIndex = (planSegmentSprites.Length - 1);
             
                //  Pobranie referencji na na renderer sprjów planu i ustawienie nowego sprajta.
                coll.gameObject.GetComponent<SpriteRenderer>().sprite
                = planSegmentSprites[planSegmentSpriteIndex];

                //  Sprawdzenie który segment wywołał kolizje ustawienie tego segmentu za poprzednim
                if (coll.name == "PlanSegment_a")
                    coll.transform.localPosition = new Vector2(planSegment_b.transform.localPosition.x + 54.64f,
                    planSegment_b.transform.localPosition.y);

                if (coll.name == "PlanSegment_b")
                    coll.transform.localPosition = new Vector2(planSegment_a.transform.localPosition.x + 54.64f,
                    planSegment_a.transform.localPosition.y);
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
            planSegment_a = _planSegment;
        else
            planSegment_b = _planSegment;
    }
    
    #endregion
}
