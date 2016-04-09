using UnityEngine;

/// <summary>
/// Interakcja z graczem
/// </summary>
public class Interaction : Behavior
{
    /// <summary>
    /// Definicja zachowania.
    /// </summary>
    public override void Behave()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, Brain.Sight);


        foreach (Collider2D coll in collisions)
        {
            if (coll.tag == "Player")
            {
                if (Mathf.Abs(coll.transform.position.x - Brain.Slave.ProjectileSpawn.transform.position.x) <=
                Brain.Slave.ProjectileType[Brain.Slave.ProjectileIndex].GetComponent<Projectile>().projectileDistance)
                {
                    Brain.inputs.HorizontalInputLeft = 0;
                    Brain.inputs.Fire = true;
                }
                else
                {
                    if (coll.transform.position.x < Brain.transform.position.x)
                    {
                        Brain.inputs.HorizontalInputLeft = -1f;
                    }

                    if (coll.transform.position.x > Brain.transform.position.x)
                    {
                        Brain.inputs.HorizontalInputLeft = 1f;
                    }
                }
                Debug.Log("Player!");
                break;
            }
        }
    }
}
