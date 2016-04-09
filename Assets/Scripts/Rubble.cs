using System.Collections.Generic;
using UnityEngine;

public class Rubble : MonoBehaviour
{
    public GameObject colCollider;
    private BoxCollider2D col;
    // Use this for initialization
    private void Start()
    {
        col = colCollider.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        col.enabled = false;
    }
}