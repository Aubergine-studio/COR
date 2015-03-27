using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Character
{
    private int experience;
    public List<Quest> questLog =  new List<Quest>();
    
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
        if (!inputs.inGameMenu)
        {
            Jump();

            if (inputs.action && inputs.isClimbing)              //    Warunki wywołania akcji wsponania.
            {
            }

            Move();

            Attack();

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

    void Update()
    {
        inputs.isGrounded = Physics2D.OverlapCircle(isOnGround.position, isOnGroundRadius, Ground);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 30f);
        int enemiesCount = 0;
        foreach (Collider2D coll in enemies)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                ++enemiesCount;
            }
        }

        if (enemiesCount > 0)
            inputs.inCombat = true;
        else
            inputs.inCombat = false;

        if (inputs.d_pad_y == 1f)
            selectedProjectile = 1;
        Dies();
    }

    /*
     * Update wywołujacy się co stały interwał czasowy.
     */

    void FixedUpdate()
    {
        Actions();
    }

    #region Kolizje

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Stairs")
        {
            bodyCollider.enabled = false;
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            foreach (Collider2D c in allColliders)
                Physics2D.IgnoreCollision(coll.collider, c);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Stairs")
        {
            bodyCollider.enabled = true;
        }
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

        if (coll.tag == "Projectile" && coll.name == "Enemy")
        {
            Health -= coll.GetComponent<Projectile>().projectileDamage;
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

    #endregion
}
