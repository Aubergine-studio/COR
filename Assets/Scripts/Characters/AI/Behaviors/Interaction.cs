using UnityEngine;
using System.Collections;

public class Interaction : Behavior
{
    public override void Behave()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, brain.sight);


        foreach (Collider2D coll in collisions)
        {
            if (coll.tag == "Player")
            {
                if (Mathf.Abs(coll.transform.position.x - brain.slave.projectileSpawn.transform.position.x) <=
                brain.slave.projectileType[brain.slave.projectileIndex].GetComponent<Projectile>().projectileDistance)
                {
                    brain.inputs.horizontalInput_left = 0;
                    brain.inputs.fire = true;
                }
                else
                {
                    if (coll.transform.position.x < brain.transform.position.x)
                    {
                        brain.inputs.horizontalInput_left = -1f;
                    }

                    if (coll.transform.position.x > brain.transform.position.x)
                    {
                        brain.inputs.horizontalInput_left = 1f;
                    }
                }
                Debug.Log("Player!");
                break;
            }
        }
    }
}
