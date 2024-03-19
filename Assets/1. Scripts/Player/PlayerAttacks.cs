using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;

    [SerializeField] private float firePower = 15f; // 발사 힘
    [SerializeField] private float fireDelay = 0.1f; // 발사 딜레이
    private float lastFireTime = 0; // 마지막 발사 시간


    [SerializeField] private Collider2D meleeAttackCollider;
    //[SerializeField] private SpriteRenderer meleeAttackSprite;
    //[SerializeField] private Animator meleeAttackAnimator;

    private void Start()
    {
        meleeAttackCollider.enabled = false;
        //meleeAttackSprite.enabled = false;
    }


    public void Fire()
    {

        if (Time.time - lastFireTime >= fireDelay)
        {
            Vector3 direction = transform.right * transform.localScale.x; // 플레이어의 방향에 따라 발사 방향 결정
            GameObject bullet = Instantiate(bulletPref, transform.position + direction, Quaternion.identity);

            // 플레이어의 방향에 따른 투사체 스프라이트 스케일 반전
            float bulletDirection = transform.localScale.x > 0 ? 1f : -1f;
            bullet.transform.localScale = new Vector3(bulletDirection, 1f, 1f);

            bullet.GetComponent<Rigidbody2D>().AddForce(direction * firePower, ForceMode2D.Impulse);

            lastFireTime = Time.time; // 마지막 발사 시간 업데이트
        }
    }

    public void Attack()
    {
        meleeAttackCollider.enabled = true;
        //meleeAttackSprite.enabled = true;
        //meleeAttackAnimator.SetTrigger("Attack");
        Invoke("DisableAttack", 0.25f);
    }

    private void DisableAttack()
    {
        meleeAttackCollider.enabled = false;
        //meleeAttackSprite.enabled = false;
        //meleeAttackAnimator.ResetTrigger("Attack");
    }
}
