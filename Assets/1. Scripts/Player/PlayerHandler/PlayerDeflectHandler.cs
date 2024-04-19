using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeflectHandler : MonoBehaviour
{
    [SerializeField] private float deflectPower = 2f;
    [SerializeField] private GameObject deflectEffect;

    PlayerAttacks playerAttacks;
    PlayerStatus playerStatus;

    void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & ((1 << 11) | (1 << 12))) != 0) // 11. Projectile  12. EnemyBullet
        {
            Rigidbody2D bulletRb = collision.GetComponent<Rigidbody2D>();

            if(bulletRb != null )
            {
                GameObject effect = ObjectManager.Instance.MakeObj("PlayerDeflectEffect"); // 풀에서 이펙트 가져옴
                if (effect != null)
                {
                    effect.transform.position = collision.transform.position;
                    effect.transform.rotation = Quaternion.identity;
                    effect.SetActive(true); // 이펙트 활성화
                }

                Vector2 deflectDirection = new Vector2(-bulletRb.velocity.x * deflectPower, -bulletRb.velocity.y * deflectPower);
                bulletRb.velocity = deflectDirection;

                collision.gameObject.layer = 20; // 플레이어 bullet
                collision.gameObject.tag = "PlayerAttackBox";

                DeflectBullet deflectBullet = collision.gameObject.GetComponent<DeflectBullet>();
                if (deflectBullet != null && playerStatus != null)
                {
                    int damage = playerStatus.attackPower[AttackTypes.DeflectionAttack];
                    deflectBullet.Initialize(AttackTypes.DeflectionAttack, damage);
                    collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(deflectBullet.damage);
                }


            }
        }
    }

}
