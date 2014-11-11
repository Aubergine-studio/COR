using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	/*
	 * Parametry zachowania postaci.
	 */

    public float maxSpeed = 10f;
	public float jumpForce = 400f;

	/*
	 * Kontrola animatora postaci. 
	 */

	private bool facingRight = true;	//	Obracanie postaci.
    private Animator _animator;			//	Animator postaci. 

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
		if (Input.GetKey(KeyCode.JoystickButton0)) 
		{
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

       _animator.SetFloat("Speed",Mathf.Abs(move));

        if (move < 0 && !facingRight)
            this.Flip();
        else
            if(move > 0 && facingRight)
                this.Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 Flip = transform.localScale;

        Flip.x *= -1;

        transform.localScale = Flip;
    }
    
}
