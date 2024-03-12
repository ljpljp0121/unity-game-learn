using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private Player player;
    private SpriteRenderer sr;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float colorLosingSpeed;
    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDuplicate;
    private int facingDir = 1;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }
    private void Update()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        cloneTimer -= Time.deltaTime;
        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));
        }

        if (sr.color.a < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack, Vector3 offset, bool canDuplicate, float chanceToDuplicate, Player player)
    {
        if (newTransform != null)
        {
            transform.position = newTransform.position + offset;
            FaceClosestTarget();
            cloneTimer = cloneDuration;
            canDuplicateClone = canDuplicate;
            this.chanceToDuplicate = chanceToDuplicate;
            if (canAttack)
            {
                animator.SetInteger("Attack", Random.Range(1, 3));
            }
        }
    }

    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (Collider2D hit in colliders)
        {
            hit.GetComponent<Enemy>().Damage();
            if (hit.GetComponent<Enemy>() != null)
            {
                if (canDuplicateClone)
                {
                    if (Random.Range(0, 100) < chanceToDuplicate)
                    {
                        SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(1.2f * facingDir, 0));
                    }
                }

            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;

        foreach (var item in colliders)
        {
            if (item.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.SqrMagnitude(transform.position - item.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = item.transform;
                }
            }

        }
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
