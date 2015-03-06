using UnityEngine;
using System.Collections;

public class AnimalAnimatorController : AnimatorController
{
    protected override void ControlAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(inputs.horizontalInput_left));
        
        if(inputs.fire)
        {
            animator.SetTrigger("Attack");
        }

        if (chracter.Health <= 0)
        {
            animator.SetTrigger("IsDead");
            this.enabled = false;
        }
    }
}
