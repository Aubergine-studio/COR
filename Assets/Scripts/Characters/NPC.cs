using UnityEngine;
using System.Collections;

public class NPC : Character
{
	protected override void Actions ()
	{
		inputs.isGrounded = true;
	}

	void Update()
	{
		Actions ();
	}
}
