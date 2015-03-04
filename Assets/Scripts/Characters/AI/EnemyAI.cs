using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    private Character mySlave;
    public Character slave
    {
        get { return mySlave; }
    }
    private Inputs _inputs;
    public Inputs inputs
    {
        get { return _inputs; }
    }
    public Transform obstacleDetector;
    public float detectorRadius = 0.3f;
    public float sight = 10;

    private Behavior[] behaviors;

    // Use this for initialization
    void Start()
    {
        _inputs = GetComponentInParent<Inputs>();
        inputs.horizontalInput = 0.4f;
        mySlave = GetComponentInParent<Enemy>();

        behaviors = GetComponents<Behavior>();
        foreach (Behavior b in behaviors)
        {
            b.Initialize(this);
        }
    }

    void Update()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, sight);


        foreach (Collider2D coll in collisions)
        {
            if (coll.tag == "Player")
            {
                Debug.Log("Player!");
                inputs.horizontalInput = 1f;
                break;
            }
        }

        foreach (Behavior b in behaviors)
        {
            b.Behave();
        }
    }
}
