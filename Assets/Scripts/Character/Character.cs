using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour 
{
    /*
     *  Dodatkowe elementy związane z sterowaniem postacią. 
     */

    protected bool facingRight  = true;        //   Obracanie postaci.
    protected bool grounded     = false;       //   Czy postać jest uziemiona?
    protected bool climb        = false;       //   Gotowość postaci do wspinaczki 
    public Transform IsOnGrond;                //   Pozycja Obiektu wykrywajacego ziemie.
    public LayerMask Ground;                   //   Maska pozwalajaca określić co jest "ziemią"
        
    protected Animator animator;                  //  Animator postaci.   TMP do zastąpienia osobną klasa.


    /*
     * Rozwiązanie tymczasowe
     */
    protected CircleCollider2D    circleColider;
    protected BoxCollider2D       boxCollider;
    /*
     * Rozwiązanie tymczasowe
     */

    /*
     * Parametry zachowania postaci.
     */

    public float maxSpeed   = 10f;      //  Maksymalna prędkośc poruszania się. 
    public float jumpForce  = 400f;     //  Siła skoku.
    protected Inputs inputMenager;        //  Wejścia od gracza.



    /*
     * Statystyki postaci.
     */

    private float Health    = 100.0f;   //  Zdrowie.
    private float Mana      = 100.0f;   //  Mana
    private float Stamina   = 100.0f;   //  Stamina

    /*
     *  Metody 
     */

    void Start()
    {
        animator = GetComponent<Animator>();
        circleColider = GetComponent<CircleCollider2D>();
        
        inputMenager = GetComponent<Inputs>();
    }

    /*
     * Funkcja obraca postać.
     */

    protected void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 Flip = transform.localScale;
        
        Flip.x *= -1;
        
        transform.localScale = Flip;
    }

    /*
     * Funkcja poruszania się.
     */
    
    protected void Move()
    {
        rigidbody2D.velocity = new Vector2(inputMenager.horizontalInput * maxSpeed, rigidbody2D.velocity.y);
        
        if (inputMenager.horizontalInput < 0 && !facingRight)
            this.Flip();
        
        if (inputMenager.horizontalInput > 0 && facingRight)
            this.Flip();
    }
    
    /*
     * Funkcja Skoku.
     */
    
    protected void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0, jumpForce));
    }
    /*
     * Funkcja wspinajaca.
     */
    
    
    protected void Climb()
    {
        float x, y, z;  //  Zmienne do określenia pozycji
        
        x = y = z = 0;  //  Zerowanie zmiennych.
        
        x = transform.position.x + inputMenager.horizontalInput;     //  Ustalanie x     //  Mechanizm powinien być 
        //  ulepszony. Pozwala na okreśenie 
        y = boxCollider.transform.position.y + 2f;      //  Ustalanie y     //  gdzie po wespnięciu powinien się znaleść gracz.
        
        transform.position = new Vector3(x, y, z);      //  Ustalenie nowej pozycji gracza
        
        rigidbody2D.gravityScale = 1;                   //  Włączenie ponownie grawitacji.
        
        climb = false;                                  //  Ustawienie frali wspoinania na fałsz.
    }

    /*
     * Metoda odpowiadająca, za sterowanie postacią. 
     */

    protected abstract void Actions();

    /*
     * Funkcje kolizji 
     */

    void OnCollisionEnter2D (Collision2D coll)
    {
        if (!grounded && coll.gameObject.tag == "Ground")
        {
            boxCollider = coll.gameObject.GetComponent<BoxCollider2D>();
            
            transform.position = new Vector3(transform.position.x, coll.gameObject.transform.position.y - 0.5f);
            rigidbody2D.velocity = new Vector2(0, 0);
            rigidbody2D.gravityScale = 0;
            
            climb = true;
        }
    }
    
    void OnCollisionStay2D (Collision2D coll)
    {
        if(coll.gameObject.tag == "Stairs")
        {
            if(inputMenager.horizontalInput < 0 && coll.gameObject.transform.localScale.x < 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, 2f);
            }
            
            if(inputMenager.horizontalInput > 0 && coll.gameObject.transform.localScale.x < 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, -2f);
            }
            
            if(inputMenager.horizontalInput > 0 && coll.gameObject.transform.localScale.x > 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, 2f);
            }
            
            if(inputMenager.horizontalInput < 0 && coll.gameObject.transform.localScale.x > 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, -2f);
            }
            
            grounded = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D coll)
    {
    }

}
