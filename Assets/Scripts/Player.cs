using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public delegate void UpdateNumberInfoAction(int a);
    public UpdateNumberInfoAction UpdateHpAction;
    public UpdateNumberInfoAction UpdateCoinAction;

    public int hp
    {
        get; private set;
    }

    public int coin
    {
        get; private set;
    }

    [SerializeField] int maxHp = 3;
    [SerializeField] float speed = 1f;
    [SerializeField] int jumpableCount = 1;
    [SerializeField] float jumpPower = 5f;

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer renderer;
    Rigidbody2D rigidbody;

    bool isDead = false;        // hp 1개 없어지는 죽음
    bool isReallyDead = false;  // hp가 0이 되었을 때
    bool isJumping = false;
    int jumpCount = 0;

    Vector3 startPos;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        UpdateHp(maxHp);
        coin = 0;
        startPos = transform.position;
    }

    void Update()
    {
        if (isDead == true || isReallyDead == true) return;

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
        if (isDead == true || isReallyDead == true) return;

        Move();
        Jump();
    }

    public void UpdateHp(int addHp)
    {
        hp += addHp;

        if (hp > maxHp) hp = maxHp;
        if (hp < 0) hp = 0;

        if (UpdateHpAction != null) UpdateHpAction(hp);
    }

    public void GetCoin(int addCoin)
    {
        coin += addCoin;

        if (UpdateCoinAction != null) UpdateCoinAction(coin);
    }

    public void OnDead()
    {
        if (isDead == true || isReallyDead == true) return;

        StartCoroutine(IEDead());
    }

    IEnumerator IEDead()
    {
        isDead = true;
        UpdateHp(-1);

        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(new Vector2(1f, 5f), ForceMode2D.Impulse);
        animator.SetBool("IsDead", isDead);

        yield return new WaitForSeconds(1.5f);

        if (hp > 0)
        {
            // 부활!
            isDead = false;
            transform.position = startPos;
            animator.SetBool("IsDead", isDead);
        }
        else
        {
            isReallyDead = true;
            Debug.Log("완전 게임 오버");
        }
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
