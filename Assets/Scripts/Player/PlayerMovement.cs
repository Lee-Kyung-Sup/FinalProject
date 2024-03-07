using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpPower = 5f;

    private float dashPower = 2f;

    private Rigidbody2D rb;
    private bool isGrounded = true; // 점프 가능 여부, 땅인지

    private Vector2 inputVector; // 이동 입력 저장

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            Jump();
        }
        else
        {
            Move(inputVector); // 움직임 처리
        }
    }

    public void Move(Vector2 inputVector)
    {
        rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
    }

    public void Jump()
    {

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGrounded = false; // 점프하면 땅이 아님
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
