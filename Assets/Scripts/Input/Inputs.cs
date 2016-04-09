using UnityEngine;
/// <summary>
/// Klasa przechowująca informacje o wejściach.
/// Za jej pośrednictwem odbywa kierowanie się postacią.
/// </summary>
public class Inputs : MonoBehaviour
{

    public bool IsFacingLeft = true;        //  Obracanie postaci.
    public bool IsGrounded = false;       	//  Czy postać jest uziemiona?
    public bool IsClimbing = false;       	//  Gotowość postaci do wspinaczki

    public bool IsGetOnLadder = false;
    public bool IsLadderClimbing = false;   //  Wspinaczka po drabinie.

    public bool IsStairsClimbing = false;

    public bool InCombat = false;

    /*
     * Odczyt analogowy - Poruszanie się
     */

    public float HorizontalInputLeft;       //  Odczyt gałki analogowej mówiący o tym jak mocno została ona wychylona w poziomie.
    public float VerticalInputLeft;         //  Odczyt gałki analogowej mówiący o tym jak mocno została ona wychylona w pionie.
    public float HorizontalInputRight;       //  Odczyt gałki analogowej mówiący o tym jak mocno została ona wychylona w poziomie.
    public float VerticalInputRight;         //  Odczyt gałki analogowej mówiący o tym jak mocno została ona wychylona w pionie.

    public float DPadX;
    public float DPadY;
    /*
     * Flagi, klawiszy.
     */

    public bool Jump = false;      //  Flaga skoku.
    public bool Fire = false;      //  Flaga ataku(w ręcz, na dystns).
    public bool Action = false;    //  Flaga akcji.
    public bool InGameMenu = false;
}
