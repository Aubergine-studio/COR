using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Character : MonoBehaviour
{
    /*
     *  Dodatkowe elementy związane z sterowaniem postacią. 
     */
    public Transform IsOnGround;                    //  Pozycja Obiektu wykrywajacego ziemie.
    public float IsOnGroundRadius = 0.2f;
    public LayerMask Ground;                    //  Maska pozwalajaca określić co jest "ziemią"

    protected Animator Animator;                //  Animator postaci.   TMP do zastąpienia osobną klasa.

    protected AnimatorController AnimatorController;

    public GameObject[] ProjectileType;
    public Transform ProjectileSpawn;
    protected int SelectedProjectile = 0;
    public int ProjectileIndex
    {
        get { return SelectedProjectile; }
    }

    public CircleCollider2D LegsCollider;
    public BoxCollider2D BodyCollider;
    protected List<Collider2D> AllColliders = new List<Collider2D>();
    public List<Collider2D> CollidersList
    {
        get { return AllColliders; }
    }

    #region Parametry postaci

    public float MaxSpeed = 10f;        //  Maksymalna prędkośc poruszania się. 
    public float JumpForce = 400f;      //  Siła skoku.
    protected Inputs Inputs;            //  Wejścia.

    public float Health = 100.0f;       //  Zdrowie.
    public float Mana = 100.0f;         //  Mana
    public float Stamina = 100.0f;      //  Stamina
    public float AttackSpead = 1f;
    private float _attackTimer = 0f;

    #endregion
    /*
     *  Metody 
     */

    protected void Start()
    {
        //projectileType = new GameObject[2];

        //projectileType[0] = Resources.Load("Hit") as GameObject;
        //projectileType[1] = Resources.Load("Knife") as GameObject;

        Animator = GetComponent<Animator>();
        Inputs = GetComponent<Inputs>();
        AnimatorController = GetComponent<AnimatorController>();
        LegsCollider = GetComponent<CircleCollider2D>();

        CircleCollider2D[] c = gameObject.GetComponents<CircleCollider2D>();
        foreach (Collider2D coll in c)
        {
            AllColliders.Add(coll);
        }
        BoxCollider2D[] b = GetComponents<BoxCollider2D>();

        foreach (Collider2D coll in b)
        {
            AllColliders.Add(coll);
        }
    }

    protected void DisableAllColliders()
    {
        foreach (Collider2D coll in AllColliders)
        {
            coll.enabled = false;
        }
    }

    /*
     * Funkcja obraca postać.
     */

    protected void Flip()
    {
        Inputs.isFacingLeft = !Inputs.isFacingLeft;

        Vector3 Flip = transform.localScale;

        Flip.x *= -1;

        transform.localScale = Flip;
    }

    /*
     * Funkcja ataku.
     */

    protected void Attack()
    {
        if (_attackTimer == 0f)
        {
            if (Inputs.fire)
            {
                Debug.Log("Spawn pocisku.");

                _attackTimer = AttackSpead;

                Projectile clone = (Instantiate(ProjectileType[SelectedProjectile], ProjectileSpawn.position, ProjectileSpawn.localRotation) as GameObject).GetComponent<Projectile>();
                if (Inputs.isFacingLeft)
                {
                    clone.moveDirection = -1.0f;
                    Transform ctransform =  clone.GetComponent<Transform>();
                    ctransform.localScale = new Vector3(ctransform.localScale.x *-1, ctransform.localScale.y, ctransform.localScale.z);

                }
                else
                    clone.moveDirection = 1.0f;

                clone.name = tag;

            }
        }
        else
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer < 0)
                _attackTimer = 0;
            Inputs.fire = false;
        }
    }

    /*
     * Funkcja poruszania się.
     */

    protected void Move()
    {
        if ((Inputs.isGrounded && !Inputs.isLadderClimbing)
                || !Inputs.isLadderClimbing)//&& !(a.nameHash == AttaksHash))                     //   Warunki wywołania akcji poruszania się.
        {

            rigidbody2D.velocity = new Vector2(Inputs.horizontalInput_left * MaxSpeed, rigidbody2D.velocity.y);

            if (Inputs.horizontalInput_left < 0 && !Inputs.isFacingLeft)
                Flip();

            if (Inputs.horizontalInput_left > 0 && Inputs.isFacingLeft)
                Flip();
        }
    }

    /*
     * Funkcja Skoku.
     */

    protected void Jump()
    {
        if (Inputs.jump && Inputs.isGrounded)             //   Warunki wywołania akcji skoku.             
        {

            rigidbody2D.AddForce(new Vector2(0, JumpForce));
            Inputs.jump = false;
        }
    }

    /*
     * Funkcja wspinajaca.
     */


    protected void Climb()
    {
    }

    protected void LadderClimb()
    {
        if (Inputs.isLadderClimbing)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Inputs.verticalInput_left * MaxSpeed);
        }
    }

    /*
     * Metoda odpowiadająca, za sterowanie postacią. 
     */

    protected abstract void Actions();

    /*
     * Metody związane z interakcją. 
     */

    protected void LadderInteraction(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ladder" && Inputs.action && !Inputs.isLadderClimbing)
        {
            if (!Inputs.isLadderClimbing)
            {
                Inputs.isGetOnLadder = true;
            }

            Inputs.isLadderClimbing = true;

            rigidbody2D.gravityScale = 0;

            float offset = 0f;

            if (coll.gameObject.transform.localScale.x > 0)
                offset = 0.5f;
            else
                offset = -0.5f;

            transform.position = new Vector3(coll.gameObject.transform.position.x + offset, transform.position.y, transform.position.z);

            if (coll.gameObject.transform.localScale.x > 0 && transform
                .localScale.x < 0)
                Flip();

            if (coll.gameObject.transform.localScale.x < 0 && transform
                .localScale.x > 0)
                Flip();

        }

        if (coll.gameObject.tag == "Ladder" && !Inputs.action && Inputs.isLadderClimbing)
        {
            rigidbody2D.gravityScale = 1;
            Inputs.isLadderClimbing = false;
            Flip();
        }

    }

    protected void Dies()
    {
        if (Health <= 0)
        {
            DisableAllColliders();
            rigidbody2D.gravityScale = 0;
            rigidbody2D.velocity = Vector2.zero;

            this.enabled = false;
        }

    }
}
