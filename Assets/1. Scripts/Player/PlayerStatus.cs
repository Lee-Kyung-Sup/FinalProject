using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerUI playerUI;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;


    [SerializeField] private int health = 3;
    [SerializeField] private float stamina = 100;
    [SerializeField] private float staminaRecoveryRate = 100;
    [SerializeField] private float staminaRecoveryDelay = 1f;
    [SerializeField] private int atk = 1;

    private float lastStaminaUseTime;
    private float maxStamina;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 20f;
    [SerializeField] private int maxJumpCount = 2;


    public float Speed => speed;
    public float JumpPower => jumpPower;
    public int MaxJumpCount => maxJumpCount;
    public float Stamina => stamina;

    public int Atk => atk;

    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody2D>();

        lastStaminaUseTime = Time.time;
        maxStamina = stamina;
        //hitCollider = GetComponent<Collider2D>();
    }

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
        OnInvincible();
        playerAnimations.GetHit();
        health -= damage;
        playerUI.UpdateHeartUI(health);

        if (health <= 0)
        {
            PlayerDead();
        }

    }

    public void OnInvincible()
    {
        gameObject.layer = 18;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        StartCoroutine(BlinkEffect(3f, 0.1f)); 
    }

    private IEnumerator BlinkEffect(float duration, float interval)
    {
        float time = 0;
        while (time < duration)
        {
            if (spriteRenderer.color.a == 0.5f)
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.7f);
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            }

            yield return new WaitForSeconds(interval);
            time += interval;
        }
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1, 1);
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

    private void PlayerDead() // 플레이어 죽음
    {
        playerAnimations.Dead();
        GameManager.instance.OnGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (((1 << collision.gameObject.layer) & ((1 << 7) | (1 << 12))) != 0) // 7. 몬스터 ,  12. 에네미 불렛
        { 

            TakeDamage(1);
            playerMovement.OnKnockback(collision.transform.position);

        }
    }
}




