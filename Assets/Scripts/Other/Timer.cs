using UnityEngine;

public class Timer
{
    private float _counter = 0;
    private float _interval;

    public float Interval
    {
        set
        {
            this.Stop();
            _interval = Mathf.Abs(value);
        }
    }

    private bool _running = false;

    public Timer()
    {
        _interval = 0;
    }

    public Timer(float interval)
    {
        this._interval = interval;
    }

    public void Start()
    {
        if (_interval > 0)
            _running = true;
    }

    public void Stop()
    {
        _counter = 0;
        _running = false;
    }

    public bool Count()
    {
        if (_running)
        {
            if (_counter >= _interval)
            {
                _counter = 0;
                return true;
            }
            else
            {
                _counter += Time.deltaTime;
            }
        }
        return false;
    }
}