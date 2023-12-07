using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private Player player;

    private bool canRotate = true;
    [SerializeField] private float returnSpeed = 12;
    private bool isReturning;

    [Header("Pierce info")]
    [SerializeField] private float pierceAmount;

    [Header("Bounce info")]
    [SerializeField] private float bouncingSpeed;
    private bool isBouncing;
    private int bounceAmount;
    public List<Transform> enemyTarget;
    private int targetIndex = 0;

    [Header("Spin info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped;
    private bool isSpinning;

    private float damageTimer;
    private float damageCooldown;

    private float spinDir;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Vector2 dir, float gravityScale, Player player)
    {
        this.player = player;
        rb.velocity = dir;
        rb.gravityScale = gravityScale;

        animator.SetBool("Rotate", true);

        spinDir = Mathf.Clamp(rb.velocity.x, -1, 1);
    }

    public void ReturnSword()
    {
        rb.isKinematic = true;
        transform.parent = null;
        canRotate = true;
        isReturning = true;
        animator.SetBool("Rotate", false);
    }

    public void SetupBounce(bool isBouncing,int amountOfBounces)
    {
        this.isBouncing = isBouncing;
        this.bounceAmount = amountOfBounces;

        enemyTarget = new List<Transform>();
    }

    public void SetupPierce(int pierceAmount)
    {
        this.pierceAmount = pierceAmount;
    }

    public void SetupSpin(bool isSpinning,float maxTravelDistance,float spinDuration,float hitCooldown)
    {
        this.isSpinning = isSpinning;
        this.spinDuration = spinDuration;
        this.maxTravelDistance = maxTravelDistance;
        damageCooldown = hitCooldown;
    }
    private void Update()
    {
        if (canRotate)
        {
            transform.right = rb.velocity;
        }

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 0.5)
            {
                player.CatchSword();
            }
        }
        BounceLogic();

        if (isSpinning)
        {
            if(Vector2.Distance(player.transform.position,transform.position) >maxTravelDistance &&!wasStopped)
            {
                StopWhenSpinning();
            }

            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position,new Vector2(transform.position.x+spinDir,transform.position.y),1.5f*Time.deltaTime);
                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpinning = false;
                }

                damageTimer -= damageTimer;
                if (damageTimer < 0)
                {
                    damageTimer = damageCooldown;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.GetComponent<Enemy>() != null)
                        {
                            collider.GetComponent<Enemy>().Damage();
                        }
                    }
                }
            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bouncingSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.01f)
            {
                enemyTarget[targetIndex].GetComponent<Enemy>().Damage();
                targetIndex++;
                bounceAmount--;
                if (bounceAmount <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                if (targetIndex >= enemyTarget.Count)
                {
                    targetIndex = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isReturning)
        {
            return;
        }

        collision.GetComponent<Enemy>()?.Damage();

        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                foreach (Collider2D collider in colliders)
                {
                    if (collider.GetComponent<Enemy>() != null)
                    {
                        enemyTarget.Add(collider.transform);
                    }
                }
            }
        }
        StuckInto(collision);
    }

    //飞剑碰撞后停止并插在物体上
    private void StuckInto(Collider2D collision)
    {
        if(pierceAmount >0&&collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        if (isSpinning)
        {
            StopWhenSpinning();
            return;
        }
            
        
        canRotate = false;
        circleCollider.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if(isBouncing && enemyTarget.Count >0)
        {
            return;
        }
        animator.SetBool("Rotate", false);
        transform.parent = collision.transform;
    }
}
