using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;
    private Player player;
    private void Awake()
    {
        animator =GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void SetupSword(Vector2 dir,float gravityScale)
    {
        rb.velocity = dir;
        rb.gravityScale = gravityScale;
    }
}
