using UnityEngine;
using System.Collections;

public class Enemy : Character 
{
	
	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.tag == "Projectile")
        {
           Health -= coll.GetComponent<Projectile>().projectileDamage;
        }
	}

	override
	protected void Actions(){}

	void Update()
	{
		if (Health <= 0) 
		{
			DisableAllColliders();
			rigidbody2D.gravityScale = 0;
		}
		inputs.isGrounded = Physics2D.OverlapCircle(isOnGround.position, 0.2f, Ground);
	}

}
