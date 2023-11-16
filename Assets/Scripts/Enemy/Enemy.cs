using System.Collections;
using System.Collections.Generic;
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
    [Header("Attack info")]
    public float attackDistance;
    public float attackCoolDown;
    [HideInInspector] public float lastTimeAttacked;

    #region 状态
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyState enemyState { get; private set; }
    #endregion 
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
        
    }
    protected override void Update()
    {
        base .Update();
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

    public virtual bool  CanBeHit()
    {
        if (canBeHit)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }
}
