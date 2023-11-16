using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//×´Ì¬»ùÀà
public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected float xInput;
    protected float yInput;
    protected Rigidbody2D rigidbody2;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        rigidbody2 = player.rigidbody2;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.animator.SetFloat("yVelocity", rigidbody2.velocity.y);

        CheckForDashInput();
    }
    //³å´Ì¼ì²â
    private void CheckForDashInput()
    {
        if(player.IsWallDetected())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)&& player.skill.dash.CanUseSkill())
        {
            player.dashDir = Input.GetAxisRaw("Horizontal");
            if (player.dashDir == 0)
            {
                player.dashDir = player.facingDir;
            }
            stateMachine.ChangeState(player.dashState);
        }
    }

    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
