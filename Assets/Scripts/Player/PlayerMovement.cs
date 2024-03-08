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
    private Transform groundCheck; // í”Œë ˆì´ì–´ì˜ í•˜ë‹¨ì— ìœ„ì¹˜
    private float groundCheckRange = 1f; // ë•… ê°ì§€ ë²”ìœ„

    [SerializeField]
    private LayerMask groundLayer; // ë•…ìœ¼ë¡œ ê°„ì£¼í•  ë ˆì´ì–´

>>>>>>> Stashed changes
    private float dashPower = 2f;

    private Rigidbody2D rb;
    public bool IsGrounded { get; private set; } = true; // Á¡ÇÁ °¡´É ¿©ºÎ, ¶¥ÀÎÁö

    private Vector2 inputVector; // ÀÌµ¿ ÀÔ·Â ÀúÀå

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
            Move(inputVector); // ¿òÁ÷ÀÓ Ã³¸®
        }
    }

    public void Move(Vector2 inputVector)
    {
        rb.velocity = new Vector2(inputVector.x * speed, rb.velocity.y);
    }

    public void Jump()
    {

        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        IsGrounded = false; // Á¡ÇÁÇÏ¸é ¶¥ÀÌ ¾Æ´Ô
    }

    public void Dash()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Ground ÅÂ±×¶û ´êÀ¸¸é
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
