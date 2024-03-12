using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHitState : EnemyState
{
    private EnemySkeleton enemy;
    public SkeletonHitState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.hitDuration;
        enemy.rigidbody2.velocity = new Vector2(-enemy.facingDir * enemy.hitDir.x,enemy.hitDir.y);

        enemy.fx.InvokeRepeating("RedColorBlink",0,0.1f);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer <0)
        {
            enemyStateMachine.ChangeState(enemy.idleState);
        }
    }
}
