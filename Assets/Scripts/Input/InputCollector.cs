using UnityEngine;

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

        _inputs.HorizontalInputLeft = Input.GetAxis("Horizontal");  //  Prawo/lewo
        _inputs.VerticalInputLeft = Input.GetAxis("Vertical");

        _inputs.HorizontalInputRight = Input.GetAxis("Horizontal_right");  //  Prawo/lewo
        _inputs.VerticalInputRight = Input.GetAxis("Vertical_right");


        //inputs.horizontalInput = Input.GetAxis("Left analog X");  //  Prawo/lewo
        //inputs.verticalInput = Input.GetAxis("Left analog Y");
        _inputs.DPadX = Input.GetAxis("D-Pad X");
        _inputs.DPadY = Input.GetAxis("D-Pad Y");
        
        #endregion

        if (Input.GetButtonDown("Attack") )     //  Atak
            _inputs.Fire = true;

        if (Input.GetButtonUp("Attack"))
            _inputs.Fire = false;

        if (Input.GetButtonDown("Jump") )     //  Atak
            _inputs.Jump = true;

        if (Input.GetButtonUp("Jump"))
            _inputs.Jump = false;

        if (Input.GetButtonDown("Use"))     //  Atak
            _inputs.Action  = true;

        if (Input.GetButtonUp("Use"))
            _inputs.Action = false;

        if (Input.GetButtonDown("InGameMenu"))     //  Atak
            _inputs.InGameMenu = !_inputs.InGameMenu;
   		
		if (Input.GetKeyDown(KeyCode.Mouse0))      //  Atak
			_inputs.Fire = true;
		
        if (Input.GetKeyUp(KeyCode.Mouse0))
			_inputs.Fire = false;
		
		
		if (Input.GetKeyDown(KeyCode.E))     //  Akcja
			_inputs.Action = true;
		
		if (Input.GetKeyUp(KeyCode.E))
			_inputs.Action = false;
		
		
		if (Input.GetKeyDown(KeyCode.Space))     //  Skok.
			_inputs.Jump = true;
		
		if (Input.GetKeyUp(KeyCode.Space))     
			_inputs.Jump = false;
		
		#endregion
		
		/*
         * Wejścia analogowe.
         */
		
	}
}
