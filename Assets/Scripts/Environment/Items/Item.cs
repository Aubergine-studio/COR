using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public static List<Player> players = new List<Player>();
    public static List<Inputs> inputs = new List<Inputs>();

    void Start()
    {
        if (players.Count == 0)
            foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Player"))
            {
                players.Add(gobj.GetComponent<Player>());
                inputs.Add(gobj.GetComponent<Inputs>());
            }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Inputs>().action)
                foreach (Quest q in players[0].questLog)
                {
                    q.UpdateQuest(this.gameObject);
                }
        }

    }

}
