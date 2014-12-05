using UnityEngine;
using System.Collections;

public class EnemyControl : Character 
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
		grounded = Physics2D.OverlapCircle(IsOnGrond.position, 0.2f, Ground);
	}

}
