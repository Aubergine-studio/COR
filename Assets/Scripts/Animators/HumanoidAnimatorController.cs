using UnityEngine;
using System.Collections;

public class HumanoidAnimatorController : AnimatorController
{
    public float attack = 0.0f;		//	Zmienna determinujaca animacje ataku.
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
        animationStatus = animator.GetCurrentAnimatorStateInfo(0);

        if (inputs.fire && AttaksHash != animationStatus.nameHash)
        {
            switch (player.projectileIndex)
            {
                case 0:
                    attack = Random.Range(1,3);
                    break;
                case 1:
                    attack = Random.Range(3, 5);
                    break;
            }
        }

        animator.SetBool("Ground", inputs.isGrounded);
        if(inputs.fire)
            animator.SetTrigger("Fire");
        animator.SetBool("LadderClimbing", inputs.isLadderClimbing);

        if (inputs.isGetOnLadder)
        {
            animator.SetTrigger("OnLadder");
            inputs.isGetOnLadder = false;
        }
        animator.SetFloat("Attack", attack);
        animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(inputs.horizontalInput));

        if (chracter.Health <= 0)
        {
            animator.SetTrigger("IsDead");
            this.enabled = false;
        }

    }

    public bool GetAttackStatus()
    {
        return AttaksHash == animationStatus.nameHash ? true : false;
    }
}
