using UnityEngine;
using System.Collections;

public class NPC : Character
{
	protected override void Actions ()
	{
		Inputs.isGrounded = true;
	}

	void Update()
	{
		Actions ();
	}
}
