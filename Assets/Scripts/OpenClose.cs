using UnityEngine;

public class OpenClose : MonoBehaviour
{
    private Animator _anim;
    private Timer t = new Timer();
    private bool _open = false;
    public float MaxInterval = 0f;
    public float MinInterval = 10f;

    // Use this for initialization
    private void Start()
    {
        _anim = this.GetComponent<Animator>();
        t.Interval = Random.Range(MinInterval, MaxInterval);
        t.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        if (t.Count())
        {
            t.Stop();
            t.Interval = Random.Range(MinInterval, MaxInterval);
            if (_open)
            {
                _anim.SetTrigger("Close");
            }
            else
            {
                _anim.SetTrigger("Open");
            }
            _open = !_open;
            t.Start();
        }
    }
}