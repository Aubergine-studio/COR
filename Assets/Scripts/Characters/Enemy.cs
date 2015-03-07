using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Character
{
    private static List<Player> players = new List<Player>();

    public int experience = 10;


    new void Start()
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

    void Update()
    {
        Actions();

        if (Health <= 0)
            foreach (Player p in players)
            {
                p.Exp = experience;
            }

        inputs.isGrounded = Physics2D.OverlapCircle(isOnGround.position, 0.2f, Ground);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.tag == "Projectile" && coll.name == "Player")
        {
            Health -= coll.GetComponent<Projectile>().projectileDamage;
        }
    }

}
