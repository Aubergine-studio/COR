using UnityEngine;
using System.Collections;

public class Player : Character
{
    private int experience;

    public int Exp
    {
        get { return experience; }
        set
        {
            if (value > 0)
            {
                Debug.Log("I get a " + value.ToString() + " experience!");
                experience += value;
            }
        }
    }

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
            if (!inputs.isLadderClimbing)
                Move();
        }

        if (inputs.action && inputs.isClimbing)              //    Warunki wywołania akcji wsponania.
        {
            //    Climb();
        }


        if (inputs.isGrounded && !inputs.isLadderClimbing)//&& !(a.nameHash == AttaksHash))                     //   Warunki wywołania akcji poruszania się.
        {
            Move();
        }

        if (inputs.fire && !(animatorController as HumanoidAnimatorController).GetAttackStatus())
        {
            Attack();
        }

        if (inputs.isLadderClimbing)
        {
            LadderClimb();
        }

    }

    void ColiderControl()
    {
        if (rigidbody2D.velocity.y < 0 && !inputs.isGrounded)
        {
            legsCollider.center = new Vector2(legsCollider.center.x, -2.8f);
        }

        if (rigidbody2D.velocity.y > 0 && !inputs.isGrounded)
        {
            legsCollider.center = new Vector2(legsCollider.center.x, 0.5f);
        }
    }

    void GetExp(int _exp)
    {
    }

    /*
     * Update wywiłujacy się co klatkę. 
     */

    void Update()
    {
        Actions();
        inputs.isGrounded = Physics2D.OverlapCircle(isOnGround.position, isOnGroundRadius, Ground);
    }

    /*
     * Update wywołujacy się co stały interwał czasowy.
     */

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
    }

    void OnCollisionStay2D(Collision2D coll)
    {
    }

    void OnCollisionExit2D(Collision2D coll)
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            coll.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (coll.gameObject.tag == "Stairs")
        {
            inputs.isStairsClimbing = true;
        }

    }

    void OnTriggerStay2D(Collider2D coll)
    {
        LadderInteraction(coll);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ladder")
        {
            inputs.isLadderClimbing = false;
            rigidbody2D.gravityScale = 1;
        }

        if (coll.gameObject.tag == "Wall")
        {
            coll.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (coll.gameObject.tag == "Stairs")
        {
            inputs.isStairsClimbing = false;
        }

    }

}
