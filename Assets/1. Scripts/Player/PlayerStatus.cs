using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    private PlayerAnimations playerAnimations;
    private PlayerUI playerUI;

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
        playerAnimations = GetComponent<PlayerAnimations>();
        //hitCollider = GetComponent<Collider2D>();
        playerUI = FindObjectOfType<PlayerUI>();
        maxStamina = stamina;
        lastStaminaUseTime = Time.time;
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
            TakeDamage(1);
            //if (collision == hitCollider) // 충돌한 콜라이더가 플레이어의 피격용 콜라이더면
            //{

            //}
        }
    }


}
