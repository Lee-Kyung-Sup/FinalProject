using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr; // 대시 효과용 TrailRenderer
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private int maxJumpCount = 2; // 최대 점프 가능 횟수
    [SerializeField] private Transform groundCheck; // 플레이어의 하단에 위치
    private LayerMask groundLayer; // 땅으로 간주할 레이어
    private LayerMask platformLayer; // 플랫폼으로 간주할 레이어

    private float groundCheckRange = 0.3f; // 땅 감지 범위
    private int jumpCount = 0; // 점프 횟수
    public bool IsGrounded { get; private set; } = false;

    private bool canDash = true; // 대쉬 가능한지
    private bool isDashing; // 현재 대쉬 중인지
    private bool isDashCooldownComplete = true;  // 대쉬 쿨다운이 완료되었는지
    [SerializeField] private float dashPower = 24f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private float dashStartTime; // 대쉬 시작 시간
    private float originalGravityScale; // 기본 중력 값

    private bool isPressingDown = false;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        groundLayer = LayerMask.GetMask("Ground", "Platform");
        platformLayer = LayerMask.GetMask("Platform");
    }

    private void Update()
    {
        //if (isDashing)
        //{
        //    return; // 대쉬 중에 다른 입력 x
        //}
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRange, groundLayer);
        IsGrounded = hit.collider != null;
        if (IsGrounded)
        {
            if (!canDash && isDashCooldownComplete)
            {
                canDash = true;  // 땅에 닿음 + 쿨다운이 완료되었다면 대쉬 가능
            }

            if (rb.velocity.y <= 0)
            {
                jumpCount = 0; // 땅에 닿으면 점프 횟수 초기화
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

    public void Move(float inputX)
    {
        if (!isDashing)
        {
            // 대쉬 중이 아닐 때만 이동
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

            if (inputX != 0)
            {
                // 방향에 따른 플레이어 스프라이트 전환
                transform.localScale = new Vector3(inputX > 0 ? 1 : -1, 1, 1);
            }

            if (inputX == 0 && IsGrounded)
            {
                // 정지 상태
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private bool IsPlatformLayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRange, platformLayer);
        return hit.collider != null;
    }

    public void Jump()
    {
        if (isPressingDown && IsPlatformLayer()) // 아래 방향키를 누르면서 점프 키 + 플랫폼인 경우
        {
            DownPlatform();
            return;
        }

        if ((IsGrounded || isDashing) && jumpCount < maxJumpCount) // 땅에서 점프
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // 수직 속도 초기화
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++; // 점프 횟수 증가 (첫 번째 점프)
        }

        else if (!IsGrounded && jumpCount > 0 && jumpCount < maxJumpCount)  // 공중에서 추가 점프
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // 수직 속도 초기화
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    public void Dash()
    {
        if (canDash && !isDashing) // 대쉬가 가능하고 현재 대쉬 중이 아닐 때
        {
            isDashing = true;
            dashStartTime = Time.time;
            canDash = false;
            isDashCooldownComplete = false;  // 쿨다운 재시작
            tr.emitting = true; // 대쉬 효과 
            rb.velocity = new Vector2(transform.localScale.x * dashPower, rb.velocity.y);
            StartCoroutine(DashCooldown()); // 대쉬 쿨다운 코루틴
        }
    }

    private void EndDash() // 대쉬 종료 처리
    {
        isDashing = false;
        rb.gravityScale = originalGravityScale;  // 대쉬가 끝나면 중력 스케일을 원래대로 복원
        tr.emitting = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);  // 수평 속도 유지, 수직 속도 0
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);

        isDashCooldownComplete = true;  // 쿨다운 완료
        if (IsGrounded)
        {
            canDash = true; // 땅에 있으면 대쉬 다시 가능
        }
    }
    public void SetIsPressingDown(bool isPressing)
    {
        isPressingDown = isPressing;
    }



    public void DownPlatform()
    {
        if (isPressingDown)
        {
            // 플레이어의 콜라이더 비활성화 (플랫폼 아래로 하강)
            rb.velocity = new Vector2(rb.velocity.x, jumpPower * 0.25f); // 살짝 점프
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
                Invoke("EnableCollider", 0.5f); // 0.5초 후에 다시 콜라이더 활성화
            }
        }
    }
    private void EnableCollider()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }
    }
}
