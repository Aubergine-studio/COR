using UnityEngine;

public class blueberryBushScript : Item
{
    public Sprite FoolSprite;
    public Sprite EmptySprite;
    private bool _isFoll = true;
    private SpriteRenderer _renderer;
    private Timer timer = new Timer(15f);

    private new void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = FoolSprite;
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        //if (!_isFoll)
        //{
        //    if (_renderer.sprite != EmptySprite)
        //        _renderer.sprite = EmptySprite;
        //    timer.Start();
        //}

        if (timer.Count())
        {
            _renderer.sprite = FoolSprite;
            _isFoll = true;
            timer.Stop();
        }
    }

    private new void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        if (other.GetComponent<Inputs>().action)
        {
            _isFoll = false;
            _renderer.sprite = EmptySprite;
            timer.Start();
        }
    }
}