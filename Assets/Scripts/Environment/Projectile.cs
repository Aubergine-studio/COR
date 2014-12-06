using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	private float projectileSpeed = 5f;
	private float projectileDistance = 1f;
	private int projectileDamage = 10;

	// Use this for initialization
	void Start ()
	{
		rigidbody2D.velocity = new Vector2 (projectileSpeed, rigidbody2D.velocity.y);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
