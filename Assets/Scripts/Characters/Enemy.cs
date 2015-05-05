using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private static List<Player> players = new List<Player>();

    public int experience = 10;

    private new void Start()
    {
        base.Start();
        if (players.Count == 0)
            foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Player"))
            {
                players.Add(gobj.GetComponent<Player>());
            }
    }

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
                p.Exp = experience;
            }

        Inputs.isGrounded = Physics2D.OverlapCircle(IsOnGround.position, 0.2f, Ground);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Projectile" && coll.name == "Player")
        {
            Health -= coll.GetComponent<Projectile>().projectileDamage;
        }
    }

    private new void Dies()
    {
        base.Dies();
        if (Health <= 0)
            foreach (Quest q in players[0].questLog)
            {
                q.UpdateQuest(this.gameObject);
            }
    }
}