using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        player.animator.SetBool("SuccessfulCounter",false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRaius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeHit())
                {
                    stateTimer = 10;
                    player.animator.SetBool("SuccessfulCounter", true);
                }
            }
        }
        if(stateTimer <0 ||triggerCalled)
        {
            player.animator.SetBool("SuccessfulCounter", false);
            stateMachine.ChangeState(player.idleState);
        }
    }
}
