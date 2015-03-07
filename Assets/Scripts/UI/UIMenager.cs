using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIMenager : MonoBehaviour
{

    public Character _player;
    public Inputs _inputs;

    public Slider HP;
    public Slider MP;
    public Slider Stamina;

    public Sprite d_pad_normal;
    public Sprite d_pad_up;
    public Sprite d_pad_down;
    public Sprite d_pad_left;
    public Sprite d_pad_right;
    public Image d_pad;

    void Start()
    {
        HP.value = HP.maxValue = _player.Health;
        MP.value = MP.maxValue = _player.Mana;
        Stamina.value = Stamina.maxValue = _player.Stamina;
    }

    void Update()
    {
        HP.value = _player.Health;

        if (_inputs.d_pad_x == 1f)
            d_pad.sprite = d_pad_right;
        if (_inputs.d_pad_x == -1f)
            d_pad.sprite = d_pad_left;

        if (_inputs.d_pad_y == 1f)
            d_pad.sprite = d_pad_up;
        if (_inputs.d_pad_y == -1f)
            d_pad.sprite = d_pad_down;

        if (_inputs.d_pad_y == 0f && _inputs.d_pad_x == 0f)
            d_pad.sprite = d_pad_normal;
    }
}
