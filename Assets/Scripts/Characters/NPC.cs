public class NPC : Character
{
	protected override void Actions ()
	{
		Inputs.IsGrounded = true;
	}

	void Update()
	{
		Actions ();
	}
}
