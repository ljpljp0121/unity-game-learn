using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : EnemyState
{
    private EnemySkeleton enemy;

    public SkeletonDeadState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.animator.SetBool(enemy.lastAnimBoolName,true);
        enemy.animator.speed = 0;
        enemy.cd.enabled = false;

        stateTimer = .1f;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer > 0)
        {
            // = new Vector2(0, 10);
        }
    }
}
