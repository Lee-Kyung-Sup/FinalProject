using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private PlayerAnimations playerAnimations;

    [SerializeField] GameObject bulletPref;

    [SerializeField] private float firePower = 15f; // 발사 힘
    [SerializeField] private float fireDelay = 0.1f; // 발사 딜레이
    private float lastFireTime = 0; // 마지막 발사 시간


    [SerializeField] private Collider2D meleeAttackCollider;
    [SerializeField] private Transform rangeAttackPosition;
    //[SerializeField] private SpriteRenderer meleeAttackSprite;
    //[SerializeField] private Animator meleeAttackAnimator;

    private void Start()
    {
        playerAnimations = GetComponent<PlayerAnimations>();

        meleeAttackCollider.enabled = false;
        //meleeAttackSprite.enabled = false;
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

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * firePower, ForceMode2D.Impulse);

            lastFireTime = Time.time; // 마지막 발사 시간 업데이트
        }
    }

    public void Attack()
    {
        //meleeAttackCollider.gameObject.transform.SetParent(null);
        meleeAttackCollider.enabled = true;
        //meleeAttackSprite.enabled = true;
        //meleeAttackAnimator.SetTrigger("Attack");
        Invoke("DisableAttack", 0.25f);
        playerAnimations.Attacking();
    }

    private void DisableAttack()
    {
        //meleeAttackCollider.gameObject.transform.SetParent(this.transform);
        meleeAttackCollider.enabled = false;
        //meleeAttackSprite.enabled = false;
        //meleeAttackAnimator.ResetTrigger("Attack");
    }

    // Attack 2, 3  TODO (콤보 공격)

    public void JumpAttack()
    {

    }
}
