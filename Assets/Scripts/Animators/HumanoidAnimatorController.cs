using UnityEngine;

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
        if (!Inputs.InGameMenu)
        {
            _animationStatus = Animator.GetCurrentAnimatorStateInfo(0);

            if (Inputs.IsGrounded && Inputs.HorizontalInputLeft == 0 && smokingTime <= 0)
            {
                smokingTime = SmokingInterval;
                Animator.SetTrigger("Smoke");
            }
            else smokingTime -= Time.deltaTime;
            if(Mathf.Abs(Inputs.HorizontalInputLeft) > 0)
            {
                smokingTime = SmokingInterval;
            }

            if (Inputs.Fire)// && AttaksHash != animationStatus.nameHash)
            {
                Debug.Log("Trigger animacji.");
                Animator.SetTrigger("Fire");
                Animator.SetFloat("Attack", Random.Range(1f, 2f));
                Animator.SetFloat("Weapon", player.ProjectileIndex + 1);
                Inputs.Fire = false;
            }

            Animator.SetBool("Ground", Inputs.IsGrounded);
            Animator.SetBool("LadderClimbing", Inputs.IsLadderClimbing);

            if (Inputs.IsGetOnLadder)
            {
                Animator.SetTrigger("OnLadder");
                Inputs.IsGetOnLadder = false;
            }
            Animator.SetFloat("vSpeed", rigidbody2D.velocity.y);
            Animator.SetFloat("Speed", Mathf.Abs(Inputs.HorizontalInputLeft));

            if (Chracter.Health <= 0)
            {
                Animator.SetTrigger("IsDead");
                this.enabled = false;
            }

            if (Inputs.InCombat)
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
