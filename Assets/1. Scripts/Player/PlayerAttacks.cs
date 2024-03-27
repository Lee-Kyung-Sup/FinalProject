using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;

    [SerializeField] GameObject bulletPref;
    [SerializeField] GameObject ChargedBulletPref;

    [SerializeField] private float ShotPower = 15f;
    [SerializeField] private float fireDelay = 0.1f;
    private float lastFireTime = 0;

    private bool isCharging = false;
    [SerializeField] private float ChargeShotPower = 20f;
    private float chargeTime = 0f;
    [SerializeField] private float maxChargeTime = 2f;

    [SerializeField] private Collider2D meleeAttackCollider;
    [SerializeField] private Collider2D jumpAttackCollider;
    [SerializeField] private Transform rangeAttackPosition;

    private bool canJumpAttack = true;


    private void Start()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStatus = GetComponent<PlayerStatus>();

        meleeAttackCollider.enabled = false;
        jumpAttackCollider.enabled = false;
    }

    private void Update()
    {
        if (playerMovement.IsGround())
        {
            canJumpAttack = true;
        }

        if (isCharging)
        {
            chargeTime += Time.deltaTime;
        }
    }


    public void Fire()
    {
        if (Time.time - lastFireTime >= fireDelay)
        {
            Vector3 direction = transform.right * transform.localScale.x; // �÷��̾��� ���⿡ ���� �߻� ���� ����
            GameObject bullet = Instantiate(bulletPref, rangeAttackPosition.position + direction, Quaternion.identity);
            playerAnimations.Fired();

            // �÷��̾��� ���⿡ ���� ����ü ��������Ʈ ������ ����
            float bulletDirection = transform.localScale.x > 0 ? 1f : -1f;
            bullet.transform.localScale = new Vector3(bulletDirection, 1f, 1f);

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * ShotPower, ForceMode2D.Impulse);

            lastFireTime = Time.time; // ������ �߻� �ð� ������Ʈ
        }
    }

    public void StartCharging()
    {
        Debug.Log("���� ����");
        isCharging = true;
        chargeTime = 0f;
        playerAnimations.Charging(true);
    }
    public void ReleaseCharge()
    {
        isCharging = false;
        if (chargeTime >= maxChargeTime)
        {
            ChargeShot();
            Debug.Log("���� �Ϸ�");
        }
        else
        {
            Fire();
            playerAnimations.Charging(false);
        }
        chargeTime = 0f; // ���� �ð� ����
    }

    public void ChargeShot()
    {
        playerAnimations.Charging(false);
        Debug.Log("������ �߻�");
        Vector3 direction = transform.right * transform.localScale.x; // �÷��̾��� ���⿡ ���� �߻� ���� ����
        GameObject bullet = Instantiate(ChargedBulletPref, rangeAttackPosition.position + direction, Quaternion.identity);
        playerAnimations.Fired();

        float bulletDirection = transform.localScale.x < 0 ? 2.5f : -2.5f;
        bullet.transform.localScale = new Vector3(bulletDirection, 2.5f, 1f);

        bullet.GetComponent<Rigidbody2D>().AddForce(direction * ChargeShotPower, ForceMode2D.Impulse);
    }

    public void Attack()
    {
        if (playerMovement.IsGround())
        meleeAttackCollider.enabled = true;
        Invoke("DisableAttack", 0.25f);
        playerAnimations.Attacking();
    }

    private void DisableAttack()
    {
        meleeAttackCollider.enabled = false;
    }

    public void JumpAttack()
    {
        if (canJumpAttack && !playerMovement.IsGround() && playerStatus.Stamina >= 25)
        {
            jumpAttackCollider.enabled = true;
            canJumpAttack = false;
            playerStatus.UseStamina(25);

            Invoke("DisableJumpAttack", 0.4f);

            playerAnimations.JumpAttacking();
            playerAnimations.JumpAttackEffect();
        }
        else
        {
            Attack();
        }
    }

    private void DisableJumpAttack()
    {
        jumpAttackCollider.enabled = false;
    }
}
