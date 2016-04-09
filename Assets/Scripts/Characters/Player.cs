using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int _experience;
    public List<Quest> QuestLog = new List<Quest>();

    public int Exp
    {
        get { return _experience; }
        set
        {
            if (value > 0)
            {
                Debug.Log("I get a " + value.ToString() + " experience!");
                _experience += value;
            }
        }
    }

    /// <summary>
    /// Akcje gracza.
    /// </summary>
    override
    protected void Actions()
    {
        if (!Inputs.InGameMenu)
        {
            Jump();

            Move();

            Attack();

            LadderClimb();
        }
    }

    private void Update()
    {
        Inputs.IsGrounded = Physics2D.OverlapCircle(IsOnGround.position, IsOnGroundRadius, Ground);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 30f);
        int enemiesCount = 0;

        foreach (Collider2D coll in enemies)
        {
            if (coll.gameObject.tag == "Enemy")
            {
                ++enemiesCount;
                break;
            }
        }

        if (enemiesCount > 0)
            Inputs.InCombat = true;
        else
            Inputs.InCombat = false;

        if (Inputs.DPadY == 1f)
            SelectedProjectile = 1;

        if (Inputs.DPadY == -1f)
            SelectedProjectile = 2;

        if (Inputs.DPadX == 1f)
            SelectedProjectile = 0;

        Dies();
    }
    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate()
    {
        Actions();
    }

    #region Kolizje

    private void OnCollisionEnter2D(Collision2D coll)
    {
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            foreach (Collider2D c in AllColliders)
                Physics2D.IgnoreCollision(coll.collider, c);
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            coll.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (coll.gameObject.tag == "Stairs")
        {
            Inputs.IsStairsClimbing = true;
        }

        if (coll.tag == "Projectile" && coll.name == "Enemy")
        {
            Health -= coll.GetComponent<Projectile>().projectileDamage;
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        LadderInteraction(coll);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ladder")
        {
            Inputs.IsLadderClimbing = false;
            rigidbody2D.gravityScale = 1;
        }

        if (coll.gameObject.tag == "Wall")
        {
            coll.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (coll.gameObject.tag == "Stairs")
        {
            Inputs.IsStairsClimbing = false;
        }
    }

    #endregion Kolizje
}