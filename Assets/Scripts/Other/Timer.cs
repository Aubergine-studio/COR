using UnityEngine;

public class Timer
{
    private float _counter = 0;
    private float _interval;

    private bool _running = false;

    public Timer(float interval)
    {
        this._interval = interval;
    }

    public void Start()
    {
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