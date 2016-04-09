using UnityEngine;
using UnityEngine.UI;

public class UIMenager : MonoBehaviour
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

        if (Inputs.DPadX == 1f)
            DPad.sprite = DPadRight;
        if (Inputs.DPadX == -1f)
            DPad.sprite = DPadLeft;

        if (Inputs.DPadY == 1f)
            DPad.sprite = DPadUp;
        if (Inputs.DPadY == -1f)
            DPad.sprite = DPadDown;

        if (Inputs.DPadY == 0f && Inputs.DPadX == 0f)
            DPad.sprite = DPadNormal;

        InGameMenu.SetActive(Inputs.InGameMenu);
    }
}