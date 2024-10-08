using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : Entity,ISaveManager
{

    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration;

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    private float defaultMoveSpeed;
    private float defaultJumpForce;
    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir;
    private float defaultDashSpeed;
    [Header("Catch Sword")]
    public float catchSwordVelocity;

    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }

    #region ״̬
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
    public PlayerDeadState deadState { get; private set; }
    #endregion
    //ʵ��������״̬
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
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if (Input.GetKeyDown(KeyCode.E))
        {
            skill.crystall.CanUseSkill();
        }
    }

    //һ���ӳ���
    public IEnumerable BusyFor(float seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(seconds);

        isBusy = false;
    }
    public override void SlowEntityBy(float slowPercentage, float slowDuration)
    {
        moveSpeed = moveSpeed * (1 - slowPercentage);
        jumpForce = jumpForce * (1 - slowPercentage);
        dashSpeed = dashSpeed * (1 - slowPercentage);
        animator.speed = animator.speed * (1 - slowPercentage);

        Invoke("ReturnDefaultSpeed", slowDuration);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;

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

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.transform;
    }

    public void SaveData(ref GameData data)
    {
        data.transform = this.transform.position;
    }
}
