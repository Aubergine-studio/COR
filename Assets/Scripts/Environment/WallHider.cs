using UnityEngine;
using System.Collections;

public class WallHider : MonoBehaviour 
{
	private SpriteRenderer spriteRenderer;

	public void Start()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player")
		{
			spriteRenderer.enabled = false;
		}
	}

	void OnTriggerExit2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player")
		{
			spriteRenderer.enabled = true;
		}
	}

}
