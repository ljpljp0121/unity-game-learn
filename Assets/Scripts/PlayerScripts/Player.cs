using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : Entity
{

    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration;

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir;
    [Header("Catch Sword")]
    public float catchSwordVelocity;

    public SkillManager skill {  get; private set; }
    public GameObject sword { get; private set; }

    #region 状态
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerSlideWallState slideWallState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public PlayerBlackHoleState blackHoleState { get; private set; }
    #endregion
    //实例化所有状态
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        slideWallState = new PlayerSlideWallState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackHoleState = new PlayerBlackHoleState(this, stateMachine, "Jump");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if(Input.GetKeyDown(KeyCode.E))
        {
            skill.crystall.CanUseSkill();
        }
    }

    //一个延迟器
    public IEnumerable BusyFor(float seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(seconds);

        isBusy = false;
    }

    public void AssignNewSword(GameObject newSword)
    {
        sword = newSword;
    }

    public void CatchSword()
    {
        Destroy(sword);
        stateMachine.ChangeState(catchSwordState);
    }

   
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();


}
