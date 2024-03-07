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
    private bool isGrounded = true; // ���� ���� ����, ������

    private Vector2 inputVector; // �̵� �Է� ����

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
        isGrounded = false; // �����ϸ� ���� �ƴ�
    }

    public void Dash()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ground �±׶� ������
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
