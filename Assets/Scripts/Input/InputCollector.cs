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
	void Update ()
	{
		GetInputs ();
	}

	public void GetInputs()
	{
		/*
         *  Wejścia z przycisków
         */
		
		#region ControllerButtons
		
		if (Input.GetKeyDown(KeyCode.JoystickButton1))      //  Atak
			inputs.fire = true;
		
		if (Input.GetKeyUp(KeyCode.JoystickButton1))
		{
			inputs.fire = false;
		}
		
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
		
		if (Input.GetKeyDown(KeyCode.S))      //  Atak
			inputs.fire = true;
		
		if (Input.GetKeyUp(KeyCode.S))
		{
			inputs.fire = false;
		}
		
		
		if (Input.GetKeyDown(KeyCode.A))     //  Akcja
			inputs.action = true;
		
		if (Input.GetKeyUp(KeyCode.A))
			inputs.action = false;
		
		
		if (Input.GetKeyDown(KeyCode.Space))     //  Skok.
			inputs.jump = true;
		
		if (Input.GetKeyUp(KeyCode.Space))     
			inputs.jump = false;
		
		#endregion
		
		/*
         * Wejścia analogowe.
         */
		
		#region AnallogInputs
		
		inputs.horizontalInput = Input.GetAxis("Horizontal");  //  Prawo/lewo
        inputs.verticalInput = Input.GetAxis("Vertical");
		
		#endregion
	}
}
