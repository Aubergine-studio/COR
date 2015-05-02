using UnityEngine;
using System.Collections;

public class PlanSegment : MonoBehaviour
{
    #region Parametry segmentu planu parlakasy

    private float _speed;                // Prędkość segmentu planu                    
    private Rigidbody2D _playerRb;       //  Ciało fizyczne gracza kontrolujace ruch smegmentu

    #endregion

    #region Metody

    /// <summary>
    /// Metoda inicjalizująca zmienne
    /// </summary>
    void Start()
    {
        #region Inicjalizowanie wartości zmiennych

        //  Pobranie referencji na obiekt kontrolera planu
        PlanController planController = GetComponentInParent<PlanController>();
        //  Dodanie przekazanie referencji na samego siebie do kontlolera planu
        planController.AddPlan(this.gameObject);
        //  Pobranie prędkości planu
        _speed = planController.speed;

        #endregion
    }
    
    /// <summary>
    /// Metoda sterująca ruchem segmentu
    /// </summary>
    void Update()
    {
        // Jeśeli obiekt nie ma refencji na ciało fizyczne gracza,
        if(_playerRb == null)
            //  zostaje wyszukany obiekt paralaksy. Po pobrana zostaje referencja na ciało fizyczne gracza.
            _playerRb = GameObject.FindGameObjectWithTag("Parallax").GetComponent<ParallaxController>().PlayerRB;

        #region Detekcja ruchu gracza.
        
        //  Jeżeli gracz biegnie w segment porusza się w 
        if(_playerRb.velocity.x > 0)
        {
            rigidbody2D.velocity = new Vector2(-_speed, 0);
        }

        //  Jeżeli gracz nie prousza się, segment pozostaje w spoczynku.
        if (_playerRb.velocity.x == 0)
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        //  Jeżeli gracz biegnie w segment porusza się w 
        if (_playerRb.velocity.x < 0)
        {
            rigidbody2D.velocity = new Vector2(_speed, 0);
        }

        #endregion
    }
    
    #endregion
}
