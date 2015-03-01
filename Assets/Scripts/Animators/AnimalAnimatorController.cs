using UnityEngine;
using System.Collections;

public class AnimalAnimatorController : AnimatorController 
{
    protected override void ControlAnimator()
    {
		if (chracter.Health <= 0) 
		{
			animator.SetTrigger("IsDead");
			this.enabled = false;
		}
    }
}
