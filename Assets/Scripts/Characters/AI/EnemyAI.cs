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
        inputs.horizontalInput_left = 0f;
        mySlave = GetComponentInParent<Enemy>();

        behaviors = GetComponents<Behavior>();
        foreach (Behavior b in behaviors)
        {
            b.Initialize(this);
        }
    }

    void Update()
    {
        foreach (Behavior b in behaviors)
        {
            b.Behave();
        }
    }
}
