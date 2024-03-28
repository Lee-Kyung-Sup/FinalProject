using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeflectHandler : MonoBehaviour
{
    [SerializeField] private float deflectPower = 2f;
    [SerializeField] private GameObject deflectEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Projectile"))
        {
            Rigidbody2D bulletRb = collision.GetComponent<Rigidbody2D>();

            if(bulletRb != null )
            {
                Instantiate(deflectEffect, collision.transform.position, Quaternion.identity);
                Vector2 deflectDirection = new Vector2(-bulletRb.velocity.x * deflectPower, -bulletRb.velocity.y * deflectPower);
                bulletRb.velocity = deflectDirection;

                collision.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
                // 몬스터가 PlayerBullet에 닿을 때 데미지를 주도록 데미지 로직 수정 TODO
            }
        }
    }

}
