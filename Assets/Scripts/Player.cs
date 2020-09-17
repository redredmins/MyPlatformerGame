using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] int jumpableCount = 1;
    [SerializeField] float jumpPower = 5f;

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer renderer;
    Rigidbody2D rigidbody;

    bool isDead = false;
    bool isJumping = false;
    int jumpCount = 0;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead == true) return;

        float xAxis = Input.GetAxis("Horizontal");
        
        if (xAxis > 0.1f || xAxis < -0.1f)
        {
            animator.SetBool("IsWalk", true);
            
            if (xAxis > 0)
                renderer.flipX = false;
            else
                renderer.flipX = true;
        }
        else
            animator.SetBool("IsWalk", false);
    }

    void FixedUpdate()
    {
        if (isDead == true) return;

        Move();
        Jump();
    }

    public void OnDead()
    {
        isDead = true;

        animator.SetBool("IsDead", isDead);
    }

    void Move()
    {
        float xVelcity = 0f;
        float xAxis = Input.GetAxis("Horizontal");
        if (xAxis < -0.1f) // Left
        {
            xVelcity = -1f;
        }
        else if (xAxis > 0.1f) // Right
        {
            xVelcity = 1f;
        }

        xVelcity = xVelcity * speed;
        rigidbody.velocity = new Vector2(xVelcity, rigidbody.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && jumpCount < jumpableCount)
        {
            isJumping = true;
            jumpCount++;

            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);

            animator.SetBool("IsJumping", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0f)
        {
            isJumping = false;
            jumpCount = 0;

            animator.SetBool("IsJumping", false);
        }
    }


}
