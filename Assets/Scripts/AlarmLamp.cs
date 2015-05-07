using UnityEngine;

public class AlarmLamp : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public int Speed = 1;
    private bool _fadeInOut = true;

    // Use this for initialization
    private void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {  
        if (_spriteRenderer.color.a < 0)
            _fadeInOut = false;
        if (_spriteRenderer.color.a > 255)
            _fadeInOut = true;

        if (_fadeInOut)
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - (Speed*Time.deltaTime));
        else
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a + (Speed * Time.deltaTime));
    }
}