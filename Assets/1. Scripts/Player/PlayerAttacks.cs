using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAttacks : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerStatus playerStatus;
    private PlayerController _playerController;

    public AttackTypes currentAttackType;

    [Header("Bullet Setting")]
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private GameObject ChargedBulletPref;

    [Header("Range Attack Parameter")]
    [SerializeField] private float ShotPower = 15f;
    [SerializeField] private float fireDelay = 0.15f;
    [SerializeField] private float ChargeShotPower = 20f;
    [SerializeField] private float maxChargeTime = 1.5f;
    private float lastFireTime = 0;
    private float chargeTime = 0f;
    private bool isCharging = false;
    private bool isFullCharge = false;

    [Header("Melee Attack Parameter")]
    [SerializeField] private float attackDelay = 0.2f;
    [SerializeField] private float comboDelay = 0.5f;
    private int attackSequence = 0;
    private float attackTimer = 0f;
    private float comboTimer = 0f;
    private bool canJumpAttack = true;


    [Header("Other Skill Parameter")]
    [SerializeField] private float deflectCooldown = 0.5f;
    private float lastDeflectTime = 0f;


    [Header("Hit Boxes & Position")]
    [SerializeField] private Collider2D meleeAttackCollider;
    [SerializeField] private Collider2D meleeAttackCollider_2;
    [SerializeField] private Collider2D meleeAttackCollider_3;
    [SerializeField] private Collider2D jumpAttackCollider;
    [SerializeField] private Collider2D deflectZoneCollider;
    [SerializeField] private Transform rangeAttackPosition;

    [Header("Charging Effect")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    [SerializeField] public Material chargingMaterial;
    [SerializeField] public Color color1 = new Color(1f, 180f / 255f, 70f / 255f, 1f);
    [SerializeField] public Color color2 = new Color(220f / 255f, 160f / 255f, 40f / 255f, 1f);

    private void Awake()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStatus = GetComponent<PlayerStatus>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        originalMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else if (comboTimer <= 0 && attackSequence != 0)
        {
            attackSequence = 0;  
        }


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
            currentAttackType = AttackTypes.RangeAttack;
            CreateBullet(bulletPref, ShotPower, false);
        }
    }

    public void ChargeShot()
    {
        currentAttackType = AttackTypes.ChargeShot;
        playerAnimations.FireEffect();
        CreateBullet(ChargedBulletPref, ChargeShotPower, true);
    }


    private void CreateBullet(GameObject bulletPrefab, float shotPower, bool isChargeShot)
    {
        Vector3 direction = transform.right * transform.localScale.x;
        GameObject bullet = Instantiate(bulletPrefab, rangeAttackPosition.position + direction, Quaternion.identity);
        playerAnimations.Fired();

        PlayerRangeAttackHandler handler = bullet.GetComponent<PlayerRangeAttackHandler>();
        if (handler != null)
        {
            handler.playerAttacks = this;
            handler.playerStatus = playerStatus;
        }

        float ChargebulletDirection = transform.localScale.x > 0 ? 1f : -1f;
        if (isChargeShot)
        {
            bullet.transform.localScale = new Vector3(-2.5f * ChargebulletDirection, 2.5f, 1f);
        }
        else
        {
            bullet.transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
        }

        bullet.GetComponent<Rigidbody2D>().AddForce(direction * shotPower, ForceMode2D.Impulse);
        lastFireTime = Time.time;
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



    public void MeleeAttack()
    {
        if (attackTimer <= 0 && playerMovement.IsGround())
        {
            switch (attackSequence)
            {
                case 0:
                    currentAttackType = AttackTypes.MeleeAttack;
                    PerformAttack(AttackTypes.MeleeAttack, meleeAttackCollider);
                    playerAnimations.Attacking();
                    playerAnimations.MeleeAttackEffect();
                    attackSequence = 1; 
                    break;
                case 1:
                    if (_playerController.LockAction[Paction.ComboAttack])
                    {
                        currentAttackType = AttackTypes.ComboAttack1;
                        PerformAttack(AttackTypes.ComboAttack1, meleeAttackCollider_2);
                        playerAnimations.Attacking2();
                        playerAnimations.MeleeAttackEffect2();
                        attackSequence = 2;

                    }
                    else
                    {
                        // 콤보 공격이 잠겨있으면 첫 번째 공격으로
                        attackSequence = 0;
                    }
                    break;
                case 2:
                    if (playerStatus.Stamina >= 25 && _playerController.LockAction[Paction.ComboAttack])
                    {
                        currentAttackType = AttackTypes.ComboAttack2;
                        PerformAttack(AttackTypes.ComboAttack2, meleeAttackCollider_2);
                        playerAnimations.Attacking3();
                        playerAnimations.MeleeAttackEffect3();
                        attackSequence = 0;
                        attackTimer = attackDelay; // 기본 공격으로 돌아가기 전 딜레이

                        playerStatus.UseStamina(25);
                    }
                    else
                    {
                        attackSequence = 0;
                    }
                    break;
            }

            attackTimer = attackDelay;
            comboTimer = comboDelay;
        }
    }

    private void PerformAttack(AttackTypes attackType, Collider2D attackCollider)
    {
        attackCollider.enabled = true;
        Invoke("DisableAttack", 0.25f); 
    }

    private void DisableAttack()
    {
        meleeAttackCollider.enabled = false;
        meleeAttackCollider_2.enabled = false;
        meleeAttackCollider_3.enabled = false;
    }

    public void JumpAttack()
    {
        if (canJumpAttack && !playerMovement.IsGround() && playerStatus.Stamina >= 25)
        {
            jumpAttackCollider.enabled = true;

            currentAttackType = AttackTypes.JumpAttack;

            canJumpAttack = false;
            playerStatus.UseStamina(25);

            StartCoroutine(MultiHitJumpAttack());

            playerAnimations.JumpAttacking();
            playerAnimations.JumpAttackEffect();
        }
    }

    private IEnumerator MultiHitJumpAttack()
    {
        int hit = 3; 
        for (int i = 0; i < hit; i++)
        {
            jumpAttackCollider.enabled = true;
            yield return new WaitForSeconds(0.1f);
            jumpAttackCollider.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }

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
