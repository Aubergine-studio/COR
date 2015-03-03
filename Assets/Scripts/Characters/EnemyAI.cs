using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private Inputs inputs;
    public Transform  obstacleDetector;
    public float detectorRadius = 0.3f;
    public float sight = 10;
    
    // Use this for initialization
    void Start()
    {
       inputs = GetComponentInParent<Inputs>();
       inputs.horizontalInput = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position,sight);
        Collider2D collision;
        

        foreach(Collider2D coll in collisions)
        {
            if(coll.tag == "Player")
            {
                Debug.Log("Player!");
                inputs.horizontalInput = 1f;
            }
        }
        collision = Physics2D.OverlapPoint(obstacleDetector.position);
        if (collision != null)
        {
            if (collision.tag == "Ground")
                inputs.horizontalInput *= -1;
        }
    }
}
