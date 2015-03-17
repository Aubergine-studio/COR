using UnityEngine;
using System.Collections;

public class HumanoidAnimatorController : AnimatorController
{
    private AnimatorStateInfo animationStatus;
    int AttaksHash = Animator.StringToHash("Base Layer.Attacks");
    Player player;

    new void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    protected override void ControlAnimator()
    {
        if (!inputs.inGameMenu)
        {
            animationStatus = animator.GetCurrentAnimatorStateInfo(0);

            if (inputs.fire)// && AttaksHash != animationStatus.nameHash)
            {
                Debug.Log("Trigger animacji.");
                animator.SetTrigger("Fire");
                animator.SetFloat("Attack", Random.Range(1f, 2f));
                animator.SetFloat("Weapon", player.projectileIndex + 1);
                inputs.fire = false;
            }

            animator.SetBool("Ground", inputs.isGrounded);
            animator.SetBool("LadderClimbing", inputs.isLadderClimbing);

            if (inputs.isGetOnLadder)
            {
                animator.SetTrigger("OnLadder");
                inputs.isGetOnLadder = false;
            }
            animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(inputs.horizontalInput_left));

            if (chracter.Health <= 0)
            {
                animator.SetTrigger("IsDead");
                this.enabled = false;
            }

            if (inputs.inCombat)
                animator.SetFloat("Status", 1f);
            else
                animator.SetFloat("Status", 0f);
        }
    }

    public bool GetAttackStatus()
    {
        return AttaksHash == animationStatus.nameHash ? true : false;
    }
}
