using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerUI playerUI;
    private Rigidbody2D rb;

    //[SerializeField] private Collider2D hitCollider; // �ǰݿ� �ݶ��̴�
    [SerializeField] private int health = 3; // ĳ���� ü��
    [SerializeField] private float stamina = 100; // ĳ���� ���¹̳�
    [SerializeField] private float staminaRecoveryRate = 100; // �ʴ� ���¹̳� ȸ����
    [SerializeField] private float staminaRecoveryDelay = 1f; // ���¹̳� ȸ�� ���� �ð�
    [SerializeField] private int Atk = 1; // ĳ���� ���ݷ� (���� ���� ��ĵ��� ���� TODO)

    private float lastStaminaUseTime;
    private float maxStamina;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 20f;
    [SerializeField] private int maxJumpCount = 2; // �ִ� ���� ���� Ƚ��


    public float Speed => speed;
    public float JumpPower => jumpPower;
    public int MaxJumpCount => maxJumpCount;
    public float Stamina => stamina;

    // Start is called before the first frame update
    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();

        playerMovement = GetComponent<PlayerMovement>();
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
        playerAnimations.GetHit();
        health -= damage;
        playerUI.UpdateHeartUI(health);

        if (health <= 0)
        {
            Die();
        }
        // ���� �� �˹� �޼���
        // ���� �� �����ð� ���� �޼���
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
        Debug.Log("�÷��̾� ����");
        playerAnimations.Dead(); // die �ִϸ��̼�

        // �÷��̾� ��� �� ���� TODO
        // DisableControls();
        // ShowGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Monster"))
        {
            TakeDamage(1);
            playerMovement.KnockbackAndInvincibility(collision.transform.position);

            //if (collision == hitCollider) // �浹�� �ݶ��̴��� �÷��̾��� �ǰݿ� �ݶ��̴���
            //{
            //}
        }
    }
}




