using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static List<Player> Players = new List<Player>();
    public static List<Inputs> Inputs = new List<Inputs>();

    protected void Start()
    {
        if (Players.Count == 0)
            foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Player"))
            {
                Players.Add(gobj.GetComponent<Player>());
                Inputs.Add(gobj.GetComponent<Inputs>());
            }
    }

    public void PlayerInteraction(Collider2D other)
    {
            if (other.tag == "Player" && other.GetComponent<Inputs>().Action)
                foreach (Quest q in Players[0].QuestLog)
                {
                    q.UpdateQuest(this.gameObject);
                }
    }
}