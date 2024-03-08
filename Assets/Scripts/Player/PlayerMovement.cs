using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ���� �÷��̾� �̵� ���� ���� Ŭ����

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpPower = 5f;

    private float dashPower = 2f;

    private Rigidbody2D rb;
    public bool IsGrounded { get; private set; } = true; // ���� ���� ����, ������


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // ������ �ٵ� ������Ʈ �ʱ�ȭ
    }

    private void FixedUpdate()
    {
            Move(rb.velocity); // �̵� ó��
    }

    public void Move(Vector2 inputVector)
    {
        // �Է� ������ x�����θ� �̵�
        rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false; // �����ϸ� ���� �ƴ�
        }
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
