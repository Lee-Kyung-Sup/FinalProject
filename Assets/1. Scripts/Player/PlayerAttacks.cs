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
            Vector3 direction = transform.right * transform.localScale.x; // 플레이어의 방향에 따라 발사 방향 결정
            GameObject bullet = Instantiate(bulletPref, rangeAttackPosition.position + direction, Quaternion.identity);
            playerAnimations.Fired();

            // 플레이어의 방향에 따른 투사체 스프라이트 스케일 반전
            float bulletDirection = transform.localScale.x > 0 ? 1f : -1f;
            bullet.transform.localScale = new Vector3(bulletDirection, 1f, 1f);

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * ShotPower, ForceMode2D.Impulse);

            lastFireTime = Time.time; // 마지막 발사 시간 업데이트
        }
    }

    public void StartCharging()
    {
        Debug.Log("차지 시작");
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
            Debug.Log("차지 완료");
        }
        else
        {
            Fire();
            playerAnimations.Charging(false);
        }
        chargeTime = 0f; // 충전 시간 리셋
    }

    public void ChargeShot()
    {
        playerAnimations.Charging(false);
        Debug.Log("차지샷 발사");
        Vector3 direction = transform.right * transform.localScale.x; // 플레이어의 방향에 따라 발사 방향 결정
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
