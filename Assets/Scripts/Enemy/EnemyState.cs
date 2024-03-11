using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine enemyStateMachine;
    private string animBoolName;

    protected bool triggerCalled;
    protected float stateTimer;
    public EnemyState(Enemy enemyBase, EnemyStateMachine enemyStateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.enemyStateMachine = enemyStateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.animator.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemyBase.animator.SetBool(animBoolName, false);
        enemyBase.AssignLastAnimName(animBoolName);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
