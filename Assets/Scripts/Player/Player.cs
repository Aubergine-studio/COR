using UnityEngine;
using System.Collections;

public class Player : Character
{
    override    
    protected void Actions()
    {
        ColiderControl();

		if (inputs.jump && inputs.isGrounded)             //   Warunki wywołania akcji skoku.             
        {
            Jump();
        }
		else
		{
			Move ();
		}

		if (inputs.action && inputs.isClimbing)              //    Warunki wywołania akcji wsponania.
        {
            Climb();
        }


		if (inputs.isGrounded )//&& !(a.nameHash == AttaksHash))                     //   Warunki wywołania akcji poruszania się.
        {
            Move();
        }

        if (inputs.fire && !(animatorController as HumanoidAnimatorController).GetAttackStatus())
        {
            Attack();
        }
    }

    void ColiderControl()
    {
		if (rigidbody2D.velocity.y < 0 && !inputs.isGrounded)
        {
			circleColliders[0].center = new Vector2(circleColliders[0].center.x, -2.0f);
        } 
        
		if (rigidbody2D.velocity.y > 0 && !inputs.isGrounded)
        {
			circleColliders[0].center = new Vector2(circleColliders[0].center.x, -0.5f);
        }
    }



    /*
     * Update wywiłujacy się co klatkę. 
     */

    void Update()
    {
		Actions();
		inputs.isGrounded = Physics2D.OverlapCircle(isOnGround.position, 0.2f, Ground);
    }

    /*
     * Update wywołujacy się co stały interwał czasowy.
     */
    
    void FixedUpdate()
    {
        
    }
}
