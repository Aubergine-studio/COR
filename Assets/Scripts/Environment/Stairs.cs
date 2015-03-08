using UnityEngine;
using System.Collections;

public class Stairs : UsableItem
{
    private Collider2D stairsCollider;
    public Transform dezactiveTrigger;
    public GameObject ActionButtonIcon;

    new void Start()
    {
        base.Start();
        stairsCollider = GetComponent<PolygonCollider2D>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].name == coll.name)
                {
                    if (playersInputs[i].action)
                    {
                        stairsCollider.enabled = true;
                    }
                    break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (stairsCollider.enabled) stairsCollider.enabled = false;
        }

        ActionButtonIcon.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        ActionButtonIcon.SetActive(false);
    }

    void Update()
    {
        foreach (GameObject p in players)
        {
            if (p.transform.position.y > transform.position.y && !stairsCollider.enabled)
            {
                stairsCollider.enabled = true;
            }
        }

        Collider2D c = Physics2D.OverlapPoint(dezactiveTrigger.position);

        if (c != null && c.gameObject.tag == "Player")
        {
            stairsCollider.enabled = false;
        }
    }
}
