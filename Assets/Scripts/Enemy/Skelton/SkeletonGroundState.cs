using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected EnemySkeleton enemy;
    protected Transform player;
    public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName, EnemySkeleton enemy) : base(enemyBase, enemyStateMachine, animBoolName)
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

        

        if(enemy.isPlayerDetected() ||Vector2.Distance(enemy.transform.position,player.position)<2)
        {
            enemyStateMachine.ChangeState(enemy.battleState);
        }
    }
}
