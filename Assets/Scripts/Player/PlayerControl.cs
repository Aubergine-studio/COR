using UnityEngine;
using System.Collections;

public class PlayerControl : Character
{

    /*
     * Kontrola animatora postaci. 
     */

    int AttaksHash = Animator.StringToHash("Base Layer.Attacks");
   
    /*
     * Wejścia. 
     */

    /*
     * Zmienne dotyczące skku
     */

    private bool combat = false;

    private float attack = 0.0f;



    /*
     * Funkcja reagujaca na wejści.
     * W odpowiedzi na nie wywołuje odpowiednie kacje.
     */

    override    
    protected void Actions()
    {
        AnimationControl();

        ColiderControl();

        if (inputMenager.jump  && grounded)             //   Warunki wywołania akcji skoku.             
        {
            Jump();
        }

        if (inputMenager.action && climb)              //    Warunki wywołania akcji wsponania.
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
        animator.SetBool ("Ground", grounded);
        animator.SetBool ("Fire", inputMenager.fire);

		animator.SetFloat ("Attack", inputMenager.attack);
        animator.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		animator.SetFloat ("Speed", Mathf.Abs(inputMenager.horizontalInput));

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

    void Attack()
    {
        rigidbody2D.velocity = new Vector2(0f, 0f);

    }


    /*
     * Update wywiłujacy się co klatkę. 
     */

    void Update()
    {
		inputMenager.GetInputs ();
		
		grounded = Physics2D.OverlapCircle(IsOnGrond.position, 0.2f, Ground);
    }

    /*
     * Update wywołujacy się co stały interwał czasowy.
     */
    
    void FixedUpdate()
    {
        Actions();
    }
}
