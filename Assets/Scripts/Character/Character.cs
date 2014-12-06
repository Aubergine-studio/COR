using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour 
{
    /*
     *  Dodatkowe elementy związane z sterowaniem postacią. 
     */
    public Transform isOnGround;                	//  Pozycja Obiektu wykrywajacego ziemie.
    public LayerMask Ground;                   	//  Maska pozwalajaca określić co jest "ziemią"
	protected  CircleCollider2D[] circleColliders;	//	wykrycie kolizji przeciwnika z atakiem wręcz gracza.
    protected Animator animator;                //  Animator postaci.   TMP do zastąpienia osobną klasa.
	protected AnimatorController animatorController;

	public GameObject projectileType;

    /*
     * Rozwiązanie tymczasowe
     */
	protected CircleCollider2D  	circleCollider;
    protected BoxCollider2D       	boxCollider;
    /*
     * Rozwiązanie tymczasowe
     */

	public CircleCollider2D hitCollider;
	public CircleCollider2D legsCollider;

    /*
     * Parametry zachowania postaci.
     */

    public float maxSpeed   = 10f;      //  Maksymalna prędkośc poruszania się. 
    public float jumpForce  = 400f;     //  Siła skoku.
    protected Inputs inputs;        //  Wejścia.



    /*
     * Statystyki postaci.
     */

	protected float Health    = 100.0f;   //  Zdrowie.
	protected float Mana      = 100.0f;   //  Mana
	protected float Stamina   = 100.0f;   //  Stamina

    /*
     *  Metody 
     */

    void Start()
    {
        animator = GetComponent<Animator>();
		circleColliders = GetComponents<CircleCollider2D>();
		hitCollider.enabled = false;
        inputs = GetComponent<Inputs>();
		animatorController = GetComponent<AnimatorController> ();
    }

    /*
     * Funkcja obraca postać.
     */

    protected void Flip()
    {
		inputs.isFacingRight = !inputs.isFacingRight;
        
		Vector3 Flip = transform.localScale;
        
        Flip.x *= -1;

        transform.localScale = Flip;
    }

	/*
	 * Funkcja ataku.
	 */

	protected void Attack()
	{
		//rigidbody2D.velocity = new Vector2(0f, 0f);
		
		hitCollider.enabled = inputs.fire;
		var clone = Instantiate (projectileType, transform.position, transform.localRotation) as GameObject;

		if(inputs.isFacingRight)
			clone.transform.localScale = new Vector3(1,1,1);
		else
			clone.transform.localScale = new Vector3(-1,1,1);

		inputs.fire = false;
	}

    /*
     * Funkcja poruszania się.
     */
    
    protected void Move()
    {
        rigidbody2D.velocity = new Vector2(inputs.horizontalInput * maxSpeed, rigidbody2D.velocity.y);
        
		if (inputs.horizontalInput < 0 && !inputs.isFacingRight)
            Flip();
        
		if (inputs.horizontalInput > 0 && inputs.isFacingRight)
            Flip();
    }
    
    /*
     * Funkcja Skoku.
     */
    
    protected void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0, jumpForce));
		inputs.jump = false;
    }
    /*
     * Funkcja wspinajaca.
     */
    
    
    protected void Climb()
    {
        float x, y, z;  //  Zmienne do określenia pozycji
        
        x = y = z = 0;  //  Zerowanie zmiennych.
        
        x = transform.position.x + inputs.horizontalInput;     //  Ustalanie x     //  Mechanizm powinien być 
        //  ulepszony. Pozwala na okreśenie 
        y = boxCollider.transform.position.y + 2f;      //  Ustalanie y     //  gdzie po wespnięciu powinien się znaleść gracz.
        
        transform.position = new Vector3(x, y, z);      //  Ustalenie nowej pozycji gracza
        
        rigidbody2D.gravityScale = 1;                   //  Włączenie ponownie grawitacji.
        
		inputs.isClimbing = false;                                  //  Ustawienie frali wspoinania na fałsz.
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
		if (!inputs.isGrounded && coll.gameObject.tag == "Ground")
        {
            boxCollider = coll.gameObject.GetComponent<BoxCollider2D>();
            
            transform.position = new Vector3(transform.position.x, coll.gameObject.transform.position.y - 0.5f);
            rigidbody2D.velocity = new Vector2(0, 0);
            rigidbody2D.gravityScale = 0;
            
			inputs.isClimbing = true;
        }
    }
    
    void OnCollisionStay2D (Collision2D coll)
    {
        if(coll.gameObject.tag == "Stairs")
        {
            if(inputs.horizontalInput < 0 && coll.gameObject.transform.localScale.x < 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, 2f);
            }
            
            if(inputs.horizontalInput > 0 && coll.gameObject.transform.localScale.x < 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, -2f);
            }
            
            if(inputs.horizontalInput > 0 && coll.gameObject.transform.localScale.x > 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, 2f);
            }
            
            if(inputs.horizontalInput < 0 && coll.gameObject.transform.localScale.x > 0)
            {
                rigidbody2D.velocity =  new Vector2(rigidbody2D.velocity.x, -2f);
            }
            
			inputs.isGrounded = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D coll)
    {
    }

}
