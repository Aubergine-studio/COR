using UnityEngine;
using System.Collections;

public class InputCollector : MonoBehaviour
{

	private Inputs _inputs;

	// Use this for initialization
	void Start ()
	{
		_inputs = gameObject.GetComponent<Inputs> ();
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

        _inputs.horizontalInput_left = Input.GetAxis("Horizontal");  //  Prawo/lewo
        _inputs.verticalInput_left = Input.GetAxis("Vertical");

        _inputs.horizontalInput_right = Input.GetAxis("Horizontal_right");  //  Prawo/lewo
        _inputs.verticalInput_right = Input.GetAxis("Vertical_right");


        //inputs.horizontalInput = Input.GetAxis("Left analog X");  //  Prawo/lewo
        //inputs.verticalInput = Input.GetAxis("Left analog Y");
        _inputs.d_pad_x = Input.GetAxis("D-Pad X");
        _inputs.d_pad_y = Input.GetAxis("D-Pad Y");
        
        #endregion

        if (Input.GetButtonDown("Attack") )     //  Atak
            _inputs.fire = true;

        if (Input.GetButtonUp("Attack"))
            _inputs.fire = false;

        if (Input.GetButtonDown("Jump") )     //  Atak
            _inputs.jump = true;

        if (Input.GetButtonUp("Jump"))
            _inputs.jump = false;

        if (Input.GetButtonDown("Use"))     //  Atak
            _inputs.action  = true;

        if (Input.GetButtonUp("Use"))
            _inputs.action = false;

        if (Input.GetButtonDown("InGameMenu"))     //  Atak
            _inputs.inGameMenu = !_inputs.inGameMenu;
   		
		if (Input.GetKeyDown(KeyCode.Mouse0))      //  Atak
			_inputs.fire = true;
		
        if (Input.GetKeyUp(KeyCode.Mouse0))
			_inputs.fire = false;
		
		
		if (Input.GetKeyDown(KeyCode.E))     //  Akcja
			_inputs.action = true;
		
		if (Input.GetKeyUp(KeyCode.E))
			_inputs.action = false;
		
		
		if (Input.GetKeyDown(KeyCode.Space))     //  Skok.
			_inputs.jump = true;
		
		if (Input.GetKeyUp(KeyCode.Space))     
			_inputs.jump = false;
		
		#endregion
		
		/*
         * Wejścia analogowe.
         */
		
	}
}
