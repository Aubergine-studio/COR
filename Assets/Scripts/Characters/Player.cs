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
        if (!Inputs.inGameMenu)
        {
            Jump();

            if (Inputs.action && Inputs.isClimbing)              //    Warunki wywołania akcji wsponania.
            {
            }

            Move();

            Attack();

            LadderClimb();
        }
    }

    void ColiderControl()
    {
        if (rigidbody2D.velocity.y < 0 && !Inputs.isGrounded)
        {
            LegsCollider.center = new Vector2(LegsCollider.center.x, -2.8f);
        }

        if (rigidbody2D.velocity.y > 0 && !Inputs.isGrounded)
        {
            LegsCollider.center = new Vector2(LegsCollider.center.x, 0.5f);
        }
    }

    void Update()
    {
        Inputs.isGrounded = Physics2D.OverlapCircle(IsOnGround.position, IsOnGroundRadius, Ground);
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
            Inputs.inCombat = true;
        else
            Inputs.inCombat = false;


        if (Inputs.d_pad_y == 1f)
            SelectedProjectile = 1;

        if (Inputs.d_pad_y == -1f)
            SelectedProjectile = 2;

        if (Inputs.d_pad_x == 1f)
            SelectedProjectile = 0;

        //if (inputs.d_pad_x == -1f)
        //    selectedProjetile = 2;

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
            BodyCollider.enabled = false;
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            foreach (Collider2D c in AllColliders)
                Physics2D.IgnoreCollision(coll.collider, c);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Stairs")
        {
            BodyCollider.enabled = true;
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
            Inputs.isStairsClimbing = true;
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
            Inputs.isLadderClimbing = false;
            rigidbody2D.gravityScale = 1;
        }

        if (coll.gameObject.tag == "Wall")
        {
            coll.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (coll.gameObject.tag == "Stairs")
        {
            Inputs.isStairsClimbing = false;
        }

    }

    #endregion
}
