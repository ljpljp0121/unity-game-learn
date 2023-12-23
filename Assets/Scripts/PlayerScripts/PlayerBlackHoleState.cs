using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    private float flyTime = .4f;
    private bool skillUsed;

    private float defaultGravity;

    public PlayerBlackHoleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravity = rigidbody2.gravityScale;
        skillUsed = false;
        stateTimer = flyTime;
        rigidbody2.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        rigidbody2.gravityScale = defaultGravity;
        player.MakeTransprent(false);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
        {
            rigidbody2.velocity = new Vector2(0, 15);
        }
        if (stateTimer < 0)
        {
            rigidbody2.velocity = new Vector2(0, -.01f);

            if (!skillUsed)
            {
                if (player.skill.blackHole.CanUseSkill())
                    skillUsed = true;
            }
        }

        if (player.skill.blackHole.BlackHoleFinished())
        {
            stateMachine.ChangeState(player.airState);
        }
    }
}
