using UnityEngine;
using System.Collections;

public class HumanoidAnimatorController : AnimatorController
{
    private AnimatorStateInfo _animationStatus;
    public float SmokingInterval = 30;
    private float smokingTime;
    int AttaksHash = Animator.StringToHash("Base Layer.Attacks");
    Player player;

    new void Start()
    {
        base.Start();
        player = GetComponent<Player>();
        smokingTime = SmokingInterval;
    }

    protected override void ControlAnimator()
    {
        if (!Inputs.inGameMenu)
        {
            _animationStatus = Animator.GetCurrentAnimatorStateInfo(0);

            if (Inputs.isGrounded && Inputs.horizontalInput_left == 0 && smokingTime <= 0)
            {
                smokingTime = SmokingInterval;
                Animator.SetTrigger("Smoke");
            }
            else smokingTime -= Time.deltaTime;
            if(Mathf.Abs(Inputs.horizontalInput_left) > 0)
            {
                smokingTime = SmokingInterval;
            }

            if (Inputs.fire)// && AttaksHash != animationStatus.nameHash)
            {
                Debug.Log("Trigger animacji.");
                Animator.SetTrigger("Fire");
                Animator.SetFloat("Attack", Random.Range(1f, 2f));
                Animator.SetFloat("Weapon", player.ProjectileIndex + 1);
                Inputs.fire = false;
            }

            Animator.SetBool("Ground", Inputs.isGrounded);
            Animator.SetBool("LadderClimbing", Inputs.isLadderClimbing);

            if (Inputs.isGetOnLadder)
            {
                Animator.SetTrigger("OnLadder");
                Inputs.isGetOnLadder = false;
            }
            Animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
            Animator.SetFloat("Speed", Mathf.Abs(Inputs.horizontalInput_left));

            if (Chracter.Health <= 0)
            {
                Animator.SetTrigger("IsDead");
                this.enabled = false;
            }

            if (Inputs.inCombat)
                Animator.SetFloat("Status", 1f);
            else
                Animator.SetFloat("Status", 0f);
        }
    }

    public bool GetAttackStatus()
    {
        return AttaksHash == _animationStatus.nameHash ? true : false;
    }
}
