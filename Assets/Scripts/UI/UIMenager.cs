using UnityEngine;
using UnityEngine.UI;

public class UiMenager : MonoBehaviour
{
    public Inputs Inputs;
    public Character Player;
    public Image DPad;
    public Sprite DPadDown;
    public Sprite DPadLeft;
    public Sprite DPadNormal;
    public Sprite DPadRight;
    public Sprite DPadUp;
    public Slider Hp;
    public GameObject InGameMenu;
    public Slider Mp;
    public Slider Stamina;

    private void Start()
    {
        Hp.value = Hp.maxValue = Player.Health;
        Mp.value = Mp.maxValue = Player.Mana;
        Stamina.value = Stamina.maxValue = Player.Stamina;
    }

    private void Update()
    {
        Hp.value = Player.Health;

        if (Inputs.d_pad_x == 1f)
            DPad.sprite = DPadRight;
        if (Inputs.d_pad_x == -1f)
            DPad.sprite = DPadLeft;

        if (Inputs.d_pad_y == 1f)
            DPad.sprite = DPadUp;
        if (Inputs.d_pad_y == -1f)
            DPad.sprite = DPadDown;

        if (Inputs.d_pad_y == 0f && Inputs.d_pad_x == 0f)
            DPad.sprite = DPadNormal;

        InGameMenu.SetActive(Inputs.inGameMenu);
    }
}