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
            fire = false;
        
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))     //  Akcja
            action = true;
        
        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
            action = false;
    
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))      //  Skok.
            jump = true;

        if (Input.GetKeyUp(KeyCode.Joystick1Button0))     
            jump = false;

        #endregion

        /*
         * Wejścia analogowe.
         */

        #region AnallogInputs

        horizontalInput = Input.GetAxis("Horizontal");  //  Prawo/lewo

        #endregion
    }
}
