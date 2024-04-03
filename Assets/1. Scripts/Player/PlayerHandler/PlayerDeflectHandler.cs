using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeflectHandler : MonoBehaviour
{
    [SerializeField] private float deflectPower = 2f;
    [SerializeField] private GameObject deflectEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & ((1 << 11) | (1 << 12))) != 0) // 11. Projectile  12. EnemyBullet
        {
            Rigidbody2D bulletRb = collision.GetComponent<Rigidbody2D>();

            if(bulletRb != null )
            {
                GameObject effect = ObjectManager.Instance.MakeObj("PlayerDeflectEffect"); // Ǯ���� ����Ʈ ������
                if (effect != null)
                {
                    effect.transform.position = collision.transform.position;
                    effect.transform.rotation = Quaternion.identity;
                    effect.SetActive(true); // ����Ʈ Ȱ��ȭ
                }

                Vector2 deflectDirection = new Vector2(-bulletRb.velocity.x * deflectPower, -bulletRb.velocity.y * deflectPower);
                bulletRb.velocity = deflectDirection;

                //collision.gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
                collision.gameObject.layer = 20;
                // ���Ͱ� PlayerBullet�� ���� �� �������� �ֵ��� ������ ���� ���� TODO
            }
        }
    }

}
