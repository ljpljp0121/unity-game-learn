using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    EnemySkeleton enemy;
    public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName,EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.ZeroVelocity();

        if(triggerCalled)
        {
            enemyStateMachine.ChangeState(enemy.battleState);
        }
    }
}
