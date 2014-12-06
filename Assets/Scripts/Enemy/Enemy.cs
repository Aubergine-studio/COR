using UnityEngine;
using System.Collections;

public class Enemy : Character 
{
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		Debug.Log ("Boli!");

		if (Health <= 0)
			Destroy (gameObject);
		else
			Health -= 50f;
	}

	override
	protected void Actions(){}

	void Update()
	{
		inputs.isGrounded = Physics2D.OverlapCircle(isOnGround.position, 0.2f, Ground);
	}

}
