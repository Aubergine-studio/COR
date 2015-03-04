using UnityEngine;
using System.Collections;

public class Wander : Behavior
{

    // Use this for initialization
    void Start()
    {

    }

    public override void Behave()
    {
        Collider2D collision = Physics2D.OverlapPoint(brain.obstacleDetector.position);

        if (collision != null)
        {
            if (collision.tag == "Ground")
                brain.inputs.horizontalInput *= -1;

            if (collision.tag == brain.tag)
            {
                foreach (Collider2D coll in brain.slave.collidersList)
                    Physics2D.IgnoreCollision(collision, coll);
            }
        }
    }
}
