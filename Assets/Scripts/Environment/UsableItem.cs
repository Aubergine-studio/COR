using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : MonoBehaviour
{
    protected static List<GameObject> players = new List<GameObject>();
    protected static List<Character> _players = new List<Character>();
    protected static List<Inputs> playersInputs = new List<Inputs>();

    protected void Start()
    {
        if (players.Count == 0)
            foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Player"))
            {
                players.Add(gobj);
                _players.Add(gobj.GetComponent<Player>());
                playersInputs.Add(gobj.GetComponent<Inputs>());
            }
    }

}
