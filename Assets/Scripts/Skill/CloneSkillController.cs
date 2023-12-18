using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float colorLosingSpeed;
    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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

    public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack,Vector3 offset)
    {
        if (newTransform != null)
        {
            transform.position = newTransform.position + offset;
            FaceClosestTarget();
            cloneTimer = cloneDuration;
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
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
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
                    closestEnemy = item.transform;
                }
            }

        }
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
