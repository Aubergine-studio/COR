using UnityEngine;
using System.Collections;

public class HumanoidAnimatorController : AnimatorController
{
	public float attack = 	0.0f;		//	Zmienna determinujaca animacje ataku.
	private AnimatorStateInfo animationStatus;

	int AttaksHash = Animator.StringToHash("Base Layer.Attacks");

	
	protected override void ControlAnimator ()
	{
		animationStatus = animator.GetCurrentAnimatorStateInfo(0);

		if(inputs.fire && AttaksHash != animationStatus.nameHash)
		{
			attack = Random.Range(1f, 2f);
		}

		animator.SetBool ("Ground", inputs.isGrounded);
		animator.SetBool ("Fire", inputs.fire);
		animator.SetFloat ("Attack", attack);
		animator.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		animator.SetFloat ("Speed", Mathf.Abs(inputs.horizontalInput));
	}

	public bool GetAttackStatus()
	{
		return AttaksHash == animationStatus.nameHash ? true : false;
	}
}
