using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 실제 플레이어 이동 관련 로직 클래스

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpPower = 5f;

    private float dashPower = 2f;

    private Rigidbody2D rb;
    private bool isGrounded = true; // 점프 가능 여부, 땅인지


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // 리지드 바디 컴포넌트 초기화
    }

    private void FixedUpdate()
    {
            Move(rb.velocity); // 이동 처리
    }

    public void Move(Vector2 inputVector)
    {
        // 입력 벡터의 x축으로만 이동
        rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false; // 점프하면 땅이 아님
        }
    }

    public void Dash()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ground 태그랑 닿으면
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
