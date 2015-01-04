using UnityEngine;
using System.Collections;

public class Inputs : MonoBehaviour 
{
	
	public bool isFacingRight  		= true;        	//  Obracanie postaci.
	public bool isGrounded     		= false;       	//  Czy postać jest uziemiona?
	public bool isClimbing       	= false;       	//  Gotowość postaci do wspinaczki

    public bool isGetOnLadder       = false;
    public bool isLadderClimbing    = false;        //  Wspinaczka po drabinie.

    /*
     * Odczyt analogowy - Poruszanie się
     */

    public float horizontalInput;       //  Odczyt gałki analogowej mówiący o tym jak mocno została ona wychylona w poziomie.
    public float verticalInput;         //  Odczyt gałki analogowej mówiący o tym jak mocno została ona wychylona w pionie.

    /*
     * Flagi, klawiszy.
     */

    public bool jump    =   false;      //  Flaga skoku.
    public bool fire    =   false;      //  Flaga ataku(w ręcz, na dystns).
    public bool action  =   false;      //  Flaga akcji.
}
