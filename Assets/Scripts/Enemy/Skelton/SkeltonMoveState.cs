using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeltonMoveState : SkeletonGroundState
{
    public SkeltonMoveState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, enemy.rigidbody2.velocity.y);

        if(enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            enemyStateMachine.ChangeState(enemy.idleState);
        }
        
    }
}
