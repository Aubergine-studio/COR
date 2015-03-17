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

        inputs.horizontalInput_left = Input.GetAxis("Horizontal");  //  Prawo/lewo
        inputs.verticalInput_left = Input.GetAxis("Vertical");

        inputs.horizontalInput_right = Input.GetAxis("Horizontal_right");  //  Prawo/lewo
        inputs.verticalInput_right = Input.GetAxis("Vertical_right");


        //inputs.horizontalInput = Input.GetAxis("Left analog X");  //  Prawo/lewo
        //inputs.verticalInput = Input.GetAxis("Left analog Y");
        inputs.d_pad_x = Input.GetAxis("D-Pad X");
        inputs.d_pad_y = Input.GetAxis("D-Pad Y");
        
        #endregion

        if (Input.GetButtonDown("Attack") )     //  Atak
            inputs.fire = true;

        if (Input.GetButtonUp("Attack"))
            inputs.fire = false;

        if (Input.GetButtonDown("Jump") )     //  Atak
            inputs.jump = true;

        if (Input.GetButtonUp("Jump"))
            inputs.jump = false;

        if (Input.GetButtonDown("Use"))     //  Atak
            inputs.action  = true;

        if (Input.GetButtonUp("Use"))
            inputs.action = false;

        if (Input.GetButtonDown("InGameMenu"))     //  Atak
            inputs.inGameMenu = !inputs.inGameMenu;
   		
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
