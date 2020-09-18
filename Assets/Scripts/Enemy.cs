using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 유한 상태 머신 (AI)
    // Idle : 지정된 좌표를 서성거리면서 캐릭터가 근처에 왔는지 탐색
    // Follow : 캐릭터가 근처에 있으면 캐릭터를 향해 이동

    [SerializeField] Vector2 moveStartPos;
    [SerializeField] Vector2 moveEndPos;
    [SerializeField] float speed = 1f;
    [SerializeField] float idleTime = 0.5f;
    float waitTime = 0f;
    bool isWaiting = false;

    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Animator animator;



    void Update()
    {
        //Ray2D ray = new Ray2D();

        OnIdle();
    }

    void OnIdle()
    {
        if (isWaiting == true)
        {
            waitTime += Time.deltaTime;

            if (waitTime >= idleTime)
            {
                isWaiting = false;
                waitTime = 0;
            }

            animator.SetBool("IsWalking", false);
        }
        else
        {
            Walk();
        }
    }
    void Walk()
    {
        Vector2 dir = (renderer.flipX == true) ? Vector2.left : Vector2.right;
        
        if (transform.position.x <= moveStartPos.x)
        {
            isWaiting = true;
            dir = Vector2.right;
            renderer.flipX = false;
        }
        else if (transform.position.x >= moveEndPos.x)
        {
            isWaiting = true;
            dir = Vector2.left;
            renderer.flipX = true;
        }

        transform.position += ((Vector3)dir * speed) * Time.deltaTime;

        animator.SetBool("IsWalking", true);
    }


    void OnFollow(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position).normalized;

        transform.position += (dir * speed) * Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();

        if (player != null)
        {
            player.OnDead();
        }
    }

}
