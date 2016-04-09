using System;
using UnityEngine;

[Serializable]
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float moveDirection = 1.0f;
    [HideInInspector]
    public string owner;
    public float projectileSpeed = 5f;
    public float projectileDistance = 1f;
    public int projectileDamage = 10;

    private float startX;

	// Use this for initialization
	void Start ()
	{
		rigidbody2D.velocity = new Vector2 (projectileSpeed * moveDirection, rigidbody2D.velocity.y);
        startX = Mathf.Abs(transform.position.x);
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(Mathf.Abs(startX - Mathf.Abs(transform.position.x)) > projectileDistance)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
