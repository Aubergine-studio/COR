using UnityEngine;
using System.Collections;

public class Inputs : MonoBehaviour 
{

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
	public float attack = 	0.0f;		//	Zmienna determinujaca animacje ataku.
    public bool action  =   false;      //  Flaga akcji.


    public void GetInputs()
    {
        /*
         *  Wejścia z przycisków
         */

        #region ControllerButtons

        if (Input.GetKeyDown(KeyCode.JoystickButton1))      //  Atak
            fire = true;
        
        if (Input.GetKeyUp(KeyCode.JoystickButton1))
		{
            fire = false;
			attack = Random.Range (1f, 2f);
		}
        
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))     //  Akcja
            action = true;
        
        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
            action = false;
    
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))      //  Skok.
            jump = true;

        if (Input.GetKeyUp(KeyCode.Joystick1Button0))     
            jump = false;

		/*
		 * TMP
		 */
		/*
		if (Input.GetKeyDown(KeyCode.S))      //  Atak
			fire = true;
		
		if (Input.GetKeyUp(KeyCode.S))
		{
			fire = false;
			attack = Random.Range (1f, 2f);
		}


		if (Input.GetKeyDown(KeyCode.A))     //  Akcja
			action = true;
		
		if (Input.GetKeyUp(KeyCode.A))
			action = false;


		if (Input.GetKeyDown(KeyCode.Space))      //  Skok.
			jump = true;
		
		if (Input.GetKeyUp(KeyCode.Space))     
			jump = false;

*/


        #endregion

        /*
         * Wejścia analogowe.
         */

        #region AnallogInputs

        horizontalInput = Input.GetAxis("Horizontal");  //  Prawo/lewo

        #endregion
    }
}
