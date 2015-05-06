using UnityEngine;
using System.Collections;

public class BanerDrop : MonoBehaviour
{
    private Timer t;
    public float TimeOfDrop = 15f;
    private Rigidbody2D _banerRigidbody2D;
    // Use this for initialization
    void Start()
    {
        _banerRigidbody2D = this.GetComponent<Rigidbody2D>();
        t = new Timer(TimeOfDrop);
        t.Start();
    }

    void FixedUpdate()
    {
        if (t.Count())
            _banerRigidbody2D.gravityScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
