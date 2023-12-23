using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region 组件
    public Animator animator { get; private set; }
    public Rigidbody2D rigidbody2 { get; private set; }
    public EntityFX fx {  get; private set; }

    public SpriteRenderer sr { get; private set; }  

    #endregion

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDir;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    [Header("Collison info")]
    public Transform attackCheck;
    public float attackCheckRaius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        fx = GetComponent<EntityFX>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnocked");
    }

    protected virtual IEnumerator HitKnocked()
    {
        isKnocked = true;

        rigidbody2.velocity = new Vector2(knockbackDir.x* - facingDir, knockbackDir.y);

        yield return new WaitForSeconds(knockbackDuration);
        isKnocked =false;
    }

    #region Flip
    //翻转
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    //翻转控制器
    public void FlipController(float x)
    {
        if (x > 0 && !facingRight)
        {
            Flip();
        }
        else if (x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion

    #region Collison
    //地面检测
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    //画线
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRaius);
    }
    #endregion

    #region Velocity
    //速度置零
    public void ZeroVelocity() => SetVelocity(0,0);


    //设置速度
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if(isKnocked)
        {
            return;
        }
        
        rigidbody2.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion

    public void MakeTransprent(bool transprent)
    {
        if (transprent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }
}
