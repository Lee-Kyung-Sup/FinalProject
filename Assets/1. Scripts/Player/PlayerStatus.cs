using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    private PlayerAnimations playerAnimations;
    private PlayerUI playerUI;
    private Rigidbody2D rb;

    [SerializeField] private float knockbackSpeed = 2f; // 넉백 속도
    [SerializeField] private float knockbackDuration = 0.1f; // 넉백 지속 시간

    //[SerializeField] private Collider2D hitCollider; // 피격용 콜라이더
    [SerializeField] private int health = 3; // 캐릭터 체력
    [SerializeField] private float stamina = 100; // 캐릭터 스태미너
    [SerializeField] private float staminaRecoveryRate = 100; // 초당 스태미너 회복량
    [SerializeField] private float staminaRecoveryDelay = 1f; // 스태미너 회복 지연 시간

    private float lastStaminaUseTime;
    private float maxStamina;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 20f;
    [SerializeField] private int maxJumpCount = 2; // 최대 점프 가능 횟수


    public float Speed => speed;
    public float JumpPower => jumpPower;
    public int MaxJumpCount => maxJumpCount;
    public float Stamina => stamina;

    // Start is called before the first frame update
    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();

        playerAnimations = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody2D>();

        lastStaminaUseTime = Time.time;
        maxStamina = stamina;
        //hitCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastStaminaUseTime >= staminaRecoveryDelay && stamina < 100)
        {
            RecoverStamina(staminaRecoveryRate * Time.deltaTime);
        }

        float Stamina = Mathf.Lerp(playerUI.staminaUI.value, maxStamina, Time.deltaTime * 10);
        playerUI.UpdateStaminaUI(Stamina);
    }

    void FixedUpdate()
    {
    }

    public void TakeDamage(int damage)
    {
            health -= damage;
            playerUI.UpdateHeartUI(health);
            playerAnimations.GetHit();

            if (health <= 0)
            {
                Die();
            }

        // 피해 시 넉백 메서드
        // 피해 시 일정시간 무적 메서드

    }

    public void UseStamina(float value)
    {
        stamina -= value;

        if (stamina < 0)
        {
            stamina = 0;
        }
        maxStamina = stamina;
        lastStaminaUseTime = Time.time;
        playerUI.UpdateStaminaUI(stamina);
    }

    private void RecoverStamina(float value)
    {
        stamina += value;
        if (stamina > 100)
        {
            stamina = 100;
        }
        maxStamina = stamina;
    }

    private void Die()
    {
        Debug.Log("플레이어 죽음");
        playerAnimations.Dead(); // die 애니메이션

        // 플레이어 사망 후 로직 TODO
        // DisableControls();
        // ShowGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Monster"))
        {

            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;
            
            knockbackDirection += new Vector3(0, 0.2f, 0);

            Vector3 knockbackPosition = transform.position + knockbackDirection * knockbackSpeed;
            StartCoroutine(KnockbackPlayer(knockbackPosition));

            TakeDamage(1);

            //if (collision == hitCollider) // 충돌한 콜라이더가 플레이어의 피격용 콜라이더면
            //{

            //}
        }
    }

    private IEnumerator KnockbackPlayer(Vector3 knockbackPosition)
    {
        float elapsedTime = 0;
        Vector2 startPosition = transform.position;

        while (elapsedTime < knockbackDuration)
        {
            transform.position = Vector3.Lerp(startPosition, knockbackPosition, (elapsedTime / knockbackDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}




