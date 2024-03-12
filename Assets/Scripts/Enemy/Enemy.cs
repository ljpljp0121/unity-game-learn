using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Hit info")]
    public float hitDuration;
    public Vector2 hitDir;
    protected bool canBeHit;
    [SerializeField] protected GameObject counterImage;


    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    private float defaulSpeed;
    [Header("Attack info")]
    public float attackDistance;
    public float attackCoolDown;
    [HideInInspector] public float lastTimeAttacked;

    #region 状态
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyState enemyState { get; private set; }
    #endregion 

    public string lastAnimBoolName { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaulSpeed = moveSpeed;
    }

    protected override void Start()
    {
        base.Start();

    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual RaycastHit2D isPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual void FreezeTimer(bool timeFrozen)
    {
        if (timeFrozen)
        {
            moveSpeed = 0;
            animator.speed = 0;
        }
        else
        {
            moveSpeed = defaulSpeed;
            animator.speed = 1;
        }
    }

    public virtual void AssignLastAnimName(string animBoolName)
    {
        this.lastAnimBoolName = animBoolName;
    }
    public override void SlowEntityBy(float slowPercentage, float slowDuration)
    {
        moveSpeed = moveSpeed * (1 - slowPercentage);
        animator.speed = animator.speed * (1 - slowPercentage);
        Invoke("ReturnDefaultSpeed",slowDuration);
    }
    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();
        moveSpeed = defaulSpeed;

    }

    protected virtual IEnumerator FreezeTimerFor(float seconds)
    {
        FreezeTimer(true);
        yield return new WaitForSeconds(seconds);
        FreezeTimer(false);
    }
    #region Counter Attack Window
    public virtual void OpenCounterAttackWindow()
    {
        canBeHit = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeHit = false;
        counterImage.SetActive(false);
    }
    #endregion

    public virtual bool CanBeHit()
    {
        if (canBeHit)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }
}
