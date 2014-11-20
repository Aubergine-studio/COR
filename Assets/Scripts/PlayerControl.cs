using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    /*
     * 
     * 
     * Parametry zachowania postaci.
     * 
     * 
     */
    
    public float maxSpeed = 10f;
    public float jumpForce = 400f;  


    /*
     * Kontrola animatora postaci. 
     */

    int AttaksHash = Animator.StringToHash("Base Layer.Attacks");
    
    private bool facingRight = true;    //  Obracanie postaci.
    private Animator animator;          //  Animator postaci. 

    private CircleCollider2D    circleColider;
    private BoxCollider2D       boxCollider;

    /*
     * Wejścia. 
     */

    /*
     * Zmienne dotyczące skku
     */
    private bool grounded = false;      //  Czy postać jest uziemiona?
    public Transform IsOnGrond;         //  Pozycja Obiektu wykrywajacego ziemie.
    public LayerMask Ground;            //  Maska na której znajdują się obiekty uznawane za ziemie.

    private bool jump;                  //  Zmienna przechowująca informacje czy skok został wykonany.



    private bool action = false;
    private bool climb = false;
    private bool combat = false;
    private bool fire = false;
    private float attack = 0.0f;

    public float horizontalInput;      //  Odczyt gałki analogowej mówiący o tym jak mocno została ona wychylona.

    /*
     * Funkcja pobierająca wejścia
     */

    void Inputs()
    {
        /*
         * Sprawdzanie przycisków.
         */

        grounded = Physics2D.OverlapCircle(IsOnGrond.position, 0.2f, Ground);

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            attack = Random.Range(1.0f, 2.0f);
            fire = true;
        }

        if (Input.GetKeyUp(KeyCode.JoystickButton1))
        {
            fire = false;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            action = true;
            Debug.Log("X");
        } 

        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            action = false;
            Debug.Log("X");
        } 


        if (Input.GetKey(KeyCode.Joystick1Button0))      //  Skok.
            jump = true;
        else
            jump = false;

        /*
         * Wejścia analogowe.
         */

        horizontalInput = Input.GetAxis("Horizontal");  //  Prawo/lewo
        
    }

    /*
     * Funkcja reagujaca na wejści.
     * W odpowiedzi na nie wywołuje odpowiednie kacje.
     */

    void Actions()
    {
        AnimationControl();

        ColiderControl();



        if (jump && grounded)             //   Warunki wywołania akcji skoku.             
        {
            Jump();
        }

        if (action && climb)              //    Warunki wywołania akcji wsponania.
        {
            Climb();
        }

        AnimatorStateInfo a = animator.GetCurrentAnimatorStateInfo(0);

        if (grounded && !(a.nameHash == AttaksHash))                     //   Warunki wywołania akcji poruszania się.
        {
            Move();
        }

        if (a.nameHash == AttaksHash)
        {
            Attack();
        }
    }

    /*
     * Funkcja Kontrolująca AC(animation controller).
     */


    void AnimationControl()
    {
        animator.SetBool("Ground", grounded);
        animator.SetBool("Fire", fire);
        animator.SetFloat("Attack", attack);
        animator.SetFloat ("vSpeed", rigidbody2D.velocity.y);
    }

    void ColiderControl()
    {
        if (rigidbody2D.velocity.y < 0 && !grounded)
        {
            circleColider.center = new Vector2(circleColider.center.x, -2.0f);
        } 
        
        if (rigidbody2D.velocity.y > 0 && !grounded)
        {
            circleColider.center = new Vector2(circleColider.center.x, -0.5f);
        }
    }

    /*
     * Funkcja wspinajaca.
     */
    void Attack()
    {
        rigidbody2D.velocity = new Vector2(0f, 0f);
    }

    void Climb()
    {
        float x, y, z;  //  Zmienne do określenia pozycji
        
        x = y = z = 0;  //  Zerowanie zmiennych.
        
        x = transform.position.x + horizontalInput;     //  Ustalanie x     //  Mechanizm powinien być 
        //                  //  ulepszony. Pozwala na okreśenie 
        y = boxCollider.transform.position.y + 2f;      //  Ustalanie y     //  gdzie po wespnięciu powinien się znaleść gracz.
        
        transform.position = new Vector3(x, y, z);      //  Ustalenie nowej pozycji gracza
        
        rigidbody2D.gravityScale = 1;                   //  Włączenie ponownie grawitacji.
        
        climb = false;                                  //  Ustawienie frali wspoinania na fałsz.
    }

    /*
     * Funkcja poruszania się.
     */

    void Move()
    {
        rigidbody2D.velocity = new Vector2(horizontalInput * maxSpeed, rigidbody2D.velocity.y);
        
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        
        if (horizontalInput < 0 && !facingRight)
            this.Flip();
        else
            if (horizontalInput > 0 && facingRight)
                this.Flip();

    }

    /*
     * Funkcja Skoku.
     */

    void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0, jumpForce));
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 Flip = transform.localScale;
        
        Flip.x *= -1;
        
        transform.localScale = Flip;
    }
    /*
     * Konstruktor
     */
    void Start()
    {
        animator = GetComponent<Animator>();
        circleColider = GetComponent<CircleCollider2D>();
    }

    /*
     * Update wywiłujacy się co klatkę. 
     */

    void Update()
    {
        Inputs();
    /*    if (attack > 0)
            attack = 0;*/
    }

    /*
     * Update wywołujacy się co stały interwał czasowy.
     */
    
    void FixedUpdate()
    {
        Actions();
    }

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
            if(horizontalInput < 0 && coll.gameObject.transform.localScale.x < 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, 2f);
            }

            if(horizontalInput > 0 && coll.gameObject.transform.localScale.x < 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, -2f);
            }

            if(horizontalInput > 0 && coll.gameObject.transform.localScale.x > 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, 2f);
            }
            
            if(horizontalInput < 0 && coll.gameObject.transform.localScale.x > 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, -2f);
            }

            /* if(rigidbody2D.velocity.x > 2f && horizontalInput < 0)
            {
                rigidbody2D.velocity =  new Vector2( 2f, rigidbody2D.velocity.y);
            }
            
            if(rigidbody2D.velocity.x > -2f && horizontalInput > 0)
            {
                rigidbody2D.velocity =  new Vector2( -2f, rigidbody2D.velocity.y);
            }*/

            grounded = true;
            /*if(horizontalInput > 0.3f) horizontalInput = 0.3f;
            if(horizontalInput < -0.3f) horizontalInput = -0.3f;*/

         /*   if(horizontalInput < 0)
                transform.position =  new Vector3(transform.position.x -0.001f, transform.position.y+0.001f, 0);*/

        }

    }

    void OnCollisionExit2D(Collision2D coll)
    {
    }

}
