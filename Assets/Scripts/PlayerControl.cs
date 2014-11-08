using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float maxSpeed = 10f;
    public Camera mainCamera;
    private bool facingRight = true;
    
    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        mainCamera.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, mainCamera.transform.localPosition.z) ;

    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        System.Console.WriteLine(move);
        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

       _animator.SetFloat("Speed",Mathf.Abs(move));

        if (move < 0 && !facingRight)
        {
            this.Flip();
        }
        else
        {
            if(move > 0 && facingRight)
                this.Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        
        Vector3 Flip = transform.localScale;

        Flip.x *= -1;

        transform.localScale = Flip;
    }
    
}
