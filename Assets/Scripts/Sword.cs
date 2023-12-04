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

    public float bouncingSpeed;
    public bool isBouncing;
    public int amountOfBounce = 4;
    public List<Transform> enemyTarget;
    private int targetIndex = 0;

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
    }

    public void ReturnSword()
    {
        rb.isKinematic = true;
        transform.parent = null;
        canRotate = true;
        isReturning = true;
        animator.SetBool("Rotate", false);
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
        if(isBouncing&&enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position,bouncingSpeed*Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.01f)
            {
                targetIndex++;
                amountOfBounce--;
                if (amountOfBounce <= 0)
                {
                    isBouncing = false;
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
        StuckInto(collision);

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
    }

    //飞剑碰撞后停止并插在物体上
    private void StuckInto(Collider2D collision)
    {
        
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
