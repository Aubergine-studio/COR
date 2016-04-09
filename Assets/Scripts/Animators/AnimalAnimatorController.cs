using UnityEngine;

public class AnimalAnimatorController : AnimatorController
{
    protected override void ControlAnimator()
    {
        Animator.SetFloat("Speed", Mathf.Abs(Inputs.HorizontalInputLeft));
        
        if(Inputs.Fire)
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
