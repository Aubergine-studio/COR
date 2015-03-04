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
    private int selectedProjectile = 0;

    public CircleCollider2D legsCollider;
    public BoxCollider2D bodyCollider;
    private List<Collider2D> allColliders = new List<Collider2D>();
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

        Projectile clone = (Instantiate(projectileType[selectedProjectile], projectileSpawn.position, projectileSpawn.localRotation) as GameObject).GetComponent<Projectile>();

        if (inputs.isFacingLeft)
            clone.moveDirection = -1.0f;
        else
            clone.moveDirection = 1.0f;

        inputs.fire = false;
    }

    /*
     * Funkcja poruszania się.
     */

    protected void Move()
    {
        rigidbody2D.velocity = new Vector2(inputs.horizontalInput * maxSpeed, rigidbody2D.velocity.y);

        if (inputs.horizontalInput < 0 && !inputs.isFacingLeft)
            Flip();

        if (inputs.horizontalInput > 0 && inputs.isFacingLeft)
            Flip();
    }

    /*
     * Funkcja Skoku.
     */

    protected void Jump()
    {
        rigidbody2D.AddForce(new Vector2(0, jumpForce));
        inputs.jump = false;
    }

    /*
     * Funkcja wspinajaca.
     */


    protected void Climb()
    {
    }

    protected void LadderClimb()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, inputs.verticalInput * maxSpeed);
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
    /*
     * Funkcje kolizji 
     */
}
