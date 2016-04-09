using UnityEngine;


/// <summary>
/// Szwendanie 
/// </summary>
public class Wander : Behavior
{
    /// <summary>
    /// Definicja zachowania.
    /// </summary>
    public override void Behave()
    {
        Collider2D collision = Physics2D.OverlapPoint(Brain.ObstacleDetector.position);

        if (collision != null)
        {
            if (collision.tag == "Ground")
                Brain.inputs.HorizontalInputLeft *= -1;

            if (collision.tag == Brain.Slave.tag)
            {
                foreach (Collider2D coll in Brain.Slave.CollidersList)
                    Physics2D.IgnoreCollision(collision, coll);
            }
        }
    }
}
