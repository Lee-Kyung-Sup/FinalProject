using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAttacks : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;

    [Header("Bullet Setting")]
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private GameObject ChargedBulletPref;

    [Header("Attack Parameter")]
    [SerializeField] private float ShotPower = 15f;
    [SerializeField] private float fireDelay = 0.15f;
    [SerializeField] private float ChargeShotPower = 20f;
    [SerializeField] private float maxChargeTime = 1.5f;
    [SerializeField] private float deflectCooldown = 0.5f;

    private float lastFireTime = 0;
    private float chargeTime = 0f;
    private float lastDeflectTime = 0f;

    [Header("Hit Boxes & Position")]
    [SerializeField] private Collider2D meleeAttackCollider;
    [SerializeField] private Collider2D jumpAttackCollider;
    [SerializeField] private Collider2D deflectZoneCollider;
    [SerializeField] private Transform rangeAttackPosition;

    [Header("Charging Effect")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    [SerializeField] public Material chargingMaterial;
    [SerializeField] public Color color1 = new Color(1f, 180f / 255f, 70f / 255f, 1f);
    [SerializeField] public Color color2 = new Color(220f / 255f, 160f / 255f, 40f / 255f, 1f);

    private bool canJumpAttack = true;
    private bool isCharging = false;
    private bool isFullCharge = false;

    private void Start()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStatus = GetComponent<PlayerStatus>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        if (playerMovement.IsGround() && !canJumpAttack)
        {
            canJumpAttack = true;
        }

        if (isCharging)
        {
            chargeTime += Time.deltaTime;

            if (chargeTime > 0.15f)
            {
                playerAnimations.Charging(true); // 충전 애니메이션 시작
            }
        }
        if (isCharging && !isFullCharge && chargeTime >= maxChargeTime)
        {
            isFullCharge = true;
            spriteRenderer.material = chargingMaterial; // 차지 머테리얼로 변경
            StartCoroutine(ChargingBlinkEffect()); // 색상 변경 코루틴 시작
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
        isCharging = true;
    }
    public void ReleaseCharge()
    {
        isCharging = false;
        isFullCharge = false;
        StopAllCoroutines();

        spriteRenderer.material = originalMaterial;
        spriteRenderer.color = Color.white;

        if (chargeTime >= maxChargeTime)
        {
            ChargeShot();
        }
        else
        {
            Fire();
        }
        chargeTime = 0f; // 충전 시간 리셋
        playerAnimations.Charging(false);
    }

    private IEnumerator ChargingBlinkEffect()
    {
        Debug.Log("깜빡임 시작");
        while (isCharging)
        {
            spriteRenderer.material.SetColor("_Color", color1);
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.material.SetColor("_Color", color2);
            yield return new WaitForSeconds(0.05f);
        }
        spriteRenderer.material.SetColor("_Color", Color.white);
    }

    public void ChargeShot()
    {
        spriteRenderer.material = originalMaterial;
        playerAnimations.Charging(false);
        playerAnimations.FireEffect();
        playerStatus.UseStamina(25);

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
            Debug.Log("점프어택");
            jumpAttackCollider.enabled = true;

            canJumpAttack = false;
            playerStatus.UseStamina(25);

            StartCoroutine(MultiHitJumpAttack());

            playerAnimations.JumpAttacking();
            playerAnimations.JumpAttackEffect();
        }
        else
        {
            Attack();
        }
    }

    private IEnumerator MultiHitJumpAttack()
    {
        jumpAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        jumpAttackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        jumpAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        jumpAttackCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        jumpAttackCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        jumpAttackCollider.enabled = false;

        canJumpAttack = true;
    }



    public void Deflect()
    {
        if (Time.time - lastDeflectTime >= deflectCooldown && playerStatus.Stamina >= 25)
        {
            lastDeflectTime = Time.time;
            playerAnimations.Deflection();
            StartCoroutine(OnDeflectZone());
            playerStatus.UseStamina(25);
        }
    }

    private IEnumerator OnDeflectZone()
    {
        deflectZoneCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        deflectZoneCollider.enabled = false;
    }
}
