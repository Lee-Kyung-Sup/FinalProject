using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IsGroundable
{
    private PlayerStatus playerStatus;
    private PlayerAnimations playerAnimations;
    private Rigidbody2D rb;

    [Header("UI & Trail")]
    [SerializeField] private Transform playerUI;
    [SerializeField] private TrailRenderer tr;

    [Header("Jump Parameters")]
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool hasJumped = false; 

    [Header("Dash Parameters")]
    [SerializeField] private float dashPower = 24f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;
    private bool canDash = true; // 대쉬 가능한지
    private bool isDashing; // 현재 대쉬 중인지
    private bool isDashCooldownComplete = true;  // 대쉬 쿨다운이 완료되었는지
    private float dashStartTime; // 대쉬 시작 시간

    private float originalGravityScale;
    private bool isPressingDown = false;

    [Header("Knockback Parameters")]
    [SerializeField] private float knockbackTime = 0.25f; // 넉백 지속 시간
    [SerializeField] private float knockbackForceY = 1.5f;
    [SerializeField] private float knockbackForceX = 5.0f;
    private bool isKnockedBack = false;


    [Header("Ground Check Options")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float boxWidth = 1f; // 땅 감지 박스
    [SerializeField] private float boxHeight = 0.1f;
    private LayerMask groundLayer;
    private LayerMask platformLayer;

    void Awake()
    {
        playerStatus = GetComponent<PlayerStatus>();
        rb = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();

        originalGravityScale = rb.gravityScale;
        groundLayer = LayerMask.GetMask("Ground", "Platform");
        platformLayer = LayerMask.GetMask("Platform");
    }

    void FixedUpdate()
    {
        Vector2 boxCenter = groundCheck.position + Vector3.down * boxHeight * 0.5f;
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, new Vector2(boxWidth, boxHeight), 0f, Vector2.down, 0, groundLayer);
        isGrounded = hit.collider != null;

        if (!isGrounded && rb.velocity.y < 0) // 낙하 중
        {
            playerAnimations.Falling(true);
        }

        if (isGrounded)
        {
            playerAnimations.Falling(false);

            if (!canDash && isDashCooldownComplete)
            {
                canDash = true;  // 땅에 닿음 + 쿨다운이 완료되었다면 대쉬 가능
            }

            if (rb.velocity.y <= 0)
            {
                jumpCount = 0; // 점프 횟수 초기화
                hasJumped = false;  // 점프 여부 초기화
            }
        }

        if (isDashing)
        {
            if (Time.time - dashStartTime <= dashTime)
            {
                rb.gravityScale = 0; // 대쉬 중 중력 0
                rb.velocity = new Vector2(transform.localScale.x * dashPower, 0);
            }
            else
            {
                EndDash(); // 대쉬 종료 처리 메서드
            }
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            float boxWidth = 1f;
            float boxHeight = 0.1f;

            Vector2 boxCenter = groundCheck.position + Vector3.down * boxHeight * 0.5f;

            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(boxCenter, new Vector3(boxWidth, boxHeight, 1));
        }
    }


    private void UnFlipPlayerUI()
    {
        // 플레이어 UI는 플레이어의 Scale.x가 반전되도 변하지 않게
        playerUI.localScale = new Vector3(transform.localScale.x > 0 ? 1 : -1, 1, 1);
    }

    public void Move(float inputX)
    {

        // 넉백 중이거나 대쉬 중일 때는 이동 x
        if (isKnockedBack || isDashing) return;

        if (!isDashing && !isKnockedBack)
        {

            // 대쉬 중 + 넉백 상태가 아닐 때만 이동
            rb.velocity = new Vector2(inputX * playerStatus.Speed, rb.velocity.y);

            playerAnimations.Moving(true);

            if (inputX != 0)
            {
                // 방향에 따른 플레이어 스프라이트 전환
                transform.localScale = new Vector3(inputX > 0 ? 1 : -1, 1, 1);
                UnFlipPlayerUI();
            }

            if (inputX == 0 && isGrounded)
            {
                // 정지 상태
                rb.velocity = new Vector2(0, rb.velocity.y);
                playerAnimations.Moving(false);

            }
        }
    }

    public void Jump()
    {
        if (isPressingDown && IsPlatformLayer()) // 아래 방향키를 누르면서 점프 키 + 플랫폼인 경우
        {
            DownPlatform();
            return;
        }

        if ((isGrounded || isDashing) && jumpCount < playerStatus.MaxJumpCount) // 땅에서 점프
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // 수직 속도 초기화
            rb.AddForce(Vector2.up * playerStatus.JumpPower, ForceMode2D.Impulse);
            
            jumpCount++; // 점프 횟수 증가 (첫 번째 점프)
            hasJumped = true;
            playerAnimations.Jumping();
        }
    }

    public void DoubleJump()
    {
        if ((!isGrounded && hasJumped && jumpCount > 0 && jumpCount < playerStatus.MaxJumpCount && playerStatus.Stamina >= 25))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // 수직 속도 초기화
            rb.AddForce(Vector2.up * playerStatus.JumpPower, ForceMode2D.Impulse);

            //playerStatus.UseStamina(25);
            jumpCount++;

            playerAnimations.Jumping();
        }
    }
    public bool HasJumped()  // 점프 여부 반환
    {
        return hasJumped;
    }

    public void Dash()
    {
        if (canDash && !isDashing && playerStatus.Stamina >= 25) 
            // 대쉬가 가능하고 현재 대쉬 중이 아닐 때 + 플레이어 스태미너 25이상
        {
            isDashing = true;



            gameObject.layer = 18; // 무적 레이어

            dashStartTime = Time.time;
            canDash = false;
            isDashCooldownComplete = false;  // 쿨다운 재시작

            playerAnimations.Dashing(); // 대쉬 애니메이션 재생

            tr.emitting = true; // 대쉬 효과 
            rb.velocity = new Vector2(transform.localScale.x * dashPower, rb.velocity.y);

            playerStatus.UseStamina(25); // 스태미너 사용
            StartCoroutine(DashCooldown()); // 대쉬 쿨다운 코루틴
        }
    }
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);

        isDashCooldownComplete = true;  // 쿨다운 완료
        if (isGrounded)
        {
            canDash = true; // 땅에 있으면 대쉬 다시 가능
        }
    }

    private void EndDash() // 대쉬 종료 처리
    {
        isDashing = false;
        gameObject.layer = 6; // 레이어 초기화 (무적 종료)

        rb.gravityScale = originalGravityScale;  // 대쉬가 끝나면 중력 스케일을 원래대로 복원
        tr.emitting = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);  // 수평 속도 유지, 수직 속도 0
    }

    public void SetIsPressingDown(bool isPressing)
    {
        isPressingDown = isPressing;
    }

    private bool IsPlatformLayer()
    {
        Vector2 boxCenter = groundCheck.position + Vector3.down * boxHeight * 0.5f;
        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, new Vector2(boxWidth, boxHeight), 0f, Vector2.down, 0, groundLayer);
        return hit.collider != null;
    }

    public void DownPlatform()
    {
        if (isPressingDown)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerStatus.JumpPower * 0.25f); // 살짝 점프
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), true);
            Invoke("Platform", 0.5f); // ignore False
        }
    }

    private void Platform()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Platform"), false);
    }

    public bool IsGround()
    {
        return isGrounded;
    }

    public void OnKnockback(Vector2 targetPosition)
    {
        isKnockedBack = true;
        rb.velocity = Vector2.zero; // 플레이어의 이동 방향 힘과 넉백 방향 힘의 상쇄 방지


        int dirc = targetPosition.x - transform.position.x < 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirc, knockbackForceX) * knockbackForceY, ForceMode2D.Impulse);

        StartCoroutine(OffKnockback(knockbackTime));
    }

    private IEnumerator OffKnockback(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

}
