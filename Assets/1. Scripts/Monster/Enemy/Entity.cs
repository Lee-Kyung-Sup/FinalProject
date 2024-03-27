using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator Ani { get; private set; }
    public Rigidbody2D Rigi { get; private set; }

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    LayerMask gpLayer;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        gpLayer = LayerMask.GetMask("Ground", "Platform");
        Rigi = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
    }
    protected virtual void Update()
    {

    }

    public void ZeroVelocity() => Rigi.velocity = new Vector2(0, 0);
    public void SetVelocity(float xVelocity,float yVelocity)
    {
        Rigi.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, gpLayer);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, gpLayer);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 100, 0);
    }
    public void FlipController(float x)
    {
        if (x > 0 && !facingRight)
        {
            Flip();
        }
        else if(x < 0 && facingRight)
        {
            Flip();
        }
    }
}
