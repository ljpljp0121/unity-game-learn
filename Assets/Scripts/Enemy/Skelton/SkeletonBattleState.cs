using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{

    EnemySkeleton enemy;
    private Transform player;
    private int Direction;

    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.isPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.isPlayerDetected().distance < enemy.attackDistance && CanAttack())
            {
                enemyStateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {
            if(stateTimer > 0|| Vector2.Distance(player.transform.position,enemy.transform.position)>15)
            {
                enemyStateMachine.ChangeState(enemy.idleState);
            }
        }

        if (player.position.x > enemy.transform.position.x)
        {
            Direction = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            Direction = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * Direction, enemy.rigidbody2.velocity.y);
    }

    private bool CanAttack()
    {
        if (Time.time > enemy.lastTimeAttacked + enemy.attackCoolDown )
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}

