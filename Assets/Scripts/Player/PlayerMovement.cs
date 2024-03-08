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
    private Transform groundCheck; // 플레이어의 하단에 위치
    private float groundCheckRange = 1f; // 땅 감지 범위

    [SerializeField]
    private LayerMask groundLayer; // 땅으로 간주할 레이어


    private float dashPower = 2f;
    private Rigidbody2D rb;
    private int jumpCount = 0; // 점프 횟수
    public bool IsGrounded { get; private set; } = true; 


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        // 땅에 닿았는지 확인
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRange, groundLayer);
        if (IsGrounded)
        {
            Debug.Log("땅입니다.");
            jumpCount = 0; // 땅에 닿으면 점프 횟수 초기화
        }
    }

    public void Move(Vector2 inputVector)
    {
        rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
    }

    public void Jump()
    {
        if (IsGrounded && jumpCount < 2)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++; // 점프 횟수 증가
            IsGrounded = false;
        }
    }

    public void Dash()
    {

    }

    //private void oncollisionenter2d(collision2d collision)
    //{
    //    if (collision.gameobject.comparetag("ground"))
    //    {
    //        isgrounded = true;
    //        jumpcount = 0; // 땅에 닿으면 점프 횟수 초기화
    //        debug.log(" 땅입니다. ");
    //    }
    //}
}
