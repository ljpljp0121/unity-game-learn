using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemySkeleton : Enemy
{
    #region 
    public SkeltonIdleState idleState { get; private set; }
    public SkeltonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonHitState hitState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }
    #endregion 
    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeltonIdleState(this, stateMachine, "Idel", this);
        moveState = new SkeltonMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        hitState = new SkeletonHitState(this, stateMachine, "Hit", this);
        deadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();     
    }

    public override bool CanBeHit()
    {
        if(base.CanBeHit())
        {
            stateMachine.ChangeState(hitState);
            return true;
        }
        return false;
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState); 
    }
}
