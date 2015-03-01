using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character 
{
	public int experience = 10;
	private static List<Player> players = new List<Player>();

	void Start()
	{
		base.Start();
		if(players.Count == 0) 
			foreach (GameObject gobj in GameObject.FindGameObjectsWithTag ("Player")) 
			{
				players.Add( gobj.GetComponent<Player>());
			}
	}
	
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
			foreach(Player p in players)
			{
				p.Exp = experience;
			}
			this.enabled = false;
		}
		inputs.isGrounded = Physics2D.OverlapCircle(isOnGround.position, 0.2f, Ground);
	}

}
