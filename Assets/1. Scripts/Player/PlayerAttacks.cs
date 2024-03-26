using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;

    [SerializeField] GameObject bulletPref;

    [SerializeField] private float firePower = 15f;
    [SerializeField] private float fireDelay = 0.1f;
    private float lastFireTime = 0;


    [SerializeField] private Collider2D meleeAttackCollider;
    [SerializeField] private Collider2D jumpAttackCollider;
    [SerializeField] private Transform rangeAttackPosition;

    private bool canJumpAttack = true;


    private void Start()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();

        meleeAttackCollider.enabled = false;
        jumpAttackCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if (playerMovement.IsGround())
        {
            canJumpAttack = true;
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

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * firePower, ForceMode2D.Impulse);

            lastFireTime = Time.time; // ������ �߻� �ð� ������Ʈ
        }
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
        if (canJumpAttack && !playerMovement.IsGround())
        {
            jumpAttackCollider.enabled = true;
            canJumpAttack = false;
            Invoke("DisableJumpAttack", 0.4f);

            playerAnimations.JumpAttacking();
            playerAnimations.JumpAttackEffect();
        }
    }

    private void DisableJumpAttack()
    {
        jumpAttackCollider.enabled = false;
    }
}
