using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour
{
    /*
     *  Dodatkowe elementy związane z sterowaniem postacią. 
     */
    public Transform isOnGround;                    //  Pozycja Obiektu wykrywajacego ziemie.
    public float isOnGroundRadius = 0.2f;
    public LayerMask Ground;                    //  Maska pozwalajaca określić co jest "ziemią"

    protected Animator animator;                //  Animator postaci.   TMP do zastąpienia osobną klasa.

    protected AnimatorController animatorController;

    public GameObject[] projectileType;
    public Transform projectileSpawn;
    protected int selectedProjectile = 0;
    public int projectileIndex
    {
        get { return selectedProjectile; }
    }

    public CircleCollider2D legsCollider;
    public BoxCollider2D bodyCollider;
    protected List<Collider2D> allColliders = new List<Collider2D>();
    public List<Collider2D> collidersList
    {
        get { return allColliders; }
    }

    #region Parametry postaci

    public float maxSpeed = 10f;        //  Maksymalna prędkośc poruszania się. 
    public float jumpForce = 400f;      //  Siła skoku.
    protected Inputs inputs;            //  Wejścia.

    public float Health = 100.0f;       //  Zdrowie.
    public float Mana = 100.0f;         //  Mana
    public float Stamina = 100.0f;      //  Stamina
    public float attackSpead = 1f;
    private float attackTimer = 0f;

    #endregion
    /*
     *  Metody 
     */

    protected void Start()
    {
        animator = GetComponent<Animator>();
        inputs = GetComponent<Inputs>();
        animatorController = GetComponent<AnimatorController>();
        legsCollider = GetComponent<CircleCollider2D>();

        CircleCollider2D[] c = gameObject.GetComponents<CircleCollider2D>();
        foreach (Collider2D coll in c)
        {
            allColliders.Add(coll);
        }
        BoxCollider2D[] b = GetComponents<BoxCollider2D>();

        foreach (Collider2D coll in b)
        {
            allColliders.Add(coll);
        }
    }

    protected void DisableAllColliders()
    {
        foreach (Collider2D coll in allColliders)
        {
            coll.enabled = false;
        }
    }

    /*
     * Funkcja obraca postać.
     */

    protected void Flip()
    {
        inputs.isFacingLeft = !inputs.isFacingLeft;

        Vector3 Flip = transform.localScale;

        Flip.x *= -1;

        transform.localScale = Flip;
    }

    /*
     * Funkcja ataku.
     */

    protected void Attack()
    {
        if (attackTimer == 0f)
        {
            if (inputs.fire)
            {
                attackTimer = attackSpead;
                Projectile clone = (Instantiate(projectileType[selectedProjectile], projectileSpawn.position, projectileSpawn.localRotation) as GameObject).GetComponent<Projectile>();

                if (inputs.isFacingLeft)
                    clone.moveDirection = -1.0f;
                else
                    clone.moveDirection = 1.0f;
                clone.name = tag;

                inputs.fire = false;
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
                attackTimer = 0;
        }
    }

    /*
     * Funkcja poruszania się.
     */

    protected void Move()
    {
        if ((inputs.isGrounded && !inputs.isLadderClimbing)
                || !inputs.isLadderClimbing)//&& !(a.nameHash == AttaksHash))                     //   Warunki wywołania akcji poruszania się.
        {

            rigidbody2D.velocity = new Vector2(inputs.horizontalInput_left * maxSpeed, rigidbody2D.velocity.y);

            if (inputs.horizontalInput_left < 0 && !inputs.isFacingLeft)
                Flip();

            if (inputs.horizontalInput_left > 0 && inputs.isFacingLeft)
                Flip();
        }
    }

    /*
     * Funkcja Skoku.
     */

    protected void Jump()
    {
        if (inputs.jump && inputs.isGrounded)             //   Warunki wywołania akcji skoku.             
        {

            rigidbody2D.AddForce(new Vector2(0, jumpForce));
            inputs.jump = false;
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
        if (inputs.isLadderClimbing)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, inputs.verticalInput_left * maxSpeed);
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
        if (coll.gameObject.tag == "Ladder" && inputs.action && !inputs.isLadderClimbing)
        {
            if (!inputs.isLadderClimbing)
            {
                inputs.isGetOnLadder = true;
            }

            inputs.isLadderClimbing = true;

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

        if (coll.gameObject.tag == "Ladder" && !inputs.action && inputs.isLadderClimbing)
        {
            rigidbody2D.gravityScale = 1;
            inputs.isLadderClimbing = false;
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
