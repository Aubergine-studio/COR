using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Przeciwnik
/// </summary>
public class Enemy : Character
{
    private static readonly List<Player> players = new List<Player>();

    public int Experience = 10;

    private new void Start()
    {
        base.Start();
        if (players.Count == 0)
            foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Player"))
            {
                players.Add(gobj.GetComponent<Player>());
            }
    }
    /// <summary>
    /// Zacgiwabue siê przeciwnika.
    /// </summary>
    override
    protected void Actions()
    {
        Move();
        Attack();
        Dies();
    }

    private void Update()
    {
        Actions();

        if (Health <= 0)
            foreach (Player p in players)
            {
                p.Exp = Experience;
            }

        Inputs.IsGrounded = Physics2D.OverlapCircle(IsOnGround.position, 0.2f, Ground);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Projectile" && coll.name == "Player")
        {
            Health -= coll.GetComponent<Projectile>().projectileDamage;
        }
    }
    /// <summary>
    /// Œmieræ przeciwnika.
    /// </summary>
    private new void Dies()
    {
        base.Dies();
        if (Health <= 0)
            foreach (Quest q in players[0].QuestLog)
            {
                q.UpdateQuest(this.gameObject);
            }
    }
}