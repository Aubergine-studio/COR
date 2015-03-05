using UnityEngine;
using System.Collections;

public class InputCollector : MonoBehaviour
{

	private Inputs inputs;

	// Use this for initialization
	void Start ()
	{
		inputs = gameObject.GetComponent<Inputs> ();
	}
	
	// Update is called once per frame
    void FixedUpdate()
	{
		GetInputs ();
	}

	public void GetInputs()
	{
		/*
         *  Wejścia z przycisków
         */
		
		#region ControllerButtons

        #region AnallogInputs

        inputs.horizontalInput = Input.GetAxis("Horizontal");  //  Prawo/lewo
        inputs.verticalInput = Input.GetAxis("Vertical");

        //inputs.horizontalInput = Input.GetAxis("Left analog X");  //  Prawo/lewo
        //inputs.verticalInput = Input.GetAxis("Left analog Y");
        inputs.d_pad_x = Input.GetAxis("D-Pad X");
        inputs.d_pad_y = Input.GetAxis("D-Pad Y");

        #endregion

        
		if (Input.GetKeyDown(KeyCode.JoystickButton1))      //  Atak
			inputs.fire = true;
		
		if (Input.GetKeyUp(KeyCode.JoystickButton1))
			inputs.fire = false;
		
		if (Input.GetKeyDown(KeyCode.Joystick1Button2))     //  Akcja
			inputs.action = true;
		
		if (Input.GetKeyUp(KeyCode.Joystick1Button2))
			inputs.action = false;
		
		if (Input.GetKeyDown(KeyCode.Joystick1Button0))      //  Skok.
			inputs.jump = true;
		
		if (Input.GetKeyUp(KeyCode.Joystick1Button0))     
			inputs.jump = false;
		
		/*
		 * TMP
		 */
		
		if (Input.GetKeyDown(KeyCode.Mouse0))      //  Atak
			inputs.fire = true;
		
        if (Input.GetKeyUp(KeyCode.Mouse0))
			inputs.fire = false;
		
		
		if (Input.GetKeyDown(KeyCode.E))     //  Akcja
			inputs.action = true;
		
		if (Input.GetKeyUp(KeyCode.E))
			inputs.action = false;
		
		
		if (Input.GetKeyDown(KeyCode.Space))     //  Skok.
			inputs.jump = true;
		
		if (Input.GetKeyUp(KeyCode.Space))     
			inputs.jump = false;
		
		#endregion
		
		/*
         * Wejścia analogowe.
         */
		
	}
}
