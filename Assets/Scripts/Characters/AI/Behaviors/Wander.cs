using UnityEngine;
using System.Collections;

public class Wander : Behavior
{
    public override void Behave()
    {
        Collider2D collision = Physics2D.OverlapPoint(brain.obstacleDetector.position);

        if (collision != null)
        {
            if (collision.tag == "Ground")
                brain.inputs.horizontalInput_left *= -1;

            if (collision.tag == brain.slave.tag)
            {
                foreach (Collider2D coll in brain.slave.CollidersList)
                    Physics2D.IgnoreCollision(collision, coll);
            }
        }
    }
}
