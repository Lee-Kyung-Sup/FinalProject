using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpPower = 5f;
    [SerializeField]
    private int addJumpCount = 1; // 추가 점프 가능 횟수

    [SerializeField]
    private Transform groundCheck; // 플레이어의 하단에 위치
    private float groundCheckRange = 0.3f; // 땅 감지 범위

    [SerializeField]
    private LayerMask groundLayer; // 땅으로 간주할 레이어

    private Rigidbody2D rb;
    private int jumpCount = 0; // 점프 횟수
    public bool IsGrounded { get; private set; } = true;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 땅에 닿았는지 확인
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRange, groundLayer);
        if (IsGrounded)
        {
            jumpCount = 0; // 땅에 닿으면 점프 횟수 초기화
        }
    }

    public void Move(float inputX)
    {
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

            if (inputX == 0 && IsGrounded)
            {
                rb.velocity = new Vector2(0, rb.velocity.y); // 입력이 없고 땅에 있을 때는 속도를 0으로 설정
            }

    }

        public void Jump()
    {
        if (jumpCount < addJumpCount)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++; // 점프 횟수 증가
            IsGrounded = false;
        }
    }

    public void Dash()
    {

    }





    //    private void oncollisionenter2d(collision2d collision)
    //    {
    //        if (collision.gameobject.comparetag("ground"))
    //        {
    //            isgrounded = true;
    //            jumpcount = 0; // 땅에 닿으면 점프 횟수 초기화
    //            debug.log(" 땅입니다. ");
    //        }
    //    }
}
