using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    private PlayerAnimations playerAnimations;
    private PlayerUI playerUI;

    //[SerializeField] private Collider2D hitCollider; // �ǰݿ� �ݶ��̴�
    [SerializeField] private int health = 3; // ĳ���� ü��
    [SerializeField] private float stamina = 100; // ĳ���� ���¹̳�
    [SerializeField] private float staminaRecoveryRate = 100; // �ʴ� ���¹̳� ȸ����
    [SerializeField] private float staminaRecoveryDelay = 1f; // ���¹̳� ȸ�� ���� �ð�

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
            //if (collision == hitCollider) // �浹�� �ݶ��̴��� �÷��̾��� �ǰݿ� �ݶ��̴���
            //{

            //}
        }
    }


}
