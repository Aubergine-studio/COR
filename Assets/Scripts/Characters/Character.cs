using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    #region Sterowanie

    public Transform IsOnGround;                    //  Pozycja Obiektu wykrywajacego ziemie.
    public float IsOnGroundRadius = 0.2f;
    public LayerMask Ground;                    //  Maska pozwalajaca określić co jest "ziemią"

    protected Animator Animator;                //  Animator postaci.   TMP do zastąpienia osobną klasa.

    protected AnimatorController AnimatorController;

    #endregion Sterowanie

    public GameObject[] ProjectileType;
    public Transform ProjectileSpawn;
    protected int SelectedProjectile = 0;

    public int ProjectileIndex
    {
        get { return SelectedProjectile; }
    }

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

    #endregion Parametry postaci

    protected void Start()
    {
        Animator = GetComponent<Animator>();
        Inputs = GetComponent<Inputs>();
        AnimatorController = GetComponent<AnimatorController>();

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

    /// <summary>
    /// Dezaktywacja  wszystkich colliderów
    /// </summary>
    protected void DisableAllColliders()
    {
        foreach (Collider2D coll in AllColliders)
        {
            coll.enabled = false;
        }
    }

    /// <summary>
    /// Funkcja obracająca postać.
    /// </summary>
    protected void Flip()
    {
        Inputs.IsFacingLeft = !Inputs.IsFacingLeft;

        var flip = transform.localScale;

        flip.x *= -1;

        transform.localScale = flip;
    }

    /// <summary>
    /// Atak.
    /// </summary>
    protected void Attack()
    {
        if (_attackTimer == 0f)
        {
            if (Inputs.Fire)
            {
                Debug.Log("Spawn pocisku.");

                _attackTimer = AttackSpead;

                var o = Instantiate(ProjectileType[SelectedProjectile], ProjectileSpawn.position, ProjectileSpawn.localRotation) as GameObject;
                if (o != null)
                {
                    Projectile clone = o.GetComponent<Projectile>();
                    if (Inputs.IsFacingLeft)
                    {
                        clone.moveDirection = -1.0f;
                        Transform ctransform = clone.GetComponent<Transform>();
                        ctransform.localScale = new Vector3(ctransform.localScale.x * -1, ctransform.localScale.y, ctransform.localScale.z);
                    }
                    else
                        clone.moveDirection = 1.0f;

                    clone.name = tag;
                }
            }
        }
        else
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer < 0)
                _attackTimer = 0;
            Inputs.Fire = false;
        }
    }

    #region Interkacje/Akcje

    /// <summary>
    /// Poruszanie się.
    /// </summary>
    protected void Move()
    {
        if ((Inputs.IsGrounded && !Inputs.IsLadderClimbing)
                || !Inputs.IsLadderClimbing)                     //   Warunki wywołania akcji poruszania się.
        {
            rigidbody2D.velocity = new Vector2(Inputs.HorizontalInputLeft * MaxSpeed, rigidbody2D.velocity.y);

            if (Inputs.HorizontalInputLeft < 0 && !Inputs.IsFacingLeft)
                Flip();

            if (Inputs.HorizontalInputLeft > 0 && Inputs.IsFacingLeft)
                Flip();
        }
    }

    /// <summary>
    /// Skok
    /// </summary>
    protected void Jump()
    {
        if (Inputs.Jump && Inputs.IsGrounded)             //   Warunki wywołania akcji skoku.
        {
            rigidbody2D.AddForce(new Vector2(0, JumpForce));
            Inputs.Jump = false;
        }
    }

    /// <summary>
    /// Wspinanie się po drabinie
    /// </summary>
    protected void LadderClimb()
    {
        if (Inputs.IsLadderClimbing)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Inputs.VerticalInputLeft * MaxSpeed);
        }
    }
    /// <summary>
    /// Metoda definiująca zachowanie postaci. Implementowana dla każdego typu postaci(gracz, NPC, przeciwnik).
    /// </summary>
    protected abstract void Actions();

    /// <summary>
    /// Interkacja z drabiną
    /// </summary>
    /// <param name="coll"> Referencja na obiekt, z którym wystąpiła kolizja.</param>
    protected void LadderInteraction(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ladder" && Inputs.Action && !Inputs.IsLadderClimbing)
        {
            if (!Inputs.IsLadderClimbing)
            {
                Inputs.IsGetOnLadder = true;
            }

            Inputs.IsLadderClimbing = true;

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

        if (coll.gameObject.tag == "Ladder" && !Inputs.Action && Inputs.IsLadderClimbing)
        {
            rigidbody2D.gravityScale = 1;
            Inputs.IsLadderClimbing = false;
            Flip();
        }
    }
    /// <summary>
    /// Metoda śmierci postaci.
    /// </summary>
    protected void Dies()
    {
        if (!(Health <= 0)) return;
        DisableAllColliders();
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = Vector2.zero;
        Animator.Play("Dies");
        this.enabled = false;
    }

    #endregion Interkacje/Akcje
}