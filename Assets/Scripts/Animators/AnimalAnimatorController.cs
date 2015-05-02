using UnityEngine;
using System.Collections;

public class AnimalAnimatorController : AnimatorController
{
    protected override void ControlAnimator()
    {
        Animator.SetFloat("Speed", Mathf.Abs(Inputs.horizontalInput_left));
        
        if(Inputs.fire)
        {
            Animator.SetTrigger("Attack");
        }

        if (Chracter.Health <= 0)
        {
            Animator.SetTrigger("IsDead");
            this.enabled = false;
        }
    }
}
