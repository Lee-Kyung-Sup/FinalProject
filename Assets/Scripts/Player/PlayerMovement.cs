using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpPower = 5f;

<<<<<<< Updated upstream
=======
    [SerializeField]
    private Transform groundCheck; // 플레이어의 하단에 위치
    private float groundCheckRange = 1f; // 땅 감지 범위

    [SerializeField]
    private LayerMask groundLayer; // 땅으로 간주할 레이어

>>>>>>> Stashed changes
    private float dashPower = 2f;

    private Rigidbody2D rb;
    public bool IsGrounded { get; private set; } = true; // ���� ���� ����, ������

    private Vector2 inputVector; // �̵� �Է� ����

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (IsGrounded)
        {
            Jump();
        }
        else
        {
            Move(inputVector); // ������ ó��
        }
    }

    public void Move(Vector2 inputVector)
    {
        rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
    }

    public void Jump()
    {

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        IsGrounded = false; // �����ϸ� ���� �ƴ�
    }

    public void Dash()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ground �±׶� ������
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }
}
